﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Band> Bands { get; set; }
        public virtual DbSet<BandMember> BandMembers { get; set; }
        public virtual DbSet<BandMembersList> BandMembersLists { get; set; }
        public virtual DbSet<FavoriteBandList> FavoriteBandLists { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Performance> Performances { get; set; }
        public virtual DbSet<ShowList> ShowLists { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<BandComment> BandComments { get; set; }
        public virtual DbSet<PerformanceCommentThread> PerformanceCommentThreads { get; set; }
        public virtual DbSet<SetList> SetLists { get; set; }
    }
}
