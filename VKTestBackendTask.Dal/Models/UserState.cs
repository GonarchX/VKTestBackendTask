using System.ComponentModel.DataAnnotations.Schema;

namespace VKTestBackendTask.Dal.Models;

[Table("user_state")]
public class UserState
{
    [Column("id")]
    public short Id { get; set; }

    [Column("code")] 
    public string Code { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }
}