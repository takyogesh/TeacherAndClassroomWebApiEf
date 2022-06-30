using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachersAndClassroomDll.Migrations
{
    public partial class addConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClassName",
                table: "Classrooms",
                type: "Varchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "Varchar(50)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClassName",
                table: "Classrooms",
                type: "Varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "Varchar(50)");
        }
    }
}
