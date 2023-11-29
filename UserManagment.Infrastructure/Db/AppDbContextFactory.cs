﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace UserManagment.Infrastructure.Db
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(args[0]);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}