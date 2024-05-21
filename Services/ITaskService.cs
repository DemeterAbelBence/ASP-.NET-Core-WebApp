using Bme.Swlab1.Rest.Dtos;

using Task = Bme.Swlab1.Rest.Dtos.Task;

namespace Bme.Swlab1.Rest.Services;

// DO NOT MODIFY THE EXISTING METHODS
// but you could extend this interface
// 
// NE VALTOZTASD MEG A MEGLEVO FUGGVENYEKET,
// de kigészítheted az interfészt
public interface ITaskService
{
    IReadOnlyCollection<Task> List();
    Task FindById(int taskId);
    Task Insert(CreateTask value);
    Task Delete(int taskId);

    Task MarkDone(int taskId);
    Task MoveToStatus(int taskId, string newStatusName);
}
