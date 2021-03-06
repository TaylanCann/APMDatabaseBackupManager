// <auto-generated />
using System;
using ApmDbBackupManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApmDbBackupManager.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("ApmDbBackupManager.Models.BackupSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BackupName")
                        .HasColumnType("TEXT");

                    b.Property<int>("BackupScheme")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DaysAdd")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DaysAddTerm")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DbName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DiffTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DriveUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FtpId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FtpThingId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDiffBackup")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDrive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFtp")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLocal")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LocalLocation")
                        .HasColumnType("TEXT");

                    b.Property<int>("MonthAdd")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MonthAddTerm")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DriveUserId");

                    b.HasIndex("FtpThingId");

                    b.ToTable("BackupSchedules");
                });

            modelBuilder.Entity("ApmDbBackupManager.Models.DriveUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("User")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DriveUsers");
                });

            modelBuilder.Entity("ApmDbBackupManager.Models.FtpThing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FtpLocation")
                        .HasColumnType("TEXT");

                    b.Property<string>("FtpPassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("FtpUserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FtpThings");
                });

            modelBuilder.Entity("ApmDbBackupManager.Models.BackupSchedule", b =>
                {
                    b.HasOne("ApmDbBackupManager.Models.DriveUser", "DriveUser")
                        .WithMany("BackupSchedules")
                        .HasForeignKey("DriveUserId");

                    b.HasOne("ApmDbBackupManager.Models.FtpThing", "FtpThing")
                        .WithMany("backupSchedules")
                        .HasForeignKey("FtpThingId");

                    b.Navigation("DriveUser");

                    b.Navigation("FtpThing");
                });

            modelBuilder.Entity("ApmDbBackupManager.Models.DriveUser", b =>
                {
                    b.Navigation("BackupSchedules");
                });

            modelBuilder.Entity("ApmDbBackupManager.Models.FtpThing", b =>
                {
                    b.Navigation("backupSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
