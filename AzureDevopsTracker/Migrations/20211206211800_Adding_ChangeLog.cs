using System;
using AzureDevopsTracker.Data;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AzureDevopsTracker.Migrations
{
    public partial class Adding_ChangeLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangeLogItemId",
                schema: DataBaseConfig.SchemaName,
                table: "WorkItems",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangeLogItemId1",
                schema: DataBaseConfig.SchemaName,
                table: "WorkItems",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChangeLogs",
                schema: DataBaseConfig.SchemaName,
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(200)", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Response = table.Column<string>(type: "varchar(200)", nullable: true),
                    Revision = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChangeLogItems",
                schema: DataBaseConfig.SchemaName,
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(200)", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    WorkItemId = table.Column<string>(type: "varchar(200)", nullable: true),
                    Title = table.Column<string>(type: "varchar(200)", nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true),
                    WorkItemType = table.Column<string>(type: "varchar(200)", nullable: true),
                    ChangeLogId = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLogItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeLogItems_ChangeLogs_ChangeLogId",
                        column: x => x.ChangeLogId,
                        principalSchema: DataBaseConfig.SchemaName,
                        principalTable: "ChangeLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_ChangeLogItemId1",
                schema: DataBaseConfig.SchemaName,
                table: "WorkItems",
                column: "ChangeLogItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLogItems_ChangeLogId",
                schema: DataBaseConfig.SchemaName,
                table: "ChangeLogItems",
                column: "ChangeLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_ChangeLogItems_ChangeLogItemId1",
                schema: DataBaseConfig.SchemaName,
                table: "WorkItems",
                column: "ChangeLogItemId1",
                principalSchema: DataBaseConfig.SchemaName,
                principalTable: "ChangeLogItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_ChangeLogItems_ChangeLogItemId1",
                schema: DataBaseConfig.SchemaName,
                table: "WorkItems");

            migrationBuilder.DropTable(
                name: "ChangeLogItems",
                schema: DataBaseConfig.SchemaName);

            migrationBuilder.DropTable(
                name: "ChangeLogs",
                schema: DataBaseConfig.SchemaName);

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_ChangeLogItemId1",
                schema: DataBaseConfig.SchemaName,
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "ChangeLogItemId",
                schema: DataBaseConfig.SchemaName,
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "ChangeLogItemId1",
                schema: DataBaseConfig.SchemaName,
                table: "WorkItems");
        }
    }
}
