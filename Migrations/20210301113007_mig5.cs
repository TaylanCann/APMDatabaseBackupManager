using Microsoft.EntityFrameworkCore.Migrations;

namespace ApmDbBackupManager.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FtpLocation",
                table: "BackupSchedules");

            migrationBuilder.DropColumn(
                name: "FtpPassword",
                table: "BackupSchedules");

            migrationBuilder.DropColumn(
                name: "FtpUserName",
                table: "BackupSchedules");

            migrationBuilder.AddColumn<int>(
                name: "FtpId",
                table: "BackupSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FtpThingId",
                table: "BackupSchedules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FtpThings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FtpLocation = table.Column<string>(type: "TEXT", nullable: true),
                    FtpPassword = table.Column<string>(type: "TEXT", nullable: true),
                    FtpUserName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FtpThings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackupSchedules_FtpThingId",
                table: "BackupSchedules",
                column: "FtpThingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BackupSchedules_FtpThings_FtpThingId",
                table: "BackupSchedules",
                column: "FtpThingId",
                principalTable: "FtpThings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackupSchedules_FtpThings_FtpThingId",
                table: "BackupSchedules");

            migrationBuilder.DropTable(
                name: "FtpThings");

            migrationBuilder.DropIndex(
                name: "IX_BackupSchedules_FtpThingId",
                table: "BackupSchedules");

            migrationBuilder.DropColumn(
                name: "FtpId",
                table: "BackupSchedules");

            migrationBuilder.DropColumn(
                name: "FtpThingId",
                table: "BackupSchedules");

            migrationBuilder.AddColumn<string>(
                name: "FtpLocation",
                table: "BackupSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpPassword",
                table: "BackupSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpUserName",
                table: "BackupSchedules",
                type: "TEXT",
                nullable: true);
        }
    }
}
