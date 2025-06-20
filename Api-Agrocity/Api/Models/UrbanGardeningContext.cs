using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api.Models;

public partial class UrbanGardeningContext : DbContext
{
    public UrbanGardeningContext()
    {
    }

    public UrbanGardeningContext(DbContextOptions<UrbanGardeningContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Garden> Gardens { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Pest> Pests { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<Publication> Publications { get; set; }

    public virtual DbSet<Reminder> Reminders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=localhost;Database=UrbanGardening;Trusted_Connection=True;TrustServerCertificate=True;");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.CalendarId).HasName("PK__Calendar__EE5496F63661BE91");

            entity.ToTable("Calendar");

            entity.Property(e => e.CalendarId).HasColumnName("calendarId");
            entity.Property(e => e.CalendarDate).HasColumnName("calendarDate");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__CDDE919D049988A8");

           // entity.HasIndex(e => e.PublicationId, "IX_Comments_publicationId");

            entity.HasIndex(e => e.UserId, "IX_Comments_userId");

            entity.Property(e => e.CommentId).HasColumnName("commentId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
           entity.Property(e => e.GardenId).HasColumnName("gardenId"); // nombre correcto en la BD

entity.HasOne(d => d.Garden).WithMany(p => p.Comments)
    .HasForeignKey(d => d.GardenId)
    .HasConstraintName("FK__Comments__gardenId");


            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Comments__userId__693CA210");
        });

        modelBuilder.Entity<Garden>(entity =>
        {
            entity.HasKey(e => e.GardenId).HasName("PK__Gardens__C5BCE574A5896009");

            entity.HasIndex(e => e.UserId, "IX_Gardens_userId");

            entity.Property(e => e.GardenId).HasColumnName("gardenId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Gardens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Gardens__userId__534D60F1");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__4BA5CEA97C271245");

            entity.HasIndex(e => e.UserId, "IX_Notifications_userId");

            entity.Property(e => e.NotificationId).HasColumnName("notificationId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Notificat__userI__6E01572D");
        });

        modelBuilder.Entity<Pest>(entity =>
        {
            entity.HasKey(e => e.PestId).HasName("PK__Pests__7F10C1DDE81FD7F1");

            entity.Property(e => e.PestId)
                .ValueGeneratedOnAdd()
                .HasColumnName("pestId");
            entity.Property(e => e.CommonName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("commonName");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Host)
                .HasColumnType("text")
                .HasColumnName("host");
            entity.Property(e => e.ImageUrl)
                .HasColumnType("text")
                .HasColumnName("imageUrl");
            entity.Property(e => e.ScientificName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("scientificName");
            entity.Property(e => e.Solution)
                .HasColumnType("text")
                .HasColumnName("solution");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__Photos__547C322DFF40E13E");

            entity.Property(e => e.PhotoId).HasColumnName("photoId");
            entity.Property(e => e.PhotoUrl)
                .HasColumnType("text")
                .HasColumnName("photoUrl");
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.PlantId).HasName("PK__Plants__870532B032979DAC");

            entity.Property(e => e.PlantId).HasColumnName("plantId");
            entity.Property(e => e.CareLevel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("careLevel");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.FlowerDetails)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("flowerDetails");
            entity.Property(e => e.FruitDetails)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fruitDetails");
            entity.Property(e => e.GrowthCycle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("growthCycle");
            entity.Property(e => e.GrowthRate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("growthRate");
            entity.Property(e => e.HardinessZone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("hardinessZone");
            entity.Property(e => e.HardinessZoneDescription)
                .HasColumnType("text")
                .HasColumnName("hardinessZoneDescription");
            entity.Property(e => e.HasLeaves).HasColumnName("hasLeaves");
            entity.Property(e => e.IsEdible).HasColumnName("isEdible");
            entity.Property(e => e.IsSaltTolerant).HasColumnName("isSaltTolerant");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("lastModified");
            entity.Property(e => e.LeafColor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("leafColor");
            entity.Property(e => e.MaintenanceLevel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("maintenanceLevel");
            entity.Property(e => e.PlantName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("plantName");
            entity.Property(e => e.ScientificName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("scientificName");
            entity.Property(e => e.SunExposure)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sunExposure");
            entity.Property(e => e.WateringFrequency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("wateringFrequency");

            entity.HasMany(d => d.Pests).WithMany(p => p.Plants)
                .UsingEntity<Dictionary<string, object>>(
                    "PlantPest",
                    r => r.HasOne<Pest>().WithMany()
                        .HasForeignKey("PestId")
                        .HasConstraintName("FK__PlantPest__pestI__59063A47"),
                    l => l.HasOne<Plant>().WithMany()
                        .HasForeignKey("PlantId")
                        .HasConstraintName("FK__PlantPest__plant__5812160E"),
                    j =>
                    {
                        j.HasKey("PlantId", "PestId").HasName("PK__PlantPes__40F43EAD78645CA7");
                        j.ToTable("PlantPests");
                        j.HasIndex(new[] { "PestId" }, "IX_PlantPests_pestId");
                        j.IndexerProperty<int>("PlantId").HasColumnName("plantId");
                        j.IndexerProperty<int>("PestId").HasColumnName("pestId");
                    });
        });

        modelBuilder.Entity<Publication>(entity =>
        {
            entity.HasKey(e => e.PublicationId).HasName("PK__Publicat__883D5CDF2EDC98F4");

            entity.HasIndex(e => e.UserId, "IX_Publications_userId");

            entity.Property(e => e.PublicationId).HasColumnName("publicationId");
            entity.Property(e => e.CommentId).HasColumnName("commentId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdAt");
            entity.Property(e => e.Likes)
                .HasDefaultValue(0)
                .HasColumnName("likes");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Publications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Publicati__userI__656C112C");
        });

        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.HasKey(e => e.ReminderId).HasName("PK__Reminder__09DAAAE369C1E1AB");

            entity.HasIndex(e => e.PlantId, "IX_Reminders_plantId");

            entity.HasIndex(e => e.UserId, "IX_Reminders_userId");

            entity.Property(e => e.ReminderId).HasColumnName("reminderId");
            entity.Property(e => e.PlantId).HasColumnName("plantId");
            entity.Property(e => e.ReminderDate).HasColumnName("reminderDate");
            entity.Property(e => e.TypeReminder)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("typeReminder");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Plant).WithMany(p => p.Reminders)
                .HasForeignKey(d => d.PlantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Reminders__plant__5CD6CB2B");

            entity.HasOne(d => d.User).WithMany(p => p.Reminders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Reminders__userI__5BE2A6F2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CFF12AAA002");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E616439D41849").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Surname)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("surname");
            entity.Property(e => e.Telephone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telephone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
