using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solution.Database.Migrations
{
    /// <inheritdoc />
    public partial class CarConversion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename Motorcycle table to Car
            migrationBuilder.RenameTable(
                name: "Motorcycle",
                newName: "Car");

            // Rename Cylinders column to NumberOfDoors
            migrationBuilder.RenameColumn(
                name: "Cylinders",
                table: "Car",
                newName: "NumberOfDoors");

            // Update foreign key names if needed
            migrationBuilder.RenameIndex(
                name: "IX_Motorcycle_ManufacturerId",
                table: "Car",
                newName: "IX_Car_ManufacturerId");

            migrationBuilder.RenameIndex(
                name: "IX_Motorcycle_TypeId", 
                table: "Car",
                newName: "IX_Car_TypeId");

            // Update foreign key constraint names
            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycle_Manufacturer_ManufacturerId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycle_Type_TypeId",
                table: "Car");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Manufacturer_ManufacturerId",
                table: "Car",
                column: "ManufacturerId",
                principalTable: "Manufacturer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Type_TypeId",
                table: "Car",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Manufacturer_ManufacturerId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_Type_TypeId",
                table: "Car");

            // Revert foreign key names
            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycle_Manufacturer_ManufacturerId",
                table: "Car",
                column: "ManufacturerId",
                principalTable: "Manufacturer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycle_Type_TypeId",
                table: "Car",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // Revert index names
            migrationBuilder.RenameIndex(
                name: "IX_Car_ManufacturerId",
                table: "Car",
                newName: "IX_Motorcycle_ManufacturerId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_TypeId",
                table: "Car", 
                newName: "IX_Motorcycle_TypeId");

            // Revert column name
            migrationBuilder.RenameColumn(
                name: "NumberOfDoors",
                table: "Car",
                newName: "Cylinders");

            // Revert table name
            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Motorcycle");
        }
    }
}
