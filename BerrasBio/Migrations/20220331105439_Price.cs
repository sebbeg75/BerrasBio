using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerrasBio.Migrations
{
    public partial class Price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MoviePrice",
                table: "Movie",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoviePrice",
                table: "Movie");
        }
    }
}
