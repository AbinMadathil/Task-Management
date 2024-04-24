using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMgmt.DataAccess.Migrations
{
    public partial class draftmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDraft",
                table: "ProjectTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDraft",
                table: "ProjectTasks");
        }
    }
}
