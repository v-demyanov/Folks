using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Folks.IdentityService.Database.Migrations.IdentityServiceDb
{
    /// <inheritdoc />
    public partial class UpdateNormalizedFieldsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a6beb063-dfc9-4cfd-82d2-97dc027fe34d",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91af011d-7d03-41b2-ae12-d620e59ba868", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEPpaE8R8ov3c3xPZYbKElSFUr+Z3NNzJPRzW6qehyC3KQk9QzLO9FB67GxwVz9olrA==", "384875d1-14ba-40bb-b426-c572e7b36a86" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a6beb063-dfc9-4cfd-82d2-97dc027fe34d",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8437f17d-035f-4dc2-aa2c-806171e62e2f", null, null, "AQAAAAIAAYagAAAAEK51QDTSOxy+Z7fCIPbmK/nofUbheKUZQm7DqcuysMWsEUEhc7C5HRi2HjaPnyChBQ==", "5b626cec-9252-4b54-a55d-fea96cf0fc4f" });
        }
    }
}
