using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace practica01.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rental_orders",
                columns: table => new
                {
                    rental_order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    customer = table.Column<string>(type: "varchar(90)", maxLength: 90, nullable: false),
                    vehicles_id = table.Column<int>(type: "int", nullable: false),
                    plate = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    requested_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    address_street = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    address_city = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    address_postal_code = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_rental_orders", x => x.rental_order_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_rental_orders_customer_vehicles_id",
                table: "rental_orders",
                columns: new[] { "customer", "vehicles_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_rental_orders_plate",
                table: "rental_orders",
                column: "plate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rental_orders");
        }
    }
}
