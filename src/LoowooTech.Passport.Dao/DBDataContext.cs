using LoowooTech.Passport.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Dao
{
    public class DBDataContext : DbContext
    {
        public DBDataContext()
            : base("name=DBDataContext")
        {
        }

        public DBDataContext(string connectionString)
            : base(connectionString)
        {
        }

        private static readonly string _dbSchema = System.Configuration.ConfigurationManager.AppSettings["DBSchema"];

        private static string GetTableName<T>()
        {
            var type = typeof(T);
            return string.Format("{0}.{1}", _dbSchema, typeof(T).Name);
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<GroupRight> GroupRight { get; set; }
        public DbSet<AccessToken> AccessToken { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<OperateLog> OperateLog { get; set; }
        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable(MappingHelper.GetTableName<Account>());
            modelBuilder.Entity<Group>().ToTable(MappingHelper.GetTableName<Group>());
            modelBuilder.Entity<GroupRight>().ToTable(MappingHelper.GetTableName<GroupRight>());
            modelBuilder.Entity<AccessToken>().ToTable(MappingHelper.GetTableName<AccessToken>());
            modelBuilder.Entity<Client>().ToTable(MappingHelper.GetTableName<Client>());
            modelBuilder.Entity<OperateLog>().ToTable(MappingHelper.GetTableName<OperateLog>());
            modelBuilder.Entity<Department>().ToTable(MappingHelper.GetTableName<Department>());
        }
    }
}
