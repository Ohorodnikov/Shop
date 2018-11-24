using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Shop.Migrations
{
    public partial class ReplyTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplyToId",
                table: "ForumMessages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ForumMessages_ReplyToId",
                table: "ForumMessages",
                column: "ReplyToId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessages_ForumMessages_ReplyToId",
                table: "ForumMessages",
                column: "ReplyToId",
                principalTable: "ForumMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessages_ForumMessages_ReplyToId",
                table: "ForumMessages");

            migrationBuilder.DropIndex(
                name: "IX_ForumMessages_ReplyToId",
                table: "ForumMessages");

            migrationBuilder.DropColumn(
                name: "ReplyToId",
                table: "ForumMessages");
        }
    }
}
