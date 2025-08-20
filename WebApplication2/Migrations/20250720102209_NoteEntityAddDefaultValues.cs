using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class NoteEntityAddDefaultValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Profiles_ProfileId",
                table: "Notes");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "Notes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Profiles_ProfileId",
                table: "Notes",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Profiles_ProfileId",
                table: "Notes");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "Notes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Profiles_ProfileId",
                table: "Notes",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
