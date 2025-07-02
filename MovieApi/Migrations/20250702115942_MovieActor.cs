using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApi.Migrations
{
    /// <inheritdoc />
    public partial class MovieActor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActorId",
                table: "Actors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actors_ActorId",
                table: "Actors",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Actors_ActorId",
                table: "Actors",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Actors_ActorId",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_ActorId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "Actors");
        }
    }
}
