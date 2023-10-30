using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyWorkFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobRoleWeekTimeTypeAndAddTablesCollaboratorsWorkScheduleAndWorkScheduleInterface : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekHours",
                table: "JobRoles");

            migrationBuilder.AddColumn<string>(
                name: "WeekTime",
                table: "JobRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekTime",
                table: "JobRoles");

            migrationBuilder.AddColumn<short>(
                name: "WeekHours",
                table: "JobRoles",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
