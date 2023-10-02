using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nnow.Domain.Entities;

namespace Nnow.Database.Configurations;

public class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> modelBuilder)
    {
        modelBuilder.ToTable("Permissions");
        modelBuilder.HasKey(d => d.Id);
        modelBuilder.Ignore(d => d.PermissionType);
    }
}