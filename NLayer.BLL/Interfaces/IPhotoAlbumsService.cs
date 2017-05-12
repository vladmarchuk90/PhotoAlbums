using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.BLL.DTO;
using AutoMapper;

namespace NLayer.BLL.Interfaces
{
    public interface IPhotoAlbumsService
    {
        IEnumerable<UserDTO> GetUsers();
        IEnumerable<AlbumDTO> GetAlbums();
        IEnumerable<PhotoDTO> GetPhotos();

        UserDTO GetUser(int id);
        AlbumDTO GetAlbum(int id);
        PhotoDTO GetPhoto(int id);

        IEnumerable<AlbumDTO> GetAlbumsOfUser(int id);
        IEnumerable<PhotoDTO> GetPhotosOfUser(int id);
        IEnumerable<PhotoDTO> GetPhotosOfAlbum(int id);

        void CreateUser(UserDTO userDTO);
        void CreateAlbum(AlbumDTO albumDTO);
        void CreatePhoto(PhotoDTO photoDTO);

        void UpdateUser(UserDTO userDTO);
        void UpdateAlbum(AlbumDTO albumDTO);
        void UpdatePhoto(PhotoDTO photoDTO);
    }
}
