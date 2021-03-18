using Microsoft.EntityFrameworkCore.Migrations;

namespace ApmDbBackupManager.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackupSchedules_DriveUsers_DriveUserId",
                table: "BackupSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "DriveUserId",
                table: "BackupSchedules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_BackupSchedules_DriveUsers_DriveUserId",
                table: "BackupSchedules",
                column: "DriveUserId",
                principalTable: "DriveUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackupSchedules_DriveUsers_DriveUserId",
                table: "BackupSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "DriveUserId",
                table: "BackupSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BackupSchedules_DriveUsers_DriveUserId",
                table: "BackupSchedules",
                column: "DriveUserId",
                principalTable: "DriveUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
