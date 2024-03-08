using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BettingApi.Models.EF;

public partial class BettingsDbContext : DbContext
{
    //I used DB first approach
    private readonly IConfiguration _configuration;
    public BettingsDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public BettingsDbContext(DbContextOptions<BettingsDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<MatchOdd> MatchOdds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("BettingsDB"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.TeamA).HasMaxLength(50);
            entity.Property(e => e.TeamB).HasMaxLength(50);
        });

        modelBuilder.Entity<MatchOdd>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Specifier).HasMaxLength(20);

            entity.HasOne(d => d.Match).WithMany(p => p.MatchOdds)
                .HasForeignKey(d => d.MatchId)
                .HasConstraintName("FK_MatchOdds_Matches");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
