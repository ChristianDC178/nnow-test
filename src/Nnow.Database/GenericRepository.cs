using Microsoft.EntityFrameworkCore;
using Nnow.Domain.Entities;

namespace Nnow.Database;

public class GenericRepository<TEntity> where TEntity : EntityBase
{

    private readonly NnowContext _context;

    public GenericRepository(NnowContext context) => _context = context;

    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cToken = default(CancellationToken), string property = null)
    {
        var set = _context.Set<TEntity>();
        IQueryable<TEntity> query = null;

        if (property != null)
        {
            query = _context.Set<TEntity>().Include(property);
        }

        return await set.FirstOrDefaultAsync(e => e.Id == id, cToken);
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return _context.Set<TEntity>().AsQueryable();
    }

    public void Update(TEntity entity) => _context.Entry(entity).State = EntityState.Modified;

    public void Delete(TEntity entity) => _context.Entry(entity).State = EntityState.Deleted;

    public void Create(TEntity entity) => _context.Entry(entity).State = EntityState.Added;

}

