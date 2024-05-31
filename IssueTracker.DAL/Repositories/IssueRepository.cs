using IssueTracker.DAL.Interfaces;
using IssueTracker.Domain.Entity;

namespace IssueTracker.DAL.Repositories;

public class IssueRepository : IBaseRepository<IssueEntity>
{
    private readonly AppDbContext _appDbContext;

    public IssueRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Create(IssueEntity entity)
    {
        await _appDbContext.Issues.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public IQueryable<IssueEntity> GetAll()
    {
        return _appDbContext.Issues;
    }

    public async Task Delete(IssueEntity entity)
    {
        _appDbContext.Issues.Remove(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<IssueEntity> Update(IssueEntity entity)
    {
        _appDbContext.Issues.Update(entity);
        await _appDbContext.SaveChangesAsync();
        return entity;
    }
}