using Microsoft.EntityFrameworkCore;
using FMG.NRT.Data.Core;
using FMG.NRT.Tests.Objects;
using System;

namespace FMG.NRT.Tests.Data
{
    public class TestingContext : Context
    {
        #region Tests

        protected DbSet<TestModel> TestModel { get; set; }

        #endregion

        public String DatabaseName { get; }

        public TestingContext()
            : this(Guid.NewGuid().ToString())
        {
        }
        public TestingContext(String databaseName)
            : base(ConfigurationFactory.Create())
        {
            DatabaseName = databaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase(DatabaseName);
        }
    }
}
