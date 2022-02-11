using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskChecklist.Models
{
    public partial class DBTasksContext : DbContext
    {
        public DBTasksContext()
        {
        }

        public DBTasksContext(DbContextOptions<DBTasksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<T01Job> T01Jobs { get; set; } = null!;
        public virtual DbSet<T02JobTask> T02JobTasks { get; set; } = null!;
        public virtual DbSet<T03Template> T03Templates { get; set; } = null!;
        public virtual DbSet<T04TemplateTask> T04TemplateTasks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T01Job>(entity =>
            {
                entity.ToTable("T01_Jobs");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.JobDate)
                    .HasColumnType("date")
                    .HasColumnName("job_date");

                entity.Property(e => e.JobDesc)
                    .IsUnicode(false)
                    .HasColumnName("job_desc");

                entity.Property(e => e.JobName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("job_name");

                entity.Property(e => e.JobTemplateId)
                    .HasColumnName("job_template_id")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.JobTemplate)
                    .WithMany(p => p.T01Jobs)
                    .HasForeignKey(d => d.JobTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T01_Jobs_T03_Template");
            });

            modelBuilder.Entity<T02JobTask>(entity =>
            {
                entity.ToTable("T02_JobTasks");

                entity.HasIndex(e => e.JobId, "IX_T02_JobTasks");

                entity.HasIndex(e => new { e.TaskId, e.JobId }, "IX_T02_JobTasks_1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.Property(e => e.TaskComments)
                    .IsUnicode(false)
                    .HasColumnName("task_comments");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("task_name");

                entity.Property(e => e.TaskSkipped).HasColumnName("task_skipped");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.T02JobTasks)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T02_JobTasks_T01_Jobs");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.T02JobTasks)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T02_JobTasks_T04_TemplateTasks");
            });

            modelBuilder.Entity<T03Template>(entity =>
            {
                entity.ToTable("T03_Template");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TemplateName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("template_name");
            });

            modelBuilder.Entity<T04TemplateTask>(entity =>
            {
                entity.ToTable("T04_TemplateTasks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TaskDesc)
                    .IsUnicode(false)
                    .HasColumnName("task_desc");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("task_name");

                entity.Property(e => e.TaskOrder)
                    .HasColumnName("task_order")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TemplateId).HasColumnName("template_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("updated_by");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.T04TemplateTasks)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T04_TemplateTasks_T03_Template");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
