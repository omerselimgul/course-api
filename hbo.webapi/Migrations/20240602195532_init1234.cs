using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace hbo.webapi.Migrations
{
    /// <inheritdoc />
    public partial class init1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "409522e1-6234-4b15-af35-9534c6cbc393");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "b82f9d01-c76d-42d1-93cf-377d941fc1ea");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "f12a3aed-f2a6-4f7f-b445-010c5a911567");

            //migrationBuilder.DeleteData(
            //    table: "IdentityUser",
            //    keyColumn: "Id",
            //    keyValue: "670f9181-3dae-4ca6-a788-4f0d2039e922");

            migrationBuilder.AddColumn<string>(
                name: "CourseDocument",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
            //    values: new object[,]
            //    {
            //        { "040e3354-ff52-4402-96e1-09ef5fe01525", "cb159329-7a13-434e-9ba5-23879b14f4e4", "IdentityUserRole", "Educator", "EDUCATOR" },
            //        { "b2cd67a2-d431-4432-b85b-4a534d89df30", "6428b93e-697b-4af8-8724-332a76152e09", "IdentityUserRole", "Admin", "ADMIN" },
            //        { "bf340a39-b078-4d5f-a7f7-7045eeff6586", "103d9597-5de4-441c-b3cc-4f53946e84cd", "IdentityUserRole", "Student", "STUDENT" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "IdentityUser",
            //    columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
            //    values: new object[] { "7ae9a37b-cfff-4ba9-a617-0f3f4ea3cfd7", 0, "1884977e-c0c4-4762-b12b-98e7eb9b3e80", null, false, false, null, null, null, "AQAAAAEAACcQAAAAEFb4Y43NjBBDDArHTreQbN4AGKNRjRJ7VDVJIWrrQXbkGKQkXc9swJxjkFtXTzr1KQ==", null, false, "d8640e4d-b226-47e0-9dae-386b5f111845", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "040e3354-ff52-4402-96e1-09ef5fe01525");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "b2cd67a2-d431-4432-b85b-4a534d89df30");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "bf340a39-b078-4d5f-a7f7-7045eeff6586");

            //migrationBuilder.DeleteData(
            //    table: "IdentityUser",
            //    keyColumn: "Id",
            //    keyValue: "7ae9a37b-cfff-4ba9-a617-0f3f4ea3cfd7");

            migrationBuilder.DropColumn(
                name: "CourseDocument",
                table: "Courses");

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
            //    values: new object[,]
            //    {
            //        { "409522e1-6234-4b15-af35-9534c6cbc393", "7412d9a8-660b-4247-8bb3-d43a8f7080ad", "IdentityUserRole", "Educator", "EDUCATOR" },
            //        { "b82f9d01-c76d-42d1-93cf-377d941fc1ea", "0d982692-3240-4f6c-8b72-0912a6204408", "IdentityUserRole", "Admin", "ADMIN" },
            //        { "f12a3aed-f2a6-4f7f-b445-010c5a911567", "2d22200d-9b7e-4026-8084-e41566578e5b", "IdentityUserRole", "Student", "STUDENT" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "IdentityUser",
            //    columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                //values: new object[] { "670f9181-3dae-4ca6-a788-4f0d2039e922", 0, "d6eaf3bb-df51-4a87-95ae-ad22432675b2", null, false, false, null, null, null, "AQAAAAEAACcQAAAAECnltgMmJx0X45oYnUm29lkey2EWkf2oV4+6FxYctDZO6C7BkWl0DPK8fvZRa2j4RA==", null, false, "d37fe7d9-b81e-4b63-8e0e-78d4c43913cd", false, "admin" });
        }
    }
}
