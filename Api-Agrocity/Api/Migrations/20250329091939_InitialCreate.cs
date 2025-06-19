using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    calendarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    calendarDate = table.Column<DateOnly>(type: "date", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Calendar__EE5496F63661BE91", x => x.calendarId);
                });

            migrationBuilder.CreateTable(
                name: "Pests",
                columns: table => new
                {
                    pestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    commonName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    scientificName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    solution = table.Column<string>(type: "text", nullable: true),
                    host = table.Column<string>(type: "text", nullable: true),
                    imageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pests__7F10C1DDE81FD7F1", x => x.pestId);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    photoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    photoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Photos__547C322DFF40E13E", x => x.photoId);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    plantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plantName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    scientificName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    growthCycle = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    wateringFrequency = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    hardinessZone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    hardinessZoneDescription = table.Column<string>(type: "text", nullable: true),
                    flowerDetails = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    sunExposure = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    fruitDetails = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    isEdible = table.Column<bool>(type: "bit", nullable: true),
                    hasLeaves = table.Column<bool>(type: "bit", nullable: true),
                    leafColor = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    growthRate = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    maintenanceLevel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    isSaltTolerant = table.Column<bool>(type: "bit", nullable: true),
                    careLevel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    lastModified = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Plants__870532B032979DAC", x => x.plantId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    firstName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    surname = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    age = table.Column<int>(type: "int", nullable: true),
                    telephone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__CB9A1CFF12AAA002", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "PlantPests",
                columns: table => new
                {
                    plantId = table.Column<int>(type: "int", nullable: false),
                    pestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PlantPes__40F43EAD78645CA7", x => new { x.plantId, x.pestId });
                    table.ForeignKey(
                        name: "FK__PlantPest__pestI__59063A47",
                        column: x => x.pestId,
                        principalTable: "Pests",
                        principalColumn: "pestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__PlantPest__plant__5812160E",
                        column: x => x.plantId,
                        principalTable: "Plants",
                        principalColumn: "plantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gardens",
                columns: table => new
                {
                    gardenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Gardens__C5BCE574A5896009", x => x.gardenId);
                    table.ForeignKey(
                        name: "FK__Gardens__userId__534D60F1",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    notificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__4BA5CEA97C271245", x => x.notificationId);
                    table.ForeignKey(
                        name: "FK__Notificat__userI__6E01572D",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    publicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    commentId = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    likes = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Publicat__883D5CDF2EDC98F4", x => x.publicationId);
                    table.ForeignKey(
                        name: "FK__Publicati__userI__656C112C",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    reminderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    plantId = table.Column<int>(type: "int", nullable: true),
                    reminderDate = table.Column<DateOnly>(type: "date", nullable: true),
                    typeReminder = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reminder__09DAAAE369C1E1AB", x => x.reminderId);
                    table.ForeignKey(
                        name: "FK__Reminders__plant__5CD6CB2B",
                        column: x => x.plantId,
                        principalTable: "Plants",
                        principalColumn: "plantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Reminders__userI__5BE2A6F2",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    commentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    publicationId = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comments__CDDE919D049988A8", x => x.commentId);
                    table.ForeignKey(
                        name: "FK__Comments__public__6A30C649",
                        column: x => x.publicationId,
                        principalTable: "Publications",
                        principalColumn: "publicationId");
                    table.ForeignKey(
                        name: "FK__Comments__userId__693CA210",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_publicationId",
                table: "Comments",
                column: "publicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_userId",
                table: "Comments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Gardens_userId",
                table: "Gardens",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_userId",
                table: "Notifications",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantPests_pestId",
                table: "PlantPests",
                column: "pestId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_userId",
                table: "Publications",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_plantId",
                table: "Reminders",
                column: "plantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_userId",
                table: "Reminders",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__AB6E616439D41849",
                table: "Users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calendar");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Gardens");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "PlantPests");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "Pests");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
