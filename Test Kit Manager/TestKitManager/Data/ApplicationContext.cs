using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestKitManager.Data
{
    public class ApplicationContext : DbContext
    {
        public string DbPath { get; }

        public ApplicationContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "database.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}