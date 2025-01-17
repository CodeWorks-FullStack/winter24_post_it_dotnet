namespace post_it_dotnet.Controllers;

[ApiController]
[Route("api/{controller}")]
public class WatchersController : ControllerBase
{

  public WatchersController(WatchersService watchersService, Auth0Provider auth0Provider)
  {
    _watchersService = watchersService;
    _auth0Provider = auth0Provider;
  }
  private readonly Auth0Provider _auth0Provider;
  private readonly WatchersService _watchersService;

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<WatcherProfile>> CreateWatcher([FromBody] Watcher watcherData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      watcherData.AccountId = userInfo.Id;
      WatcherProfile watcherProfile = _watchersService.CreateWatcher(watcherData);
      return Ok(watcherProfile);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{watcherId}")]
  public async Task<ActionResult<string>> DeleteWatcher(int watcherId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _watchersService.DeleteWatcher(watcherId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
