using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIRestaurant.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class notDeleteOnCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Ordens_OrdenId",
                table: "Dishes");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Ordens_OrdenId",
                table: "Dishes",
                column: "OrdenId",
                principalTable: "Ordens",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Ordens_OrdenId",
                table: "Dishes");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Ordens_OrdenId",
                table: "Dishes",
                column: "OrdenId",
                principalTable: "Ordens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
