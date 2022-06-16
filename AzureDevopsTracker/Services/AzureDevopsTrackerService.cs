﻿using AzureDevopsTracker.DTOs;
using AzureDevopsTracker.DTOs.Create;
using AzureDevopsTracker.DTOs.Update;
using AzureDevopsTracker.Entities;
using AzureDevopsTracker.Extensions;
using AzureDevopsTracker.Helpers;
using AzureDevopsTracker.Interfaces;
using AzureDevopsTracker.Interfaces.Internals;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevopsTracker.Services
{
    public class AzureDevopsTrackerService : IAzureDevopsTrackerService
    {
        public readonly IWorkItemRepository _workItemRepository;
        public readonly IWorkItemAdapter _workItemAdapter;
        public readonly IChangeLogItemRepository _changeLogItemRepository;

        public AzureDevopsTrackerService(
            IWorkItemAdapter workItemAdapter,
            IWorkItemRepository workItemRepository,
            IChangeLogItemRepository changeLogItemRepository)
        {
            _workItemAdapter = workItemAdapter;
            _workItemRepository = workItemRepository;
            _changeLogItemRepository = changeLogItemRepository;
        }

        public async Task Create(CreateWorkItemDTO create, bool addWorkItemChange = true)
        {
            var workItem = new WorkItem(create.Resource.Id);

            workItem.Update(create.Resource.Fields.Title,
                            create.Resource.Fields.TeamProject,
                            create.Resource.Fields.AreaPath,
                            create.Resource.Fields.IterationPath,
                            create.Resource.Fields.Type,
                            create.Resource.Fields.CreatedBy.ExtractEmail(),
                            create.Resource.Fields.AssignedTo.ExtractEmail(),
                            create.Resource.Fields.Tags,
                            create.Resource.Fields.Parent,
                            create.Resource.Fields.Effort,
                            create.Resource.Fields.StoryPoints,
                            create.Resource.Fields.OriginalEstimate,
                            create.Resource.Fields.Activity,
                            create.Resource.Fields.Lancado);

            if (addWorkItemChange)
                AddWorkItemChange(workItem, create);

            CheckWorkItemAvailableToChangeLog(workItem, create.Resource.Fields);

            await _workItemRepository.Add(workItem);
            await _workItemRepository.SaveChangesAsync();
        }

        public async Task Create(UpdatedWorkItemDTO updateDto)
        {
            var createDto = new CreateWorkItemDTO()
            {
                Resource = new DTOs.Create.Resource()
                {
                    Fields = updateDto.Resource.Revision.Fields,
                    Id = updateDto.Resource.WorkItemId,
                }
            };

            await Create(createDto, false);
        }

        public async Task Update(UpdatedWorkItemDTO update)
        {
            if (!_workItemRepository.Exist(update.Resource.WorkItemId).Result)
                await Create(update);

            var workItem = await _workItemRepository.GetByWorkItemId(update.Resource.WorkItemId);
            if (workItem is null)
                return;

            workItem.Update(update.Resource.Revision.Fields.Title,
                            update.Resource.Revision.Fields.TeamProject,
                            update.Resource.Revision.Fields.AreaPath,
                            update.Resource.Revision.Fields.IterationPath,
                            update.Resource.Revision.Fields.Type,
                            update.Resource.Revision.Fields.CreatedBy.ExtractEmail(),
                            update.Resource.Revision.Fields.AssignedTo.ExtractEmail(),
                            update.Resource.Revision.Fields.Tags,
                            update.Resource.Revision.Fields.Parent,
                            update.Resource.Revision.Fields.Effort,
                            update.Resource.Revision.Fields.StoryPoints,
                            update.Resource.Revision.Fields.OriginalEstimate,
                            update.Resource.Revision.Fields.Activity,
                            update.Resource.Revision.Fields.Lancado);

            AddWorkItemChange(workItem, update);

            CheckWorkItemAvailableToChangeLog(workItem, update.Resource.Revision.Fields);

            _workItemRepository.Update(workItem);
            await _workItemRepository.SaveChangesAsync();
        }

        public async Task<WorkItemDTO> GetByWorkItemId(string workItemId)
        {
            var workItem = await _workItemRepository.GetByWorkItemId(workItemId);
            if (workItem is null)
                return null;

            return _workItemAdapter.ToWorkItemDTO(workItem);
        }

        #region Support Methods
        public WorkItemChange ToWorkItemChange(
            string workItemId, string changedBy,
            string iterationPath, DateTime newDate, string newState,
            string oldState = null, DateTime? oldDate = null)
        {
            return new WorkItemChange(workItemId, changedBy.ExtractEmail(), iterationPath, newDate, newState, oldState, oldDate);
        }

        public void AddWorkItemChange(WorkItem workItem, CreateWorkItemDTO create)
        {
            var workItemChange = ToWorkItemChange(workItem.Id,
                                                  create.Resource.Fields.ChangedBy,
                                                  workItem.IterationPath,
                                                  create.Resource.Fields.CreatedDate.ToDateTimeFromTimeZoneInfo(),
                                                  create.Resource.Fields.State);

            if (CheckWorkItemChangeExists(workItem, workItemChange))
                return;

            workItem.AddWorkItemChange(workItemChange);
        }

        public void AddWorkItemChange(WorkItem workItem, UpdatedWorkItemDTO update)
        {
            var changedBy = update.Resource.Revision.Fields.ChangedBy ?? update.Resource.Fields.ChangedBy.NewValue;
            var workItemChange = ToWorkItemChange(workItem.Id,
                                      changedBy,
                                      workItem.IterationPath,
                                      update.Resource.Fields.StateChangeDate.NewValue.ToDateTimeFromTimeZoneInfo(),
                                      update.Resource.Fields.State.NewValue,
                                      update.Resource.Fields.State.OldValue,
                                      update.Resource.Fields.StateChangeDate.OldValue.ToDateTimeFromTimeZoneInfo());

            if (CheckWorkItemChangeExists(workItem, workItemChange))
                return;

            workItem.AddWorkItemChange(workItemChange);

            UpdateTimeByStates(workItem);
        }

        public void UpdateTimeByStates(WorkItem workItem)
        {
            RemoveTimeByStateFromDataBase(workItem);

            workItem.ClearTimesByState();
            workItem.AddTimesByState(workItem.CalculateTotalTimeByState());
        }

        public void RemoveTimeByStateFromDataBase(WorkItem workItem)
        {
            _workItemRepository.RemoveAllTimeByState(workItem.TimeByStates.ToList());
        }

        public void CheckWorkItemAvailableToChangeLog(WorkItem workItem, Fields fields)
        {
            if (workItem.CurrentStatus != "Closed" &&
                workItem.LastStatus == "Closed" &&
                workItem.ChangeLogItem is not null &&
                !workItem.ChangeLogItem.WasReleased)
                RemoveChangeLogItem(workItem);

            if (workItem.CurrentStatus != "Closed" ||
                fields.ChangeLogDescription.IsNullOrEmpty())
                return;

            if (workItem.ChangeLogItem is null)
                workItem.VinculateChangeLogItem(ToChangeLogItem(workItem, fields));
            else
                workItem.ChangeLogItem.Update(workItem.Title, workItem.Type, fields.ChangeLogDescription);
        }

        public bool CheckWorkItemChangeExists(WorkItem workItem, WorkItemChange newWorkItemChange)
        {
            return workItem.WorkItemsChanges.Any(x => x.NewDate == newWorkItemChange.NewDate &&
                                                      x.OldDate == newWorkItemChange.OldDate &&
                                                      x.NewState == newWorkItemChange.NewState &&
                                                      x.OldState == newWorkItemChange.OldState &&
                                                      x.ChangedBy == newWorkItemChange.ChangedBy &&
                                                      x.IterationPath == newWorkItemChange.IterationPath &&
                                                      x.TotalWorkedTime == newWorkItemChange.TotalWorkedTime);
        }

        public ChangeLogItem ToChangeLogItem(WorkItem workItem, Fields fields)
        {
            return new ChangeLogItem(workItem.Id, workItem.Title, fields.ChangeLogDescription, workItem.Type);
        }

        public void RemoveChangeLogItem(WorkItem workItem)
        {
            var changeLogItem = _changeLogItemRepository.GetById(workItem.ChangeLogItem?.Id).Result;
            if (changeLogItem is not null)
            {
                _changeLogItemRepository.Delete(changeLogItem);
                _changeLogItemRepository.SaveChangesAsync().Wait();

                workItem.RemoveChangeLogItem();
            }
        }

        public Task Create(string jsonText, bool addWorkItemChange = true)
        {
            throw new NotImplementedException();

            try
            {
                ReadJsonHelper.ReadJson(jsonText);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task Update(string jsonText)
        {
            throw new NotImplementedException();

            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}