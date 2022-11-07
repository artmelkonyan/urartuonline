
using Microsoft.EntityFrameworkCore;
using Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityDataLayer.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<Banner> Banner { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }

    }
}
