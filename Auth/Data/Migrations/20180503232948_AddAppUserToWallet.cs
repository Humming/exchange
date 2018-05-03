using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Auth.Data.Migrations
{
    public partial class AddAppUserToWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Wallets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_ApplicationUserId",
                table: "Wallets",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_AspNetUsers_ApplicationUserId",
                table: "Wallets",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_AspNetUsers_ApplicationUserId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_ApplicationUserId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Wallets");
        }
    }
}
