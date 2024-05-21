using System.ComponentModel.DataAnnotations.Schema;
using Bme.Swlab1.Rest.Dtos;

namespace Bme.Swlab1.Rest.Dal.Entities;

public class DbTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }

    public int StatusId {  get; set; }
    public DbStatus Status { get; set; }
}
