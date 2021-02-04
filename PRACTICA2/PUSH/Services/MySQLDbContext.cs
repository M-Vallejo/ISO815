﻿using Microsoft.EntityFrameworkCore;
using PUSH.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUSH.Services
{
    internal class MySQLDbContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=ISO815;Uid=Alvaro;Pwd=armp0208;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
