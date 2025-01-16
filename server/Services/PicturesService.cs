


namespace post_it_dotnet.Services;


public class PicturesService
{
  public PicturesService(PicturesRepository repository)
  {
    _repository = repository;
  }
  private readonly PicturesRepository _repository;

  internal Picture CreatePicture(Picture pictureData)
  {
    Picture picture = _repository.CreatePicture(pictureData);
    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    List<Picture> pictures = _repository.GetPicturesByAlbumId(albumId);
    return pictures;
  }

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

    _repository.DeletePicture(pictureId);

    return "Picture was deleted!";
  }
}