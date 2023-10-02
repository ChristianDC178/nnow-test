using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nnow.Domain.Entities;

namespace Nnow.Database.Configurations;

public class PermissionTypeConfig : IEntityTypeConfiguration<PermissionType>
{
    public void Configure(EntityTypeBuilder<PermissionType> modelBuilder)
    {
        modelBuilder.ToTable("PermissionTypes");
        modelBuilder.HasKey(d => d.Id);
        modelBuilder.Ignore(d => d.Permissions);

        modelBuilder.HasMany(d => d.Permissions).WithOne(p => p.PermissionType).HasForeignKey((p) => p.PermissionTypeId);
    }
}