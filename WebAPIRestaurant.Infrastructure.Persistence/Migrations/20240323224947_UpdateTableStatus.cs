using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIRestaurant.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tables_StatusId",
                table: "Tables");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_StatusId",
                table: "Tables",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tables_StatusId",
                table: "Tables");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_StatusId",
                table: "Tables",
                column: "StatusId",
                unique: true);
        }
    }
}
