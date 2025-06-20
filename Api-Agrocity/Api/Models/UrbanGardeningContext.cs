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





}
