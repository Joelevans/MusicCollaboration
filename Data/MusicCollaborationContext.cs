﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicCollaboration.Areas.Identity.Data;
using MusicCollaboration.Models;

namespace MusicCollaboration.Data
{
    public class MusicCollaborationContext: IdentityDbContext<MusicCollaborationUser>
    {
        public MusicCollaborationContext(DbContextOptions<MusicCollaborationContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //setting a composite key to the relation CollaborationMembers
            builder.Entity<CollaborationMember>()
                .HasKey(x => new { x.CollaborationID, x.UserID });
        }

        public DbSet<Collaboration> Collaboration { get; set; }
        public DbSet<CollaborationMember> CollaborationMembers { get; set; }
    }
}
