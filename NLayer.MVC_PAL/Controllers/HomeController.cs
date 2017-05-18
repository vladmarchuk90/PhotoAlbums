using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLayer.BLL.Interfaces;
using NLayer.BLL.DTO;
using NLayer.BLL.Infrastructure;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using NLayer.MVC_PAL.Util;
using System.Net;

namespace NLayer.MVC_PAL.Controllers
{
    public class HomeController : Controller
    {
        private IPhotoAlbumsService _photoAlbumsService;

        public HomeController(IPhotoAlbumsService photoAlbumsService)
        {
            _photoAlbumsService = photoAlbumsService;
        }

        // GET: Index
        public ActionResult Index(string orderBy = "date")
        {
            Func<AlbumDTO, int> sorting = null;
            if(orderBy == "date")
            {
                sorting = new Func<AlbumDTO, int>(al => al.Id);
                @ViewBag.Title = "The newest albums";
            }
            else
            {
                @ViewBag.Title = "The most popular albums";
                sorting = new Func<AlbumDTO, int>(al => al.amountOfLikes);
            }

            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;

            IEnumerable<AlbumDTO> albums = _photoAlbumsService.GetAlbums().OrderByDescending(sorting);
            return View(albums);
        }

        // GET: Photos
        public ActionResult Photos(int albumId)
        {
            AlbumDTO album = _photoAlbumsService.GetAlbum(albumId);
            ViewBag.Album = album;

            IEnumerable<PhotoDTO> photos = _photoAlbumsService.GetPhotosOfAlbum(albumId);
            return View(photos);
        }

        // GET: UploadPhoto
        public ActionResult UploadPhoto(int userId, int albumId)
        {
            ViewBag.AlbumId = albumId;
            ViewBag.UserId = userId;
            return View();
        }

        // POST: UploadPhoto
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public ActionResult UploadPhoto(PhotoDTO photo, HttpPostedFileBase uploadImage, string albumName = null, string albumDescription = null)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                string pathToFile = "";

                if (uploadImage.ContentLength > 0)
                {
                    string extension = Path.GetExtension(uploadImage.FileName).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" ||
                        extension == ".png" || extension == ".gif")
                    {
                        string userName = User.Identity.GetUserName();

                        string PathToFolderRelative = "/PhotoStorage/" + userName;
                        string PathToFolder = Server.MapPath(PathToFolderRelative);

                        if (!Directory.Exists(PathToFolder))
                        {
                            Directory.CreateDirectory(PathToFolder);
                        }

                        string fileName = "" + Guid.NewGuid() + extension;
                        string fileNameThumb = "thumb_" + fileName;

                        pathToFile = Path.Combine(PathToFolder, fileName);
                        string pathToFileThumb = Path.Combine(PathToFolder, fileNameThumb);

                        uploadImage.SaveAs(pathToFile);

                        //thumb
                        try
                        {
                            ImageHelper imHelper = new ImageHelper();
                            imHelper.CreateThumbFile(pathToFile, pathToFileThumb);
                        }
                        catch (Exception ex)
                        {
                            //Log an error     
                            return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                        }

                        photo.DateUploaded = DateTime.Now;
                        photo.UserId = photo.UserId;
                        photo.AlbumId = photo.AlbumId;
                        photo.Title = photo.Title;
                        photo.Description = photo.Description;
                        photo.PathToPhoto = PathToFolderRelative + "/" + fileName;
                        photo.PathToThumbPhoto = PathToFolderRelative + "/" + fileNameThumb;
                        photo.amountOfLikes = 0;

                        _photoAlbumsService.CreatePhoto(photo);

                        ViewBag.UploadingSuccess = true;
                        ViewBag.UploadingMessage = "file is uploaded successfully!";
                    }
                    else
                    {
                        ViewBag.UploadingSuccess = false;
                        ViewBag.UploadingMessage = "You can choose just photo file!";
                    }
                }
            }
            else
            {
                ViewBag.UploadingSuccess = false;
                ViewBag.UploadingMessage = "You didn't choose photo!";
            }

            Session["UploadingSuccess"] = ViewBag.UploadingSuccess;
            Session["UploadingMessage"] = ViewBag.UploadingMessage;

            return RedirectToAction("EditAlbum", "Home", new { albumId = photo.AlbumId, userId = photo.UserId, name = albumName, description = albumDescription });
        }

        // GET: About
        public ActionResult About()
        {
            return View();
        }

        // GET: MyPhoto
        [Authorize(Roles = "admin, user")]
        public ActionResult MyPhoto()
        {
            string name = User.Identity.Name;

            ViewBag.Admin = User.IsInRole("admin");

            UserDTO user = _photoAlbumsService.GetUser(name);
            if (user == null)
                return RedirectToAction("Login", "Account");

            return View(user);
        }

        // GET: PhotosOfUser (Partial)
        public ActionResult PhotosOfUser(int userId)
        {
            IEnumerable<PhotoDTO> photos = _photoAlbumsService.GetPhotosOfUser(userId);
            return PartialView(photos);
        }

        // GET: AlbumsOfUsers (Partial)
        public ActionResult AlbumsOfUser(int userId)
        {
            ViewBag.UserId = userId;
            IEnumerable<AlbumDTO> albums = _photoAlbumsService.GetAlbumsOfUser(userId);
            return PartialView(albums);
        }

        // GET: PhotosOfAlbum (Partial)
        public ActionResult PhotosOfAlbum(int albumId)
        {
            IEnumerable<PhotoDTO> photos = _photoAlbumsService.GetPhotosOfAlbum(albumId);
            return PartialView("PhotosOfUser", photos);
        }

        // GET: CreateAlbum
        public ActionResult CreateAlbum(int userId)
        {
            ViewBag.UserId = userId;
            return View();
        }

        // POST: CreateAlbum
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public ActionResult CreateAlbum(AlbumDTO albumDTO)
        {
            if(ModelState.IsValid)
            {
                albumDTO.amountOfLikes = 0;
                _photoAlbumsService.CreateAlbum(albumDTO);
            }
            return RedirectToAction("EditAlbum", "Home", new { albumId = albumDTO.Id, userId = albumDTO.UserId, name = albumDTO.Name, description = albumDTO.Description });
        }

        // GET: EditAlbum
        public ActionResult EditAlbum(int albumId, int userId, string name = null, string description = null)
        {
            ViewBag.AlbumId = albumId;
            ViewBag.UserId = userId;
            ViewBag.Name = name;
            ViewBag.Description = description;

            ViewBag.UploadingSuccess = Session["UploadingSuccess"];
            ViewBag.UploadingMessage = Session["UploadingMessage"];

            Session["UploadingSuccess"] = null;
            Session["UploadingMessage"] = null;

            IEnumerable<PhotoDTO> photos = _photoAlbumsService.GetPhotosOfAlbum(albumId);

            return View(photos);
        }

        // POST: PutAlbumLike
        [HttpPost]
        public ActionResult PutAlbumLike(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                int albumId = int.Parse(id);

                AlbumDTO album = _photoAlbumsService.GetAlbum(albumId);

                if(album != null)
                {
                    album.amountOfLikes += 1;
                    _photoAlbumsService.UpdateAlbum(album);
                }

                return Content("" + album.amountOfLikes);
            }
            return new EmptyResult();
        }
    }
}