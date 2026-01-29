using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SendsProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class primarykey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryPersonId",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecipientId",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Packages_DeliveryPersonId",
                table: "Packages",
                column: "DeliveryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_RecipientId",
                table: "Packages",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_DeliveryPeople_DeliveryPersonId",
                table: "Packages",
                column: "DeliveryPersonId",
                principalTable: "DeliveryPeople",
                principalColumn: "DeliveryPersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Recipients_RecipientId",
                table: "Packages",
                column: "RecipientId",
                principalTable: "Recipients",
                principalColumn: "RecipientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_DeliveryPeople_DeliveryPersonId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Recipients_RecipientId",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_DeliveryPersonId",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_RecipientId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "DeliveryPersonId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "Packages");
        }
    }
}
