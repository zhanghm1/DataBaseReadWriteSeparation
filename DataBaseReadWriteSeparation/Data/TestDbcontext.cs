using DataBaseReadWriteSeparation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseReadWriteSeparation.Data
{
    public class TestDbcontext : DbContext
    {
        public TestDbcontext(DbContextOptions<TestDbcontext> options) : base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }
    }
}
