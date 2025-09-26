using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPark.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCliente",
                columns: table => new
                {
                    idCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    documento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblClien__885457EED54D88BF", x => x.idCliente);
                });

            migrationBuilder.CreateTable(
                name: "tblRol",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblRol__3C872F763E176C2C", x => x.idRol);
                });

            migrationBuilder.CreateTable(
                name: "tblTipoVehiculo",
                columns: table => new
                {
                    idTipoVehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblTipoV__429A3B81E89B4C0B", x => x.idTipoVehiculo);
                });

            migrationBuilder.CreateTable(
                name: "tblEmpleado",
                columns: table => new
                {
                    idEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    documento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    idRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblEmple__5295297C75A101A7", x => x.idEmpleado);
                    table.ForeignKey(
                        name: "FK_Rol_Empleado",
                        column: x => x.idRol,
                        principalTable: "tblRol",
                        principalColumn: "idRol");
                });

            migrationBuilder.CreateTable(
                name: "tblBahia",
                columns: table => new
                {
                    idBahia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ubicacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    idTipoVehiculo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblBahia__D875E662473A726D", x => x.idBahia);
                    table.ForeignKey(
                        name: "FK_Bahia_Tipo",
                        column: x => x.idTipoVehiculo,
                        principalTable: "tblTipoVehiculo",
                        principalColumn: "idTipoVehiculo");
                });

            migrationBuilder.CreateTable(
                name: "tblTarifa",
                columns: table => new
                {
                    idTarifa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idTipoVehiculo = table.Column<int>(type: "int", nullable: false),
                    valorHora = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblTarif__550711E1484930E6", x => x.idTarifa);
                    table.ForeignKey(
                        name: "FK_Tarifa_Tipo",
                        column: x => x.idTipoVehiculo,
                        principalTable: "tblTipoVehiculo",
                        principalColumn: "idTipoVehiculo");
                });

            migrationBuilder.CreateTable(
                name: "tblVehiculo",
                columns: table => new
                {
                    placa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    idTipoVehiculo = table.Column<int>(type: "int", nullable: false),
                    color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblVehic__0C05742446CA4711", x => x.placa);
                    table.ForeignKey(
                        name: "FK_Vehiculo_Tipo",
                        column: x => x.idTipoVehiculo,
                        principalTable: "tblTipoVehiculo",
                        principalColumn: "idTipoVehiculo");
                });

            migrationBuilder.CreateTable(
                name: "tblUsuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    contrasena = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idEmpleado = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblUsuar__645723A68F139248", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Empleado",
                        column: x => x.idEmpleado,
                        principalTable: "tblEmpleado",
                        principalColumn: "idEmpleado");
                });

            migrationBuilder.CreateTable(
                name: "tblTicketEntrada",
                columns: table => new
                {
                    idTicket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaHoraEntrada = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    placa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    idCliente = table.Column<int>(type: "int", nullable: false),
                    idBahia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblTicke__22B1456FAB399D60", x => x.idTicket);
                    table.ForeignKey(
                        name: "FK_Ticket_Bahia",
                        column: x => x.idBahia,
                        principalTable: "tblBahia",
                        principalColumn: "idBahia");
                    table.ForeignKey(
                        name: "FK_Ticket_Cliente",
                        column: x => x.idCliente,
                        principalTable: "tblCliente",
                        principalColumn: "idCliente");
                    table.ForeignKey(
                        name: "FK_Ticket_Vehiculo",
                        column: x => x.placa,
                        principalTable: "tblVehiculo",
                        principalColumn: "placa");
                });

            migrationBuilder.CreateTable(
                name: "tblFactura",
                columns: table => new
                {
                    idFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaHoraSalida = table.Column<DateTime>(type: "datetime", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    idTicket = table.Column<int>(type: "int", nullable: false),
                    idEmpleado = table.Column<int>(type: "int", nullable: false),
                    idTarifa = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblFactu__3CD5687EC8140890", x => x.idFactura);
                    table.ForeignKey(
                        name: "FK_Factura_Empleado",
                        column: x => x.idEmpleado,
                        principalTable: "tblEmpleado",
                        principalColumn: "idEmpleado");
                    table.ForeignKey(
                        name: "FK_Factura_Tarifa",
                        column: x => x.idTarifa,
                        principalTable: "tblTarifa",
                        principalColumn: "idTarifa");
                    table.ForeignKey(
                        name: "FK_Factura_Ticket",
                        column: x => x.idTicket,
                        principalTable: "tblTicketEntrada",
                        principalColumn: "idTicket");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBahia_idTipoVehiculo",
                table: "tblBahia",
                column: "idTipoVehiculo");

            migrationBuilder.CreateIndex(
                name: "UQ__tblClien__A25B3E61CB77FCBD",
                table: "tblCliente",
                column: "documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblEmpleado_idRol",
                table: "tblEmpleado",
                column: "idRol");

            migrationBuilder.CreateIndex(
                name: "UQ__tblEmple__A25B3E61E2847EB6",
                table: "tblEmpleado",
                column: "documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblFactura_idEmpleado",
                table: "tblFactura",
                column: "idEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_tblFactura_idTarifa",
                table: "tblFactura",
                column: "idTarifa");

            migrationBuilder.CreateIndex(
                name: "IX_tblFactura_idTicket",
                table: "tblFactura",
                column: "idTicket");

            migrationBuilder.CreateIndex(
                name: "IX_tblTarifa_idTipoVehiculo",
                table: "tblTarifa",
                column: "idTipoVehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_tblTicketEntrada_idBahia",
                table: "tblTicketEntrada",
                column: "idBahia");

            migrationBuilder.CreateIndex(
                name: "IX_tblTicketEntrada_idCliente",
                table: "tblTicketEntrada",
                column: "idCliente");

            migrationBuilder.CreateIndex(
                name: "IX_tblTicketEntrada_placa",
                table: "tblTicketEntrada",
                column: "placa");

            migrationBuilder.CreateIndex(
                name: "UQ__tblTipoV__72AFBCC6388796A6",
                table: "tblTipoVehiculo",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblUsuario_idEmpleado",
                table: "tblUsuario",
                column: "idEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_tblVehiculo_idTipoVehiculo",
                table: "tblVehiculo",
                column: "idTipoVehiculo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFactura");

            migrationBuilder.DropTable(
                name: "tblUsuario");

            migrationBuilder.DropTable(
                name: "tblTarifa");

            migrationBuilder.DropTable(
                name: "tblTicketEntrada");

            migrationBuilder.DropTable(
                name: "tblEmpleado");

            migrationBuilder.DropTable(
                name: "tblBahia");

            migrationBuilder.DropTable(
                name: "tblCliente");

            migrationBuilder.DropTable(
                name: "tblVehiculo");

            migrationBuilder.DropTable(
                name: "tblRol");

            migrationBuilder.DropTable(
                name: "tblTipoVehiculo");
        }
    }
}
