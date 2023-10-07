using Core.Entities;

namespace Entities.Concrete;

public class User : IEntity
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string UserMail { get; set; }
    public string Password { get; set; }
}