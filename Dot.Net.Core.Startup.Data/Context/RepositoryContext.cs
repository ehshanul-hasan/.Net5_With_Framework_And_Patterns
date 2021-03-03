using Dot.Net.Core.Startup.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dot.Net.Core.Startup.Data.Context
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Hahn_Applicant").ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            optionsBuilder.LogTo(Console.WriteLine);

        }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Add Dummy Data Here If Needed
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Applicant> Applicants { get; set; }

    }
}
