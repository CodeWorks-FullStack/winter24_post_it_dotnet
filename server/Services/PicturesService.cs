namespace post_it_dotnet.Services;


public class PicturesService
{
  public PicturesService(PicturesRepository repository)
  {
    _repository = repository;
  }
  private readonly PicturesRepository _repository;

}