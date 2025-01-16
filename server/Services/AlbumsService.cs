



namespace post_it_dotnet.Services;

public class AlbumsService
{
  public AlbumsService(AlbumsRepository repository)
  {
    _repository = repository;
  }
  private readonly AlbumsRepository _repository;

  internal Album CreateAlbum(Album albumData)
  {
    Album album = _repository.CreateAlbum(albumData);
    return album;
  }

  internal List<Album> GetAllAlbums()
  {
    List<Album> albums = _repository.GetAllAlbums();
    return albums;
  }

  internal Album GetAlbumById(int albumId)
  {
    Album album = _repository.GetAlbumById(albumId);

    if (album == null) throw new Exception($"Invalid album id: {albumId}");

    return album;
  }

  internal Album ArchiveAlbum(int albumId, string userId)
  {
    Album album = GetAlbumById(albumId);

    if (album.CreatorId != userId) throw new Exception("YOU CANNOT ARCHIVE ANOTHER USER'S ALBUM, FRIEND-O");

    _repository.ArchiveAlbum(albumId);

    album.Archived = true;

    return album;
  }
}