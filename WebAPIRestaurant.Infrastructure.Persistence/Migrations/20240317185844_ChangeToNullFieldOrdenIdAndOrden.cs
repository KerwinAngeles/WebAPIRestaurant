using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIRestaurant.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToNullFieldOrdenIdAndOrden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Ordens_OrdenId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_OrdenId",
                table: "Tables");

            migrationBuilder.AlterColumn<int>(
                name: "OrdenId",
                table: "Tables",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_OrdenId",
                table: "Tables",
                column: "OrdenId",
                unique: true,
                filter: "[OrdenId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Ordens_OrdenId",
                table: "Tables",
                column: "OrdenId",
                principalTable: "Ordens",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Ordens_OrdenId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_OrdenId",
                table: "Tables");

            migrationBuilder.AlterColumn<int>(
                name: "OrdenId",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_OrdenId",
                table: "Tables",
                column: "OrdenId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Ordens_OrdenId",
                table: "Tables",
                column: "OrdenId",
                principalTable: "Ordens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
