using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyWorkFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobRoleWeekTimeTypeAndAddTablesCollaboratorsWorkScheduleAndWorkScheduleInterface2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkScheduleInterfaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstShiftTimeIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstShiftTimeOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecondShiftTimeIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecondShiftTimeOut = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkScheduleInterfaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollaboratorWorkSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkScheduleInterfaceId = table.Column<int>(type: "int", nullable: false),
                    JobRoleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobRoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobWeekHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstShiftTimeIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstShiftTimeOut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondShiftTimeIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondShiftTimeOut = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorWorkSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorWorkSchedules_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorWorkSchedules_WorkScheduleInterfaces_WorkScheduleInterfaceId",
                        column: x => x.WorkScheduleInterfaceId,
                        principalTable: "WorkScheduleInterfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorWorkSchedules_UserId",
                table: "CollaboratorWorkSchedules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorWorkSchedules_WorkScheduleInterfaceId",
                table: "CollaboratorWorkSchedules",
                column: "WorkScheduleInterfaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorWorkSchedules");

            migrationBuilder.DropTable(
                name: "WorkScheduleInterfaces");
        }
    }
}
