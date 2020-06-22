using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Database.Model;

namespace ToDo.Domain.Database.Providers
{
    public class MsSqlLiteDatabaseContext : DbContext
    {
        private const string dbFileName = "todo.db";
        private const string dbFileDirectory = "ToDo.Domain";

        public DbSet<ToDoDbModel> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite($"Data Source={GetDbFilePath()}");

        private string GetDbFilePath()
        {
            string dbFileFullDirectory = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName, dbFileDirectory);
            string dbFilePath = Path.Combine(dbFileFullDirectory, dbFileName);
            return dbFilePath = @"D:\GittHub\ToDoApp\ToDoApp\ToDo.Domain\todo.db";
        }
    }
}