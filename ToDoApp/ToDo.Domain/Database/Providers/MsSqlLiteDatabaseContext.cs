using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Database.Model;

namespace ToDo.Domain.Database.Providers
{
    public class MsSqlLiteDatabaseContext : DbContext
    {
        public DbSet<ToDoDbModel> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
             => options.UseSqlite(@"Data Source=D:\GittHub\ToDoApp\ToDoApp\ToDo.Domain\todo.db");
    }
}