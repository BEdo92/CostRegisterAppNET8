using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Incomes",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Costs",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "CostPlans",
                newName: "Total");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Incomes",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Costs",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "CostPlans",
                newName: "Amount");
        }
    }
}
