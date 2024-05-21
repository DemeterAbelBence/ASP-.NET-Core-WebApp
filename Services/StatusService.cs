using System.Data;
using Bme.Swlab1.Rest.Dal;
using Bme.Swlab1.Rest.Dal.Entities;
using Bme.Swlab1.Rest.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Bme.Swlab1.Rest.Services;

public class StatusService : IStatusService
{
    private readonly TasksDbContext _dbContext;

    private Status ToModel(DbStatus value)
    {
        return new Status(value.Id, value.Name);
    }

    public StatusService(TasksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool ExistsWithName(string statusName)
    {
        return _dbContext.Statuses.Any(s => EF.Functions.Like(s.Name, statusName));
    }

    public Status FindById(int statusId)
    {
        var status = _dbContext.Statuses.SingleOrDefault(s => s.Id == statusId);
        return status == null ? null : ToModel(status);
    }

    public Status Insert(CreateStatus value)
    {
        using var tran = _dbContext.Database.BeginTransaction(IsolationLevel.RepeatableRead);

        if (_dbContext.Statuses.Any(s => EF.Functions.Like(s.Name, value.Name)))
            throw new ArgumentException("Name must be unique");

        var status = new DbStatus() { Name = value.Name };
        _dbContext.Statuses.Add(status);

        _dbContext.SaveChanges();
        tran.Commit();

        return ToModel(status);
    }

    public IReadOnlyCollection<Status> List()
    {
        return _dbContext.Statuses.Select(ToModel).ToList();
    }
}
