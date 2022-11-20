using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eagle.Services.CouponAPI.Migrations
{
    public partial class DatabaseSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "Code", "DiscountAmount" },
                values: new object[] { 1, "10OFF", 10.0 });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "Code", "DiscountAmount" },
                values: new object[] { 2, "20OFF", 20.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 2);
        }
    }
}
