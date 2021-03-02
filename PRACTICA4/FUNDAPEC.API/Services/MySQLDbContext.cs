using FUNDAPEC.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FUNDAPEC.API.Services
{
    public class MySQLDbContext: DbContext
    {
        public MySQLDbContext(DbContextOptions<MySQLDbContext> options): base(options)
        {

        }

        public DbSet<Detail> PAYMENTS { get; set; }
    }
}
