using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Auth.Data.Migrations
{
    public partial class AddDepositToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepositId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepositId",
                table: "AspNetUsers",
                column: "DepositId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Deposits_DepositId",
                table: "AspNetUsers",
                column: "DepositId",
                principalTable: "Deposits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Deposits_DepositId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DepositId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DepositId",
                table: "AspNetUsers");
        }
    }
}
