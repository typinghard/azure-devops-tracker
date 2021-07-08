using System;
using AzureDevopsStateTracker.Data;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AzureDevopsStateTracker.Migrations
{
    public partial class AzureDevopsStateTrackerInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: DataBaseConfig.SchemaName);

            migrationBuilder.CreateTable(
                name: "WorkItems",
                schema: DataBaseConfig.SchemaName,
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(200)", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    AssignedTo = table.Column<string>(type: "varchar(200)", nullable: true),
                    Type = table.Column<string>(type: "varchar(200)", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(200)", nullable: true),
                    Title = table.Column<string>(type: "varchar(200)", nullable: true),
                    TeamProject = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkItemsChange",
                schema: DataBaseConfig.SchemaName,
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(200)", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    WorkItemId = table.Column<string>(type: "varchar(200)", nullable: true),
                    NewDate = table.Column<DateTime>(nullable: false),
                    OldDate = table.Column<DateTime>(nullable: true),
                    NewState = table.Column<string>(type: "varchar(200)", nullable: true),
                    OldState = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItemsChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItemsChange_WorkItems_WorkItemId",
                        column: x => x.WorkItemId,
                        principalSchema: DataBaseConfig.SchemaName,
                        principalTable: "WorkItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkItemsStatusTime",
                schema: DataBaseConfig.SchemaName,
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(200)", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    WorkItemId = table.Column<string>(type: "varchar(200)", nullable: true),
                    State = table.Column<string>(type: "varchar(200)", nullable: true),
                    TotalTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItemsStatusTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItemsStatusTime_WorkItems_WorkItemId",
                        column: x => x.WorkItemId,
                        principalSchema: DataBaseConfig.SchemaName,
                        principalTable: "WorkItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemsChange_WorkItemId",
                schema: DataBaseConfig.SchemaName,
                table: "WorkItemsChange",
                column: "WorkItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemsStatusTime_WorkItemId",
                schema: DataBaseConfig.SchemaName,
                table: "WorkItemsStatusTime",
                column: "WorkItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkItemsChange",
                schema: DataBaseConfig.SchemaName);

            migrationBuilder.DropTable(
                name: "WorkItemsStatusTime",
                schema: DataBaseConfig.SchemaName);

            migrationBuilder.DropTable(
                name: "WorkItems",
                schema: DataBaseConfig.SchemaName);
        }
    }
}
