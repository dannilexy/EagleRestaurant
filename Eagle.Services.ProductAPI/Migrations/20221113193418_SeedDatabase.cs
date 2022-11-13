using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eagle.Services.ProductAPI.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Appetizer", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.britannica.com%2Ftopic%2Fpastry&psig=AOvVaw1E5BZS6FnObHOnobWL18t0&ust=1668454339755000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCID9-Pvyq_sCFQAAAAAdAAAAABAD", "Samosa", 15.0 },
                    { 2, "Appetizer", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.megachicken.com.ng%2Fproduct%2Fmega-pastries%2F&psig=AOvVaw1E5BZS6FnObHOnobWL18t0&ust=1668454339755000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCID9-Pvyq_sCFQAAAAAdAAAAABAO", "Paneer Tikka", 13.99 },
                    { 3, "Dessert", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.britannica.com%2Ftopic%2Fpastry&psig=AOvVaw1E5BZS6FnObHOnobWL18t0&ust=1668454339755000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCID9-Pvyq_sCFQAAAAAdAAAAABAD", "Sweet Pie", 10.99 },
                    { 4, "Entree", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DBUVY3MUJ1ws&psig=AOvVaw1E5BZS6FnObHOnobWL18t0&ust=1668454339755000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCID9-Pvyq_sCFQAAAAAdAAAAABAJ", "Pav Bhaji", 15.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);
        }
    }
}
