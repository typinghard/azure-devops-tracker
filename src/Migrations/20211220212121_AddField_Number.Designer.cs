﻿// <auto-generated />
using System;
using AzureDevopsTracker.Data;
using AzureDevopsTracker.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AzureDevopsTracker.Migrations
{
    [DbContext(typeof(AzureDevopsTrackerContext))]
    [Migration("20211220212121_AddField_Number")]
    partial class AddField_Number
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema(DataBaseConfig.SchemaName)
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AzureDevopsTracker.Entities.ChangeLog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Response")
                        .HasColumnType("varchar(max)");

                    b.Property<int>("Revision")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ChangeLogs");
                });

            modelBuilder.Entity("AzureDevopsTracker.Entities.ChangeLogItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ChangeLogId")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("WorkItemId")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("WorkItemType")
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ChangeLogId");

                    b.HasIndex("WorkItemId")
                        .IsUnique()
                        .HasFilter("[WorkItemId] IS NOT NULL");

                    b.ToTable("ChangeLogItems");
                });

            modelBuilder.Entity("AzureDevopsTracker.Entities.TimeByState", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("State")
                        .HasColumnType("varchar(200)");

                    b.Property<double>("TotalTime")
                        .HasColumnType("float");

                    b.Property<double>("TotalWorkedTime")
                        .HasColumnType("float");

                    b.Property<string>("WorkItemId")
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("WorkItemId");

                    b.ToTable("TimeByStates");
                });

            modelBuilder.Entity("AzureDevopsTracker.Entities.WorkItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Activity")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("AreaPath")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("AssignedTo")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Effort")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("IterationPath")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Lancado")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("OriginalEstimate")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("StoryPoints")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Tags")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("TeamProject")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Type")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("WorkItemParentId")
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("WorkItems");
                });

            modelBuilder.Entity("AzureDevopsTracker.Entities.WorkItemChange", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ChangedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("IterationPath")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("NewDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewState")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("OldDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OldState")
                        .HasColumnType("varchar(200)");

                    b.Property<double>("TotalWorkedTime")
                        .HasColumnType("float");

                    b.Property<string>("WorkItemId")
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("WorkItemId");

                    b.ToTable("WorkItemsChange");
                });

            modelBuilder.Entity("AzureDevopsTracker.Entities.ChangeLogItem", b =>
                {
                    b.HasOne("AzureDevopsTracker.Entities.ChangeLog", "ChangeLog")
                        .WithMany("ChangeLogItems")
                        .HasForeignKey("ChangeLogId");

                    b.HasOne("AzureDevopsTracker.Entities.WorkItem", null)
                        .WithOne("ChangeLogItem")
                        .HasForeignKey("AzureDevopsTracker.Entities.ChangeLogItem", "WorkItemId");
                });

            modelBuilder.Entity("AzureDevopsTracker.Entities.TimeByState", b =>
                {
                    b.HasOne("AzureDevopsTracker.Entities.WorkItem", "WorkItem")
                        .WithMany("TimeByStates")
                        .HasForeignKey("WorkItemId");
                });

            modelBuilder.Entity("AzureDevopsTracker.Entities.WorkItemChange", b =>
                {
                    b.HasOne("AzureDevopsTracker.Entities.WorkItem", "WorkItem")
                        .WithMany("WorkItemsChanges")
                        .HasForeignKey("WorkItemId");
                });
#pragma warning restore 612, 618
        }
    }
}
