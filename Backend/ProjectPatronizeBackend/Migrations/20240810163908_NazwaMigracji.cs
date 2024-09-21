using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectPatronizeBackend.Migrations
{
    /// <inheritdoc />
    public partial class NazwaMigracji : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripePlanId",
                table: "Tiers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripePlanId",
                table: "Tiers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
