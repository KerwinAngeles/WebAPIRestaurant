using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIRestaurant.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTableDisheOrden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Ordens_OrdenId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_OrdenId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "OrdenId",
                table: "Dishes");

            migrationBuilder.CreateTable(
                name: "DishesOrden",
                columns: table => new
                {
                    DishesId = table.Column<int>(type: "int", nullable: false),
                    OrdensId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishesOrden", x => new { x.DishesId, x.OrdensId });
                    table.ForeignKey(
                        name: "FK_DishesOrden_Dishes_DishesId",
                        column: x => x.DishesId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishesOrden_Ordens_OrdensId",
                        column: x => x.OrdensId,
                        principalTable: "Ordens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishesOrden_OrdensId",
                table: "DishesOrden",
                column: "OrdensId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishesOrden");

            migrationBuilder.AddColumn<int>(
                name: "OrdenId",
                table: "Dishes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_OrdenId",
                table: "Dishes",
                column: "OrdenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Ordens_OrdenId",
                table: "Dishes",
                column: "OrdenId",
                principalTable: "Ordens",
                principalColumn: "Id");
        }
    }
}
