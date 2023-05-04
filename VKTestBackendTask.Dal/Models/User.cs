using System.ComponentModel.DataAnnotations.Schema;

namespace VKTestBackendTask.Dal.Models;

[Table("user")]
public class User
{
    [Column("id")] 
    public long Id { get; set; }
    
    [Column("login")] 
    public string Login { get; set; } = null!;
    
    [Column("password")] 
    public string Password { get; set; } = null!;
    
    [Column("created_date")] 
    public DateTime CreatedDate { get; set; }
    
    [Column("user_group_id")] 
    public short UserGroupId { get; set; }
    
    public UserGroup? UserGroup { get; set; }
    
    [Column("user_state_id")] 
    public short UserStateId { get; set; }
    
    public UserState? UserState { get; set; }
}