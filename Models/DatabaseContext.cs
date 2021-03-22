using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApmDbBackupManager.Models
{
    class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            //Çalışma sırasında veritabanının oluşturulma işlemi
            //Database.Migrate();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //Veribanımızın bağlantı bilgisi
            options.UseSqlite("Data Source=libraryDB.sqlite");
        }
        public DbSet<BackupSchedule> BackupSchedules { get; set; }
        public DbSet<DriveUser> DriveUsers { get; set; }
        public DbSet<FtpThing> FtpThings { get; set; }
    }
}
