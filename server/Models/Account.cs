namespace post_it_dotnet.Models;

public class Account : Profile
{
  // NOTE inherited from profile!
  // public string Id { get; set; }
  // public string Name { get; set; }
  // public string Picture { get; set; }
  public string Email { get; set; }
}
