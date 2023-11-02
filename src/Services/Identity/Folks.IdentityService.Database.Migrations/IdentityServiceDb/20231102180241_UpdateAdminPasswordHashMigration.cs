using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Folks.IdentityService.Database.Migrations.IdentityServiceDb
{
    /// <inheritdoc />
    public partial class UpdateAdminPasswordHashMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a6beb063-dfc9-4cfd-82d2-97dc027fe34d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8437f17d-035f-4dc2-aa2c-806171e62e2f", "AQAAAAIAAYagAAAAEK51QDTSOxy+Z7fCIPbmK/nofUbheKUZQm7DqcuysMWsEUEhc7C5HRi2HjaPnyChBQ==", "5b626cec-9252-4b54-a55d-fea96cf0fc4f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a6beb063-dfc9-4cfd-82d2-97dc027fe34d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2576aad2-7dd2-4d39-b3fd-b366d674ee81", null, "0bb012fc-c453-42ae-83f7-31652a9f83b7" });
        }
    }
}
