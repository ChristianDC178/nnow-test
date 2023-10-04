using Nnow.Domain.Entities;

namespace Nnow.Database;

public class UnitOfWork
{
    private readonly NnowContext _context;
    private GenericRepository<Permission> _permissionRepo;
    private GenericRepository<PermissionType> _permissionTypeRepo;

    public UnitOfWork(NnowContext context) => _context = context;

    public GenericRepository<Permission> PermissionRepo 
    {
        get { return _permissionRepo ?? new GenericRepository<Permission>(_context); }
    }

    public GenericRepository<PermissionType> PermissionTypeRepo
    {
        get { return _permissionTypeRepo ?? new GenericRepository<PermissionType>(_context); }
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

}

