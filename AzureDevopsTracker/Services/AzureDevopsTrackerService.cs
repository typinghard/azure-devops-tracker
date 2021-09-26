using AzureDevopsTracker.DTOs;
using AzureDevopsTracker.DTOs.Create;
using AzureDevopsTracker.DTOs.Update;
using AzureDevopsTracker.Entities;
using AzureDevopsTracker.Extensions;
using AzureDevopsTracker.Interfaces;
using AzureDevopsTracker.Interfaces.Internals;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevopsTracker.Services
{
    public class AzureDevopsTrackerService : IAzureDevopsTrackerService
    {
        internal readonly IWorkItemRepository _workItemRepository;
        public readonly IWorkItemAdapter _workItemAdapter;

        public AzureDevopsTrackerService(
            IWorkItemAdapter workItemAdapter, IWorkItemRepository workItemRepository)
        {
            _workItemAdapter = workItemAdapter;
            _workItemRepository = workItemRepository;
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
        #endregion
    }
}