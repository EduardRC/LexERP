using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LexERP.Server.Data.Migrations
{
    public partial class bdinicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImporteHoraBase = table.Column<decimal>(type: "money", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abreviatura = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abreviatura = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    NIF = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Poblacion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormasDePago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abreviatura = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasDePago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Idiomas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abreviatura = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idiomas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoActuaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abreviatura = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TextoActuacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoActuaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoContactos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clase = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContactos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoContratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abreviatura = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContratos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoExpediente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abreviatura = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoExpediente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abreviatura = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_Areas_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Areas_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarifas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    Abreviatura = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Predeterminada = table.Column<bool>(type: "bit", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarifas_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdiomaId = table.Column<int>(type: "int", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paises_Idiomas_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "Idiomas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expedientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPublico = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    TipoExpedienteId = table.Column<int>(type: "int", nullable: false),
                    ResponsableId = table.Column<int>(type: "int", nullable: true),
                    SocioResponsableId = table.Column<int>(type: "int", nullable: true),
                    ResponsableComercialId = table.Column<int>(type: "int", nullable: true),
                    ResponsableFacturacionId = table.Column<int>(type: "int", nullable: true),
                    Referencia = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ReferenciaCliente = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubicacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Facturable = table.Column<bool>(type: "bit", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    TarifaId = table.Column<int>(type: "int", nullable: true),
                    EstimacionHoras = table.Column<int>(type: "int", nullable: false),
                    EstimacionImporte = table.Column<decimal>(type: "money", nullable: false),
                    AlarmaSobrepasoEstimacion = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Confidencial = table.Column<bool>(type: "bit", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expedientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expedientes_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expedientes_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expedientes_Expedientes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expedientes_Tarifas_TarifaId",
                        column: x => x.TarifaId,
                        principalTable: "Tarifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expedientes_TipoExpediente_TipoExpedienteId",
                        column: x => x.TipoExpedienteId,
                        principalTable: "TipoExpediente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iniciales = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    EsSocio = table.Column<bool>(type: "bit", nullable: false),
                    EsResponsableComercial = table.Column<bool>(type: "bit", nullable: false),
                    EsResponsableExpediente = table.Column<bool>(type: "bit", nullable: false),
                    EsResponsableFacturacion = table.Column<bool>(type: "bit", nullable: false),
                    EsCaptadorComisionista = table.Column<bool>(type: "bit", nullable: false),
                    ExpedienteId = table.Column<int>(type: "int", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TarifaDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarifaId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    ImporteHora = table.Column<decimal>(type: "money", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarifaDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TarifaDetalles_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TarifaDetalles_Tarifas_TarifaId",
                        column: x => x.TarifaId,
                        principalTable: "Tarifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarifaDetalles_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioLogs_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClienteServicio",
                columns: table => new
                {
                    ClientesId = table.Column<int>(type: "int", nullable: false),
                    ServiciosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteServicio", x => new { x.ClientesId, x.ServiciosId });
                    table.ForeignKey(
                        name: "FK_ClienteServicio_Servicios_ServiciosId",
                        column: x => x.ServiciosId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConceptosEconomicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoConceptoEconomico = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<decimal>(type: "money", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProveedorId = table.Column<int>(type: "int", nullable: true),
                    Facturable = table.Column<bool>(type: "bit", nullable: false),
                    ImpuestoTPC = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    ImpuestoImporte = table.Column<decimal>(type: "money", nullable: false),
                    IRPFTPC = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    IRPFImporte = table.Column<decimal>(type: "money", nullable: false),
                    FacturaId = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptosEconomicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConceptosEconomicos_ConceptosEconomicos_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ConceptosEconomicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConceptosEconomicos_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actuaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    ExpedienteId = table.Column<int>(type: "int", nullable: false),
                    TipoActuacionId = table.Column<int>(type: "int", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Minutos = table.Column<int>(type: "int", nullable: false),
                    PrecioHora = table.Column<decimal>(type: "money", nullable: false),
                    Importe = table.Column<decimal>(type: "money", nullable: false),
                    ConceptoEconomicoId = table.Column<int>(type: "int", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actuaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actuaciones_ConceptosEconomicos_ConceptoEconomicoId",
                        column: x => x.ConceptoEconomicoId,
                        principalTable: "ConceptosEconomicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actuaciones_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actuaciones_TipoActuaciones_TipoActuacionId",
                        column: x => x.TipoActuacionId,
                        principalTable: "TipoActuaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actuaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    TipoContratoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Periodicidad = table.Column<int>(type: "int", nullable: false),
                    DiaFacturacion = table.Column<int>(type: "int", nullable: false),
                    FormaDePagoId = table.Column<int>(type: "int", nullable: false),
                    PackHoras = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<decimal>(type: "money", nullable: false),
                    TextoFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioControlId = table.Column<int>(type: "int", nullable: true),
                    TarifaId = table.Column<int>(type: "int", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contratos_FormasDePago_FormaDePagoId",
                        column: x => x.FormaDePagoId,
                        principalTable: "FormasDePago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contratos_Tarifas_TarifaId",
                        column: x => x.TarifaId,
                        principalTable: "Tarifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contratos_TipoContratos_TipoContratoId",
                        column: x => x.TipoContratoId,
                        principalTable: "TipoContratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contratos_Usuarios_UsuarioControlId",
                        column: x => x.UsuarioControlId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPublica = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    Serie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ejercicio = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rectificativa = table.Column<bool>(type: "bit", nullable: false),
                    Rectificada = table.Column<bool>(type: "bit", nullable: false),
                    FacturaRectificadaId = table.Column<int>(type: "int", nullable: true),
                    MotivoRectificada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Poblacion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    PaisId = table.Column<int>(type: "int", nullable: false),
                    TotalCobrada = table.Column<decimal>(type: "money", nullable: false),
                    TotalHonorarios = table.Column<decimal>(type: "money", nullable: false),
                    TotalSuplidos = table.Column<decimal>(type: "money", nullable: false),
                    TotalGastos = table.Column<decimal>(type: "money", nullable: false),
                    TotalProvisionesRecibidas = table.Column<decimal>(type: "money", nullable: false),
                    TotalProvisionesDevueltas = table.Column<decimal>(type: "money", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturas_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facturas_Facturas_FacturaRectificadaId",
                        column: x => x.FacturaRectificadaId,
                        principalTable: "Facturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Saludo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    UbicacionPrincipalId = table.Column<int>(type: "int", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdiomaId = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_Idiomas_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "Idiomas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPublico = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Abreviatura = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Codigo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CodigoAlternativo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    NombreComercial = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ResponsableComercialId = table.Column<int>(type: "int", nullable: true),
                    ResponsableFacturacionId = table.Column<int>(type: "int", nullable: true),
                    CaptadorId = table.Column<int>(type: "int", nullable: true),
                    TarifaId = table.Column<int>(type: "int", nullable: true),
                    AplicarIVA = table.Column<bool>(type: "bit", nullable: false),
                    AplicarRetencion = table.Column<bool>(type: "bit", nullable: false),
                    Empleados = table.Column<int>(type: "int", nullable: false),
                    ValorFacturacion = table.Column<decimal>(type: "money", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Clientes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clientes_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clientes_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clientes_Tarifas_TarifaId",
                        column: x => x.TarifaId,
                        principalTable: "Tarifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_CaptadorId",
                        column: x => x.CaptadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_ResponsableComercialId",
                        column: x => x.ResponsableComercialId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_ResponsableFacturacionId",
                        column: x => x.ResponsableFacturacionId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPublico = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Empleados = table.Column<int>(type: "int", nullable: false),
                    ValorFacturacion = table.Column<decimal>(type: "money", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contactos_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contactos_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatosContacto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    TipoContactoId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosContacto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatosContacto_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatosContacto_TipoContactos_TipoContactoId",
                        column: x => x.TipoContactoId,
                        principalTable: "TipoContactos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonaSector",
                columns: table => new
                {
                    PersonasId = table.Column<int>(type: "int", nullable: false),
                    SectoresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaSector", x => new { x.PersonasId, x.SectoresId });
                    table.ForeignKey(
                        name: "FK_PersonaSector_Personas_PersonasId",
                        column: x => x.PersonasId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaSector_Sectores_SectoresId",
                        column: x => x.SectoresId,
                        principalTable: "Sectores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPublico = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoCliente = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proveedores_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proveedores_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Direccion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Poblacion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    PaisId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonaId = table.Column<int>(type: "int", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: false),
                    CreadoFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoFecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ubicaciones_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ubicaciones_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actuaciones_ConceptoEconomicoId",
                table: "Actuaciones",
                column: "ConceptoEconomicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Actuaciones_ExpedienteId",
                table: "Actuaciones",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Actuaciones_TipoActuacionId",
                table: "Actuaciones",
                column: "TipoActuacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Actuaciones_UsuarioId",
                table: "Actuaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_DepartamentoId",
                table: "Areas",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_ParentId",
                table: "Areas",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CaptadorId",
                table: "Clientes",
                column: "CaptadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EmpresaId",
                table: "Clientes",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ParentId",
                table: "Clientes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_PersonaId",
                table: "Clientes",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ResponsableComercialId",
                table: "Clientes",
                column: "ResponsableComercialId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ResponsableFacturacionId",
                table: "Clientes",
                column: "ResponsableFacturacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TarifaId",
                table: "Clientes",
                column: "TarifaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteServicio_ServiciosId",
                table: "ClienteServicio",
                column: "ServiciosId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptosEconomicos_ClienteId",
                table: "ConceptosEconomicos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptosEconomicos_EmpresaId",
                table: "ConceptosEconomicos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptosEconomicos_FacturaId",
                table: "ConceptosEconomicos",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptosEconomicos_ParentId",
                table: "ConceptosEconomicos",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptosEconomicos_ProveedorId",
                table: "ConceptosEconomicos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_EmpresaId",
                table: "Contactos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_PersonaId",
                table: "Contactos",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_ClienteId",
                table: "Contratos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_FormaDePagoId",
                table: "Contratos",
                column: "FormaDePagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_TarifaId",
                table: "Contratos",
                column: "TarifaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_TipoContratoId",
                table: "Contratos",
                column: "TipoContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_UsuarioControlId",
                table: "Contratos",
                column: "UsuarioControlId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosContacto_PersonaId",
                table: "DatosContacto",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosContacto_TipoContactoId",
                table: "DatosContacto",
                column: "TipoContactoId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_AreaId",
                table: "Expedientes",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_ClienteId",
                table: "Expedientes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_ContratoId",
                table: "Expedientes",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_EmpresaId",
                table: "Expedientes",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_ParentId",
                table: "Expedientes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_ResponsableComercialId",
                table: "Expedientes",
                column: "ResponsableComercialId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_ResponsableFacturacionId",
                table: "Expedientes",
                column: "ResponsableFacturacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_ResponsableId",
                table: "Expedientes",
                column: "ResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_SocioResponsableId",
                table: "Expedientes",
                column: "SocioResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_TarifaId",
                table: "Expedientes",
                column: "TarifaId");

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_TipoExpedienteId",
                table: "Expedientes",
                column: "TipoExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ClienteId",
                table: "Facturas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_EmpresaId",
                table: "Facturas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_FacturaRectificadaId",
                table: "Facturas",
                column: "FacturaRectificadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_PaisId",
                table: "Facturas",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Paises_IdiomaId",
                table: "Paises",
                column: "IdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_ClienteId",
                table: "Personas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_IdiomaId",
                table: "Personas",
                column: "IdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_UbicacionPrincipalId",
                table: "Personas",
                column: "UbicacionPrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaSector_SectoresId",
                table: "PersonaSector",
                column: "SectoresId");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_EmpresaId",
                table: "Proveedores",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_PersonaId",
                table: "Proveedores",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_TarifaDetalles_CategoriaId",
                table: "TarifaDetalles",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_TarifaDetalles_TarifaId",
                table: "TarifaDetalles",
                column: "TarifaId");

            migrationBuilder.CreateIndex(
                name: "IX_TarifaDetalles_UsuarioId",
                table: "TarifaDetalles",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifas_EmpresaId",
                table: "Tarifas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_PaisId",
                table: "Ubicaciones",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_PersonaId",
                table: "Ubicaciones",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioLogs_UsuarioId",
                table: "UsuarioLogs",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CategoriaId",
                table: "Usuarios",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DepartamentoId",
                table: "Usuarios",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ExpedienteId",
                table: "Usuarios",
                column: "ExpedienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Clientes_ClienteId",
                table: "Expedientes",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Contratos_ContratoId",
                table: "Expedientes",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Usuarios_ResponsableComercialId",
                table: "Expedientes",
                column: "ResponsableComercialId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Usuarios_ResponsableFacturacionId",
                table: "Expedientes",
                column: "ResponsableFacturacionId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Usuarios_ResponsableId",
                table: "Expedientes",
                column: "ResponsableId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Usuarios_SocioResponsableId",
                table: "Expedientes",
                column: "SocioResponsableId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteServicio_Clientes_ClientesId",
                table: "ClienteServicio",
                column: "ClientesId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConceptosEconomicos_Clientes_ClienteId",
                table: "ConceptosEconomicos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConceptosEconomicos_Facturas_FacturaId",
                table: "ConceptosEconomicos",
                column: "FacturaId",
                principalTable: "Facturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConceptosEconomicos_Proveedores_ProveedorId",
                table: "ConceptosEconomicos",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Clientes_ClienteId",
                table: "Contratos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Clientes_ClienteId",
                table: "Facturas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Clientes_ClienteId",
                table: "Personas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Ubicaciones_UbicacionPrincipalId",
                table: "Personas",
                column: "UbicacionPrincipalId",
                principalTable: "Ubicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Expedientes_ExpedienteId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_CaptadorId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_ResponsableComercialId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_ResponsableFacturacionId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Empresa_EmpresaId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarifas_Empresa_EmpresaId",
                table: "Tarifas");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Personas_PersonaId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ubicaciones_Personas_PersonaId",
                table: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "Actuaciones");

            migrationBuilder.DropTable(
                name: "ClienteServicio");

            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "DatosContacto");

            migrationBuilder.DropTable(
                name: "PersonaSector");

            migrationBuilder.DropTable(
                name: "TarifaDetalles");

            migrationBuilder.DropTable(
                name: "UsuarioLogs");

            migrationBuilder.DropTable(
                name: "ConceptosEconomicos");

            migrationBuilder.DropTable(
                name: "TipoActuaciones");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "TipoContactos");

            migrationBuilder.DropTable(
                name: "Sectores");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Expedientes");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "TipoExpediente");

            migrationBuilder.DropTable(
                name: "FormasDePago");

            migrationBuilder.DropTable(
                name: "TipoContratos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "Tarifas");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "Idiomas");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}
