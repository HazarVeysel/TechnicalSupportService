﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class helpdeskEntities : DbContext
    {
        public helpdeskEntities()
            : base("name=helpdeskEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<RequestCategory> RequestCategories { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}