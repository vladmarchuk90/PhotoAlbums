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
        private IMapper mapperUserToUserDTO;
        private IMapper mapperAlbumToAlbumDTO;
        private IMapper mapperPhotoToPhotoDTO;
        private IMapper mapperUserDTOToUser;
        private IMapper mapperAlbumDTOToAlbum;
        private IMapper mapperPhotoDTOToPhoto;

        public PhotoAlbumsService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;

            mapperUserToUserDTO   = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()));
            mapperAlbumToAlbumDTO = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Album, AlbumDTO>()));
            mapperPhotoToPhotoDTO = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Photo, PhotoDTO>()));
            mapperUserDTOToUser   = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()));
            mapperAlbumDTOToAlbum = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<AlbumDTO, Album>()));
            //mapperPhotoDTOToPhoto = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<PhotoDTO, Photo>()));
            mapperPhotoDTOToPhoto = new MapperConfiguration(cfg => cfg.CreateMap<PhotoDTO, Photo>().
                //ForMember(dest => dest.User, opt => opt.NullSubstitute(null)).
                ForMember(dest => dest.Description, opt => opt.NullSubstitute(""))).CreateMapper();
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<User> users = Database.Users.GetAll();
            return mapperUserToUserDTO.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

        public IEnumerable<AlbumDTO> GetAlbums()
        {
            IEnumerable<Album> albums = Database.Albums.GetAll();
            return mapperAlbumToAlbumDTO.Map<IEnumerable<Album>, IEnumerable<AlbumDTO>>(albums);
        }

        public IEnumerable<PhotoDTO> GetPhotos()
        {
            IEnumerable<Photo> photos = Database.Photos.GetAll();
            Mapper.Initialize(cfg => cfg.CreateMap<Photo, PhotoDTO>());

            return mapperPhotoToPhotoDTO.Map<IEnumerable<Photo>, IEnumerable<PhotoDTO>>(photos);
        }

        public UserDTO GetUser(int id)
        {
            User user = Database.Users.Get(id);
            return mapperUserToUserDTO.Map<User, UserDTO>(user);
        }

        public AlbumDTO GetAlbum(int id)
        {
            Album album = Database.Albums.Get(id);
            return mapperAlbumToAlbumDTO.Map<Album, AlbumDTO>(album);
        }

        public PhotoDTO GetPhoto(int id)
        {
            Photo photo = Database.Photos.Get(id);
            return mapperPhotoToPhotoDTO.Map<Photo, PhotoDTO>(photo);
        }

        public IEnumerable<AlbumDTO> GetAlbumsOfUser(int id)
        {
            IEnumerable<Album> albums = Database.Albums.Find(album => album.UserId == id);
            return mapperAlbumToAlbumDTO.Map<IEnumerable<Album>, IEnumerable<AlbumDTO>>(albums);
        }

        public IEnumerable<PhotoDTO> GetPhotosOfUser(int id)
        {
            IEnumerable<Photo> photos = Database.Photos.Find(photo => photo.UserId == id);
            return mapperPhotoToPhotoDTO.Map<IEnumerable<Photo>, IEnumerable<PhotoDTO>>(photos);
        }

        public IEnumerable<PhotoDTO> GetPhotosOfAlbum(int id)
        {
            IEnumerable<Photo> photos = Database.Photos.Find(photo => photo.AlbumId == id);
            return mapperPhotoToPhotoDTO.Map<IEnumerable<Photo>, IEnumerable<PhotoDTO>>(photos);
        }

        public void CreateUser(UserDTO userDTO)
        {
            User user = mapperUserDTOToUser.Map<UserDTO, User>(userDTO);
            Database.Users.Create(user);
            SaveChangesInDatabase();
        }

        public void CreateAlbum(AlbumDTO albumDTO)
        {
            Album album = mapperAlbumDTOToAlbum.Map<AlbumDTO, Album>(albumDTO);
            Database.Albums.Create(album);
            SaveChangesInDatabase();
        }

        public void CreatePhoto(PhotoDTO photoDTO)
        {
            //Mapper.Initialize(cfg => cfg.CreateMap<PhotoDTO, Photo>());
            //Photo photo = Mapper.Map<PhotoDTO, Photo>(photoDTO);
            Photo photo = mapperPhotoDTOToPhoto.Map<PhotoDTO, Photo>(photoDTO);
            Database.Photos.Create(photo);
            SaveChangesInDatabase();
        }

        public void UpdateUser(UserDTO userDTO)
        {
            User user = mapperUserDTOToUser.Map<UserDTO, User>(userDTO);
            Database.Users.Update(user);
            SaveChangesInDatabase();
        }

        public void UpdateAlbum(AlbumDTO albumDTO)
        {
            Album album = mapperAlbumDTOToAlbum.Map<AlbumDTO, Album>(albumDTO);
            Database.Albums.Update(album);
            SaveChangesInDatabase();
        }

        public void UpdatePhoto(PhotoDTO photoDTO)
        {
            IMapper mapperPhotoDTOToPhoto = new MapperConfiguration(cfg => cfg.CreateMap<PhotoDTO, Photo>()).CreateMapper();
            Photo photo = mapperPhotoDTOToPhoto.Map<PhotoDTO, Photo>(photoDTO);
            Database.Photos.Update(photo);
            SaveChangesInDatabase();
        }

        private void SaveChangesInDatabase()
        {
            Database.Save();
        }
    }
}
