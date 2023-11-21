using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPI_ProgramacionIII.Migrations
{
    /// <inheritdoc />
    public partial class cambiandoAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "LastName", "Name", "UserName" },
                values: new object[] { "lnovo@gmail.com", "Novo", "Luisina", "lnovo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "LastName", "Name", "UserName" },
                values: new object[] { "ngomez@gmail.com", "Gomez", "Nicolas", "ngomez_admin" });
        }
    }
}
