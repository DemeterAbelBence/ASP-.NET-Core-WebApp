namespace Bme.Swlab1.Rest.Dal.Entities;

public class DbStatus
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<DbTask> Tasks { get; set; }
}
