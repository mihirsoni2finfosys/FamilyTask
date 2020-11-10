using Core.Abstractions;
using Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataLayer
{
    public class FamilyTaskContext : DbContext
    {

        public FamilyTaskContext(DbContextOptions<FamilyTaskContext> options) : base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<TaskDm> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskDm>()
                .HasOne<Member>(s => s.MemberObj)
                .WithMany()
                .HasForeignKey(s => s.Member);

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.ToTable("Member");
            });

            modelBuilder.Entity<TaskDm>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.ToTable("Task");
                entity.Property(e => e.Text).HasColumnName("Subject");
                entity.Property(e => e.IsDone).HasColumnName("IsComplete");
                entity.Property(e => e.Member).HasColumnName("AssignedToId");
            });
        }
    }
}