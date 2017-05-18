using NLayer.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.BLL.DTO;
using NLayer.DAL.Interfaces;
using NLayer.DAL.Entities;
using AutoMapper;

namespace NLayer.BLL.Services
{
    public class PhotoAlbumsService : IPhotoAlbumsService
    {
        private IUnitOfWork Database { get; set; }

        //We need to use intances of Mapper instead non-statical, because:
        //1. always need to change map configuration in static class
        //2. if use, for example AJAX, different query can need to acquire different objects with different map config.
        private IMapper mapperDTOToEntity;
        private IMapper mapperEntityToDTO;

        public PhotoAlbumsService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;

            MapperConfiguration mapperConfigurationEntityToDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Album, AlbumDTO>();
                cfg.CreateMap<Photo, PhotoDTO>();
                cfg.CreateMap<User, UserDTO>();
            });

            MapperConfiguration mapperConfigurationDTOToEntity = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AlbumDTO, Album>();
                cfg.CreateMap<PhotoDTO, Photo>();
                cfg.CreateMap<UserDTO, User>();
            });

            mapperEntityToDTO = new Mapper(mapperConfigurationEntityToDTO);
            mapperDTOToEntity = new Mapper(mapperConfigurationDTOToEntity);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<User> users = Database.Users.GetAll();
            return mapperEntityToDTO.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

        public IEnumerable<AlbumDTO> GetAlbums()
        {
            IEnumerable<Album> albums = Database.Albums.GetAll();
            return mapperEntityToDTO.Map<IEnumerable<Album>, IEnumerable<AlbumDTO>>(albums);
        }

        public IEnumerable<PhotoDTO> GetPhotos()
        {
            IEnumerable<Photo> photos = Database.Photos.GetAll();
            Mapper.Initialize(cfg => cfg.CreateMap<Photo, PhotoDTO>());

            return mapperEntityToDTO.Map<IEnumerable<Photo>, IEnumerable<PhotoDTO>>(photos);
        }

        public UserDTO GetUser(string Name)
        {
            User user = null;

            IEnumerable<User> users = Database.Users.Find(usr => usr.Name == Name);
            if (users.Count() > 0)
                user = users.FirstOrDefault();
            return mapperEntityToDTO.Map<User, UserDTO>(user);
        }

        public AlbumDTO GetAlbum(int id)
        {
            Album album = Database.Albums.Get(id);
            return mapperEntityToDTO.Map<Album, AlbumDTO>(album);
        }

        public PhotoDTO GetPhoto(int id)
        {
            Photo photo = Database.Photos.Get(id);
            return mapperEntityToDTO.Map<Photo, PhotoDTO>(photo);
        }

        public IEnumerable<AlbumDTO> GetAlbumsOfUser(int id)
        {
            IEnumerable<Album> albums = Database.Albums.Find(album => album.UserId == id);
            return mapperEntityToDTO.Map<IEnumerable<Album>, IEnumerable<AlbumDTO>>(albums);
        }

        public IEnumerable<PhotoDTO> GetPhotosOfUser(int id)
        {
            IEnumerable<Photo> photos = Database.Photos.Find(photo => photo.UserId == id);
            return mapperEntityToDTO.Map<IEnumerable<Photo>, IEnumerable<PhotoDTO>>(photos);
        }

        public IEnumerable<PhotoDTO> GetPhotosOfAlbum(int id)
        {
            IEnumerable<Photo> photos = Database.Photos.Find(photo => photo.AlbumId == id);
            return mapperEntityToDTO.Map<IEnumerable<Photo>, IEnumerable<PhotoDTO>>(photos);
        }

        public void CreateUser(UserDTO userDTO)
        {
            User user = mapperDTOToEntity.Map<UserDTO, User>(userDTO);
            Database.Users.Create(user);
            SaveChangesInDatabase();
        }

        public void CreateAlbum(AlbumDTO albumDTO)
        {
            Album album = mapperDTOToEntity.Map<AlbumDTO, Album>(albumDTO);
            Database.Albums.Create(album);
            SaveChangesInDatabase();
        }

        public void CreatePhoto(PhotoDTO photoDTO)
        {
            Photo photo = mapperDTOToEntity.Map<PhotoDTO, Photo>(photoDTO);
            Database.Photos.Create(photo);
            SaveChangesInDatabase();
        }

        public void UpdateUser(UserDTO userDTO)
        {
            User user = mapperDTOToEntity.Map<UserDTO, User>(userDTO);
            Database.Users.Update(user);
            SaveChangesInDatabase();
        }

        public void UpdateAlbum(AlbumDTO albumDTO)
        {
            Album album = mapperDTOToEntity.Map<AlbumDTO, Album>(albumDTO);
            Database.Albums.Update(album);
            SaveChangesInDatabase();
        }

        public void UpdatePhoto(PhotoDTO photoDTO)
        {
            Photo photo = mapperDTOToEntity.Map<PhotoDTO, Photo>(photoDTO);
            Database.Photos.Update(photo);
            SaveChangesInDatabase();
        }

        private void SaveChangesInDatabase()
        {
            Database.Save();
        }

        public IEnumerable<AlbumDTO> GetGeneralAlbums()
        {
            IEnumerable<Album> albums = Database.Albums.Find(album => album.UserId == null);
            return mapperEntityToDTO.Map<IEnumerable<Album>, IEnumerable<AlbumDTO>>(albums);
        }
    }
}
