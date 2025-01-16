namespace post_it_dotnet.Controllers;

[ApiController]
[Route("api/{controller}")] // api/albums
public class AlbumsController : ControllerBase
{
  public AlbumsController(AlbumsService albumsService)
  {
    _albumsService = albumsService;
  }
  private readonly AlbumsService _albumsService;

}