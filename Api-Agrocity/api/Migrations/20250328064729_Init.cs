using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScientificName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrowthCycle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Watering = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HardinessZone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HardinessZoneDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flowers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sun = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fruits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edible = table.Column<bool>(type: "bit", nullable: false),
                    Leaf = table.Column<bool>(type: "bit", nullable: false),
                    LeafColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrowthRate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maintenance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaltTolerant = table.Column<bool>(type: "bit", nullable: false),
                    CareLevel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plants");
        }
    }
}
