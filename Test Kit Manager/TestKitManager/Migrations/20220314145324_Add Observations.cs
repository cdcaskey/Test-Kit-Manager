using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestKitManager.Migrations
{
    public partial class AddObservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Observations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    ThirdPartyIssue = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineObservation",
                columns: table => new
                {
                    MachinesId = table.Column<int>(type: "INTEGER", nullable: false),
                    ObservationsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineObservation", x => new { x.MachinesId, x.ObservationsId });
                    table.ForeignKey(
                        name: "FK_MachineObservation_Machines_MachinesId",
                        column: x => x.MachinesId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineObservation_Observations_ObservationsId",
                        column: x => x.ObservationsId,
                        principalTable: "Observations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservationService",
                columns: table => new
                {
                    ObservationsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ServicesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationService", x => new { x.ObservationsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ObservationService_Observations_ObservationsId",
                        column: x => x.ObservationsId,
                        principalTable: "Observations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObservationService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineObservation_ObservationsId",
                table: "MachineObservation",
                column: "ObservationsId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservationService_ServicesId",
                table: "ObservationService",
                column: "ServicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineObservation");

            migrationBuilder.DropTable(
                name: "ObservationService");

            migrationBuilder.DropTable(
                name: "Observations");
        }
    }
}
