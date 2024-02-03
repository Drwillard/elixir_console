using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElixirProject_Willard.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Text", "IsActive" },
                values: new object[,]
                {
                    { 1, "In what city were you born?", true },
                    { 2, "What is the name of your favorite pet?", true },
                    { 3, "What is your mother's maiden name?", true },
                    { 4, "What high school did you attend?", true },
                    { 5, "What was the mascot of your high school?", true },
                    { 6, "What was the make of your first car?", true },
                    { 7, "What was your favorite toy as a child?", true },
                    { 8, "Where did you meet your spouse?", true },
                    { 9, "What is your favorite meal?", true },
                    { 10, "Who is your favorite actor / actress?", true },
                    { 11, "What is your favorite album?", true },

                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
