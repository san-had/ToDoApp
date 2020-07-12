using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDo.Domain.Database.Model;
using ToDo.Extensibility.Dto;

namespace ToDo.Domain.Database.Providers
{
    public class MsSqlLiteDatabaseContext : DbContext
    {
        private readonly string sqlLiteDbFilePath;

        public MsSqlLiteDatabaseContext(IOptionsSnapshot<ConfigurationSettings> configurationSettings)
        {
            sqlLiteDbFilePath = configurationSettings.Value.SqlLiteDbFilePath;
        }

        public DbSet<ToDoDbModel> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
             => options.UseSqlite($"Data Source={sqlLiteDbFilePath}");
    }
}