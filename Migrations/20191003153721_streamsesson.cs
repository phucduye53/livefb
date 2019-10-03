using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace liveBot.Migrations
{
    public partial class streamsesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StreamSessonId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StreamSessons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    StreamId = table.Column<string>(nullable: true),
                    StreamUrl = table.Column<string>(nullable: true),
                    StreamTitle = table.Column<string>(nullable: true),
                    SecureStreamUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamSessons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StreamSessonId",
                table: "Comments",
                column: "StreamSessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_StreamSessons_StreamSessonId",
                table: "Comments",
                column: "StreamSessonId",
                principalTable: "StreamSessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_StreamSessons_StreamSessonId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "StreamSessons");

            migrationBuilder.DropIndex(
                name: "IX_Comments_StreamSessonId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "StreamSessonId",
                table: "Comments");
        }
    }
}
