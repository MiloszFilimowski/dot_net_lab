using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "961b8a3b-e78e-49f2-832a-6caf19e02223", "00544765-d619-4440-9f5e-b7db8562c731" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "961b8a3b-e78e-49f2-832a-6caf19e02223");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00544765-d619-4440-9f5e-b7db8562c731");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "968dc826-34c5-497d-8a63-596faf7ae8e3", "968dc826-34c5-497d-8a63-596faf7ae8e3", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c8faf4ab-2587-41d0-b081-4f4ad08224eb", 0, "4841622b-11fa-4d72-a8d0-32e6368776d2", "adminuser@wsei.edu.pl", true, false, null, "ADMINUSER@WSEI.EDU.PL", "ADMINUSER@WSEI.EDU.PL", "AQAAAAIAAYagAAAAENEw2JUOZ/BlcftZnQ2cRJvf1Hn9FyR/50VsnyDIQk98PLS4idifPp/Twuoj/rVxvg==", null, false, "b3ff6b43-26b4-47d8-bd13-982dcd8bb0e3", false, "adminuser@wsei.edu.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "968dc826-34c5-497d-8a63-596faf7ae8e3", "c8faf4ab-2587-41d0-b081-4f4ad08224eb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "968dc826-34c5-497d-8a63-596faf7ae8e3", "c8faf4ab-2587-41d0-b081-4f4ad08224eb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "968dc826-34c5-497d-8a63-596faf7ae8e3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c8faf4ab-2587-41d0-b081-4f4ad08224eb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "961b8a3b-e78e-49f2-832a-6caf19e02223", "961b8a3b-e78e-49f2-832a-6caf19e02223", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "00544765-d619-4440-9f5e-b7db8562c731", 0, "749a8b89-ad53-4193-988e-4172e9df4e53", "adminuser@wsei.edu.pl", true, false, null, "ADMINUSER@WSEI.EDU.PL", "ADMINUSER@WSEI.EDU.PL", "AQAAAAIAAYagAAAAECMQRYs7S1IrlEMjHayf1xVKQ+ByF6Wsv7fVlAfKMarHiKnu4DraiEwEGBZAayX/Ww==", null, false, "03effdfb-ac49-43e6-bd39-320aae835976", false, "adminuser@wsei.edu.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "961b8a3b-e78e-49f2-832a-6caf19e02223", "00544765-d619-4440-9f5e-b7db8562c731" });
        }
    }
}
