using Microsoft.EntityFrameworkCore;
using PULL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PULL.Services
{
    internal class MySQLDbContext: DbContext
    {
        public DbSet<nominaEmpresas> NominaEmpresas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=ISO815;Uid=admin;Pwd=123456;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
