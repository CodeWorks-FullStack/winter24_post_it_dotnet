namespace post_it_dotnet.Repositories;

public class AlbumsRepository
{
  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }


  private readonly IDbConnection _db;

}