using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Auth.Data.Migrations
{
    public partial class AddBidsToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Bids",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ApplicationUserId",
                table: "Bids",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_AspNetUsers_ApplicationUserId",
                table: "Bids",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_AspNetUsers_ApplicationUserId",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_ApplicationUserId",
                table: "Bids");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Bids",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
