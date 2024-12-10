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
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ManufactererName = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    AreaName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SlaveDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlaveDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlaveDevices_ModbusNetworks_Id",
                        column: x => x.Id,
                        principalTable: "ModbusNetworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    IsMeasurement = table.Column<bool>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    SamplingPeriod = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    ModbusAddress = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variable_SlaveDevices_Id",
                        column: x => x.Id,
                        principalTable: "SlaveDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Variable_Unit_Id",
                        column: x => x.Id,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalogicVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", precision: 18, scale: 2, nullable: false),
                    UnitID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalogicVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalogicVariables_Variable_Id",
                        column: x => x.Id,
                        principalTable: "Variable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<short>(type: "INTEGER", maxLength: 1024, nullable: false),
                    UnitID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalVariables_Variable_Id",
                        column: x => x.Id,
                        principalTable: "Variable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalogicVariables");

            migrationBuilder.DropTable(
                name: "DigitalVariables");

            migrationBuilder.DropTable(
                name: "Variable");

            migrationBuilder.DropTable(
                name: "SlaveDevices");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "ModbusNetworks");
        }
    }
}
