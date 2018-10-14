using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Backend.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UserDbContext() { }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(a => a.Email);
        }
    }
}
