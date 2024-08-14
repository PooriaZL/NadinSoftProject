using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NadinSoftProject.Migrations
{
    /// <inheritdoc />
    public partial class AddingRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRef",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserRef",
                table: "Products",
                column: "UserRef");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserRef",
                table: "Products",
                column: "UserRef",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserRef",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserRef",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserRef",
                table: "Products");
        }
    }
}
