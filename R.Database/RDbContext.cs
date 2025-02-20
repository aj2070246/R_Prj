using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using R.Database.Entities;

namespace R.Database
{
    public class RDbContext : DbContext
    {
        public RDbContext(DbContextOptions<RDbContext> options) : base(options) { }
        public DbSet<RUsers> Users { get; set; }
        public DbSet<UsersMessages> UsersMessages { get; set; }
        public DbSet<Captcha> Captchas { get; set; }
        public DbSet<Age> Age { get; set; }
        public DbSet<HealthStatus> HealthStatus { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<MarriageStatus> MarriageStatus { get; set; }
        public DbSet<LiveType> LiveType { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<IncomeAmount> IncomeAmount { get; set; }
        public DbSet<CarValue> CarValue { get; set; }
        public DbSet<HomeValue> HomeValue { get; set; }
        public DbSet<RelationType> RelationType { get; set; }

    }
}
