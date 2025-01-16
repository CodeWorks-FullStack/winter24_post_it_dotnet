namespace post_it_dotnet.Services;

public class AlbumsService
{
  public AlbumsService(AlbumsRepository repository)
  {
    _repository = repository;
  }
  private readonly AlbumsRepository _repository;

}