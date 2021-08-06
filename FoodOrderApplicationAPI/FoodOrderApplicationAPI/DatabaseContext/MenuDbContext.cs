using FoodOrderApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApplicationAPI.DatabaseContext
{
    public class MenuDbContext : DbContext
    {
        public MenuDbContext(DbContextOptions<MenuDbContext> options)
    : base(options)
        {

        }

        public DbSet<MenuModel> Menu { get; set; }
    }
}
