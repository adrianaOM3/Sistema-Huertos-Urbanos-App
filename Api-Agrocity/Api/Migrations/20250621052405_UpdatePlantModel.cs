using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlantModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "careLevel",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "flowerDetails",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "fruitDetails",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "growthCycle",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "growthRate",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "hardinessZone",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "hasLeaves",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "isEdible",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "isSaltTolerant",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "lastModified",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "leafColor",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "maintenanceLevel",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "scientificName",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "sunExposure",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "wateringFrequency",
                table: "Plants");

            migrationBuilder.RenameColumn(
                name: "hardinessZoneDescription",
                table: "Plants",
                newName: "imageUrl");

            migrationBuilder.AddColumn<int>(
                name: "PlantId",
                table: "Gardens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gardens_PlantId",
                table: "Gardens",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gardens_Plants_PlantId",
                table: "Gardens",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "plantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gardens_Plants_PlantId",
                table: "Gardens");

            migrationBuilder.DropIndex(
                name: "IX_Gardens_PlantId",
                table: "Gardens");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Gardens");

            migrationBuilder.RenameColumn(
                name: "imageUrl",
                table: "Plants",
                newName: "hardinessZoneDescription");

            migrationBuilder.AddColumn<string>(
                name: "careLevel",
                table: "Plants",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "Plants",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<string>(
                name: "flowerDetails",
                table: "Plants",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fruitDetails",
                table: "Plants",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "growthCycle",
                table: "Plants",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "growthRate",
                table: "Plants",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hardinessZone",
                table: "Plants",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "hasLeaves",
                table: "Plants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isEdible",
                table: "Plants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSaltTolerant",
                table: "Plants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastModified",
                table: "Plants",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<string>(
                name: "leafColor",
                table: "Plants",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "maintenanceLevel",
                table: "Plants",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "scientificName",
                table: "Plants",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sunExposure",
                table: "Plants",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wateringFrequency",
                table: "Plants",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }
    }
}
