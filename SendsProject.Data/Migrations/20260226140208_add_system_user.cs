using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SendsProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_system_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Recipients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DeliveryPeople",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_UserId",
                table: "Recipients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPeople_UserId",
                table: "DeliveryPeople",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryPeople_User_UserId",
                table: "DeliveryPeople",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipients_User_UserId",
                table: "Recipients",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryPeople_User_UserId",
                table: "DeliveryPeople");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipients_User_UserId",
                table: "Recipients");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_UserId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryPeople_UserId",
                table: "DeliveryPeople");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DeliveryPeople");
        }
    }
}
