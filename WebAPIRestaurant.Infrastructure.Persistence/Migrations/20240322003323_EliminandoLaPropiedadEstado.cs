using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIRestaurant.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EliminandoLaPropiedadEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Tables");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
