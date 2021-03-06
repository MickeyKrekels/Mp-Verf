﻿using Core.Repositories.Conventions;
using Repositorie.Entities.Base;
using Repositorie.Entities.Users;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Repositorie.DbContexts
{
    public class MPVerfDB : DbContext, IMPVerfDB
    {
        public MPVerfDB()
           : base("dbi436203")
        {
            Database.SetInitializer<MPVerfDB>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.AddBefore<ForeignKeyAssociationMultiplicityConvention>(new CustomForeignKeyDiscoveryConvention());
        }

        //users
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Admin> Admin { get; set; }

        //Products
        public DbSet<StoreItem> StoreItem { get; set; }
        public DbSet<StoreImage> StoreImage { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        //comments
        public DbSet<UserComment> UserComment { get; set; }
        public DbSet<CommentRating> CommentRating { get; set; }

    }
}

