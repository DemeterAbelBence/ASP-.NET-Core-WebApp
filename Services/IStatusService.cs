using Bme.Swlab1.Rest.Dtos;

namespace Bme.Swlab1.Rest.Services;

// DO NOT MODIFY THE EXISTING METHODS
// NE VALTOZTASD MEG A MEGLEVO FUGGVENYEKET
public interface IStatusService
{
    IReadOnlyCollection<Status> List();
    bool ExistsWithName(string statusName);
    Status FindById(int statusId);
    Status Insert(CreateStatus value);
}
