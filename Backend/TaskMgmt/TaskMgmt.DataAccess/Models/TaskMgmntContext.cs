using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskMgmt.DataAccess.Models
{
    public class TaskMgmntContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<UserGroup> UserGroups { get; set; } = null!;
        public DbSet<Invitation> Invitations { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<ProjectTask> ProjectTasks { get; set; } = null!;
        public DbSet<ProjectTaskStatus> ProjectTaskStatuses { get; set; } = null!;

        public TaskMgmntContext(DbContextOptions<TaskMgmntContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(e => e.DefaultGroup)
                .WithMany()
                .HasPrincipalKey(e => e.GroupId)
                .HasForeignKey(e => e.DefaultGroupId)
                .IsRequired(false);

            modelBuilder.Entity<UserGroup>().HasKey(e => new { e.UserId, e.GroupId });

            modelBuilder.Entity<Invitation>().HasOne(e => e.InvitedByUserNavigation)
                .WithMany(e => e.Invitations)
                .HasPrincipalKey(e => e.UserId)
                .HasForeignKey(e => e.InvitedByUser)
                .IsRequired();

            modelBuilder.Entity<Project>().HasOne(e => e.Owner)
                .WithMany(e => e.Projects)
                .HasPrincipalKey(e => e.UserId)
                .HasForeignKey(e => e.OwnerId)
                .IsRequired();

            // modelBuilder.Entity<ProjectTaskStatus>().HasIndex(e => new { e.ProjectId, e.StatusText })
            //     .IsUnique();

            modelBuilder.Entity<ProjectTask>(
                nestedBuilder =>
                {
                    nestedBuilder.HasOne(e => e.Assignee)
                        .WithMany()
                        .HasPrincipalKey(e => e.UserId)
                        .HasForeignKey(e => e.AssigneeId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    nestedBuilder.HasOne(e => e.Creator)
                        .WithMany()
                        .HasPrincipalKey(e => e.UserId)
                        .HasForeignKey(e => e.CreatorId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            // modelBuilder.Entity<ProjectTaskStatus>().HasOne(e => e.Project)
            //         .WithMany()
            //         .HasPrincipalKey(e => e.ProjectId)
            //         .HasForeignKey(e => e.ProjectId)
            //         .OnDelete(DeleteBehavior.NoAction)
            //         .IsRequired();
            modelBuilder.Entity<ProjectTaskStatus>(entity =>
            {
                entity.HasKey(e => e.ProjectTaskStatusId); // Primary key

                entity.Property(e => e.StatusText).IsRequired().HasMaxLength(450);
                entity.Property(e => e.StatusColor).IsRequired();

                // Foreign key relationship
                entity.HasOne(d => d.Project)
                      .WithMany(p => p.ProjectTaskStatuses)
                      .HasForeignKey(d => d.ProjectId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .IsRequired();

                // Unique constraint
                entity.HasIndex(e => new { e.ProjectId, e.StatusText }).IsUnique();
            });
        }
    }
}
