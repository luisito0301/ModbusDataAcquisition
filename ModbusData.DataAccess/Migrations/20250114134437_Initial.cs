using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModbusData.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModbusNetworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MasterIpAddress = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModbusNetworks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ManufactererName = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    AreaName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SlaveDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    ModbusNetworkId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlaveDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlaveDevices_ModbusNetworks_ModbusNetworkId",
                        column: x => x.ModbusNetworkId,
                        principalTable: "ModbusNetworks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Variables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    IsMeasurement = table.Column<bool>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    SamplingPeriod = table.Column<string>(type: "TEXT", nullable: false),
                    ModbusAddress = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SlaveDeviceId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UnitId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variables_SlaveDevices_SlaveDeviceId",
                        column: x => x.SlaveDeviceId,
                        principalTable: "SlaveDevices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Variables_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Variables_Units_UnitId1",
                        column: x => x.UnitId1,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnalogicVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalogicVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalogicVariables_Variables_Id",
                        column: x => x.Id,
                        principalTable: "Variables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<short>(type: "INTEGER", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalVariables_Variables_Id",
                        column: x => x.Id,
                        principalTable: "Variables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlaveDevices_ModbusNetworkId",
                table: "SlaveDevices",
                column: "ModbusNetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_SlaveDeviceId",
                table: "Variables",
                column: "SlaveDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_UnitId",
                table: "Variables",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_UnitId1",
                table: "Variables",
                column: "UnitId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalogicVariables");

            migrationBuilder.DropTable(
                name: "DigitalVariables");

            migrationBuilder.DropTable(
                name: "Variables");

            migrationBuilder.DropTable(
                name: "SlaveDevices");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "ModbusNetworks");
        }
    }
}
