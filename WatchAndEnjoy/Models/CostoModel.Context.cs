﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WatchAndEnjoy.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WatchAndEnjoyDbEntities : DbContext
    {
        public WatchAndEnjoyDbEntities()
            : base("name=WatchAndEnjoyDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Anime> Animes { get; set; }
        public virtual DbSet<Movy> Movies { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
    }
}
