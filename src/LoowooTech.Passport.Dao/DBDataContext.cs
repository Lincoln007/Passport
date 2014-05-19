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
        public DbSet<AccountAgent> AccountAgent { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<GroupRight> GroupRight { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<OperateLog> OperateLog { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Rank> Rank { get; set; }

        public DbSet<VAccount> VAccount { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable(MappingHelper.GetTableName<Account>());
            modelBuilder.Entity<AccountAgent>().ToTable(MappingHelper.GetTableName<AccountAgent>());
            modelBuilder.Entity<Group>().ToTable(MappingHelper.GetTableName<Group>());
            modelBuilder.Entity<GroupRight>().ToTable(MappingHelper.GetTableName<GroupRight>());
            modelBuilder.Entity<Client>().ToTable(MappingHelper.GetTableName<Client>());
            modelBuilder.Entity<OperateLog>().ToTable(MappingHelper.GetTableName<OperateLog>());
            modelBuilder.Entity<Department>().ToTable(MappingHelper.GetTableName<Department>());
            modelBuilder.Entity<Rank>().ToTable(MappingHelper.GetTableName<Rank>());

            modelBuilder.Entity<VAccount>().ToTable(MappingHelper.GetTableName<VAccount>());
        }
    }
}
