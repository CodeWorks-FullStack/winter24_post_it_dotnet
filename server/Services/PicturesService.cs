


namespace post_it_dotnet.Services;


public class PicturesService
{
  public PicturesService(PicturesRepository repository, AlbumsService albumsService)
  {
    _repository = repository;
    _albumsService = albumsService;
  }
  // NOTE each service should only have access to one repository
  private readonly PicturesRepository _repository;
  // NOTE services can call to other services, but should never directly call to a different repository
  private readonly AlbumsService _albumsService;

  internal Picture CreatePicture(Picture pictureData)
  {
    Album album = _albumsService.GetAlbumById(pictureData.AlbumId);

    if (album.Archived) throw new Exception($"{album.Title} is archived and no longer accepting pictures");

    // NOTE make sure all relevant checks are done BEFORE changing the data in the database
    Picture picture = _repository.CreatePicture(pictureData);

    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    List<Picture> pictures = _repository.GetPicturesByAlbumId(albumId);
    return pictures;
  }

  // NOTE if your controller doesn't need access to the method, don't expose it
  private Picture GetPictureById(int pictureId)
  {
    Picture picture = _repository.GetPictureById(pictureId);

    if (picture == null) throw new Exception($"Invalid picture id: {pictureId}");

    return picture;
  }

  internal string DeletePicture(int pictureId, string userId)
  {
    Picture picture = GetPictureById(pictureId);

    if (picture.CreatorId != userId) throw new Exception("YOU CAN NOT DELETE ANOTHER USER'S PICTURE, BUD");

    Album album = _albumsService.GetAlbumById(picture.AlbumId);

    if (album.Archived) throw new Exception($"{album.Title} is archived and cannot have pictures removed from it!");

    // NOTE make sure all relevant checks are done BEFORE changing the data in the database
    _repository.DeletePicture(pictureId);

    return "Picture was deleted!";
  }
}