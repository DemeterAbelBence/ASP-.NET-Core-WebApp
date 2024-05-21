namespace Bme.Swlab1.Rest.Dtos;

// DO NOT CHANGE ANYTHING
// NE VALTOZTASS MEG SEMMIT
public record PagedTaskList(IReadOnlyCollection<Task> Items, int Count, int? NextId, string NextUrl);
