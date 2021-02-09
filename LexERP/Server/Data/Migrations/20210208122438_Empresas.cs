using Microsoft.EntityFrameworkCore.Migrations;

namespace LexERP.Server.Data.Migrations
{
    public partial class Empresas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Empresa_EmpresaId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_ConceptosEconomicos_Empresa_EmpresaId",
                table: "ConceptosEconomicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Empresa_EmpresaId",
                table: "Contactos");

            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_Empresa_EmpresaId",
                table: "Expedientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Empresa_EmpresaId",
                table: "Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedores_Empresa_EmpresaId",
                table: "Proveedores");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarifas_Empresa_EmpresaId",
                table: "Tarifas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empresa",
                table: "Empresa");

            migrationBuilder.RenameTable(
                name: "Empresa",
                newName: "Empresas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empresas",
                table: "Empresas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Empresas_EmpresaId",
                table: "Clientes",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConceptosEconomicos_Empresas_EmpresaId",
                table: "ConceptosEconomicos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Empresas_EmpresaId",
                table: "Contactos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Empresas_EmpresaId",
                table: "Expedientes",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Empresas_EmpresaId",
                table: "Facturas",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedores_Empresas_EmpresaId",
                table: "Proveedores",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarifas_Empresas_EmpresaId",
                table: "Tarifas",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Empresas_EmpresaId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_ConceptosEconomicos_Empresas_EmpresaId",
                table: "ConceptosEconomicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Empresas_EmpresaId",
                table: "Contactos");

            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_Empresas_EmpresaId",
                table: "Expedientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Empresas_EmpresaId",
                table: "Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedores_Empresas_EmpresaId",
                table: "Proveedores");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarifas_Empresas_EmpresaId",
                table: "Tarifas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empresas",
                table: "Empresas");

            migrationBuilder.RenameTable(
                name: "Empresas",
                newName: "Empresa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empresa",
                table: "Empresa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Empresa_EmpresaId",
                table: "Clientes",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConceptosEconomicos_Empresa_EmpresaId",
                table: "ConceptosEconomicos",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Empresa_EmpresaId",
                table: "Contactos",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Empresa_EmpresaId",
                table: "Expedientes",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Empresa_EmpresaId",
                table: "Facturas",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedores_Empresa_EmpresaId",
                table: "Proveedores",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarifas_Empresa_EmpresaId",
                table: "Tarifas",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
