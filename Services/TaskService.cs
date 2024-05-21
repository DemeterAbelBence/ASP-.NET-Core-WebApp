using System.Data;
using System.Threading.Tasks;
using Bme.Swlab1.Rest.Dal;
using Bme.Swlab1.Rest.Dal.Entities;
using Bme.Swlab1.Rest.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Bme.Swlab1.Rest.Services;


public class TaskService : ITaskService
{
    private readonly TasksDbContext _dbContext;

    private Dtos.Task ToModel(DbTask value)
    {
        var status = _dbContext.Statuses.SingleOrDefault(s => s.Id == value.StatusId);
        string statusName = status != null ? status.Name : null;
        return new Dtos.Task(value.Id, value.Title, value.IsDone, statusName);
    }

    public TaskService(TasksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    Dtos.Task ITaskService.Delete(int taskId)
    {
        var task = _dbContext.Tasks.SingleOrDefault(t => t.Id == taskId);

        if (task == null)
        {
            return null;
        }
        else
        {
            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();
            return ToModel(task);
        }
    }

    Dtos.Task ITaskService.FindById(int taskId)
    {
        var task = _dbContext.Tasks.SingleOrDefault(t => t.Id == taskId);

        if (task == null)
            return null;
        else
            return ToModel(task);
    }

    Dtos.Task ITaskService.Insert(CreateTask value)
    {
        using var tran = _dbContext.Database.BeginTransaction(IsolationLevel.RepeatableRead);

        var status = _dbContext.Statuses.SingleOrDefault(s => s.Name == value.Status);
        if(status == null)
        {
            status = new DbStatus() { Name = value.Status };
            _dbContext.Statuses.Add(status);
            _dbContext.SaveChanges();
        }

        var task = new DbTask() { Title =  value.Title, IsDone = false, Status = status };
        _dbContext.Tasks.Add(task);
        _dbContext.SaveChanges();
        tran.Commit();

        return ToModel(task);
    }

    IReadOnlyCollection<Dtos.Task> ITaskService.List()
    {
        return _dbContext.Tasks.Select(ToModel).ToList();
    }

    Dtos.Task ITaskService.MarkDone(int taskId)
    {
        var task = _dbContext.Tasks.SingleOrDefault(t => t.Id == taskId);

        if (task == null)
        {
            return null;
        }
        else
        {
            task.IsDone = true;
            return ToModel(task);
        }
    }

    Dtos.Task ITaskService.MoveToStatus(int taskId, string newStatusName)
    {
        var status = _dbContext.Statuses.SingleOrDefault(s => s.Name == newStatusName);
        if (status == null)
        {
            status = new DbStatus() { Name = newStatusName };
            _dbContext.Statuses.Add(status);
            _dbContext.SaveChanges();
        }

        var task = _dbContext.Tasks.SingleOrDefault(t => t.Id == taskId);

        if (task == null)
        {
            return null;
        }
        else
        {
            task.StatusId = status.Id;
            task.Status = status;
            _dbContext.SaveChanges();
            return ToModel(task);
        }
    }
}

