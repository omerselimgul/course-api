using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace hbo.webapi.Migrations
{
    /// <inheritdoc />
    public partial class beyza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "215c9814-f8ee-481e-86a9-a55463fe778d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7a9f817-8477-4aec-b22f-c4177e5e4f82");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c601f35b-8ab1-4c3e-8eb8-7c641eef514f");

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "409522e1-6234-4b15-af35-9534c6cbc393", "7412d9a8-660b-4247-8bb3-d43a8f7080ad", "IdentityUserRole", "Educator", "EDUCATOR" },
                    { "b82f9d01-c76d-42d1-93cf-377d941fc1ea", "0d982692-3240-4f6c-8b72-0912a6204408", "IdentityUserRole", "Admin", "ADMIN" },
                    { "f12a3aed-f2a6-4f7f-b445-010c5a911567", "2d22200d-9b7e-4026-8084-e41566578e5b", "IdentityUserRole", "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "670f9181-3dae-4ca6-a788-4f0d2039e922", 0, "d6eaf3bb-df51-4a87-95ae-ad22432675b2", null, false, false, null, null, null, "AQAAAAEAACcQAAAAECnltgMmJx0X45oYnUm29lkey2EWkf2oV4+6FxYctDZO6C7BkWl0DPK8fvZRa2j4RA==", null, false, "d37fe7d9-b81e-4b63-8e0e-78d4c43913cd", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "409522e1-6234-4b15-af35-9534c6cbc393");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b82f9d01-c76d-42d1-93cf-377d941fc1ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f12a3aed-f2a6-4f7f-b445-010c5a911567");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "215c9814-f8ee-481e-86a9-a55463fe778d", "dfdb111c-5bd4-47f6-9876-a97e90401f76", "IdentityUserRole", "Student", "STUDENT" },
                    { "b7a9f817-8477-4aec-b22f-c4177e5e4f82", "c84219c0-4394-4a6b-9b63-486d7eabce0c", "IdentityUserRole", "Admin", "ADMIN" },
                    { "c601f35b-8ab1-4c3e-8eb8-7c641eef514f", "61786f9d-b3bd-425a-9f94-8dce925c0367", "IdentityUserRole", "Educator", "EDUCATOR" }
                });
        }
    }
}
