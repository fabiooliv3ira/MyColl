using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MYCOLL.Migrations
{
    /// <inheritdoc />
    public partial class inicial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensCarrinho_Encomendas_EncomendaId",
                table: "ItensCarrinho");

            migrationBuilder.DropIndex(
                name: "IX_ItensCarrinho_EncomendaId",
                table: "ItensCarrinho");

            migrationBuilder.DropColumn(
                name: "EncomendaId",
                table: "ItensCarrinho");

            //migrationBuilder.DropColumn(
            //    name: "PrecoUnitario",
            //    table: "ItensCarrinho");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Encomendas",
                newName: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_EncomendaId",
                table: "Pagamentos",
                column: "EncomendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamentos_Encomendas_EncomendaId",
                table: "Pagamentos",
                column: "EncomendaId",
                principalTable: "Encomendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagamentos_Encomendas_EncomendaId",
                table: "Pagamentos");

            migrationBuilder.DropIndex(
                name: "IX_Pagamentos_EncomendaId",
                table: "Pagamentos");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Encomendas",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "EncomendaId",
                table: "ItensCarrinho",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoUnitario",
                table: "ItensCarrinho",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_ItensCarrinho_EncomendaId",
                table: "ItensCarrinho",
                column: "EncomendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensCarrinho_Encomendas_EncomendaId",
                table: "ItensCarrinho",
                column: "EncomendaId",
                principalTable: "Encomendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
