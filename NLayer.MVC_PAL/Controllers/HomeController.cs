using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLayer.BLL.Interfaces;
using NLayer.BLL.DTO;
using NLayer.BLL.Infrastructure;
using System.IO;

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
        public ActionResult Index()
        {
            //IEnumerable<PhotoDTO> photos = _photoAlbumsService.GetPhotos();
            return View();
        }

        // Create: Index
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PhotoDTO photo, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                string pathToFile = "";

                if(uploadImage.ContentLength > 0)
                {
                    string extension = Path.GetExtension(uploadImage.FileName).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" ||
                        extension == ".png" || extension == ".gif")
                    {
                        string PathTofolder = Server.MapPath("~/PhotoStorage/Album3");
                        if(!Directory.Exists(PathTofolder))
                        {
                            Directory.CreateDirectory(PathTofolder);
                        }

                        pathToFile = Path.Combine(PathTofolder, Guid.NewGuid() + extension);
                        uploadImage.SaveAs(pathToFile);

                        ViewBag.UploadSuccess = true;
                    }
                }
                photo.DateUploaded = DateTime.Now;
                photo.UserId = 1;
                photo.AlbumId = 1;
                photo.PathToPhoto = pathToFile;

                _photoAlbumsService.CreatePhoto(photo);

                return RedirectToAction("Index");
            }
            return View(photo);
        }

        // GET: About
        public ActionResult About()
        {
            return View();
        }

        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }
    }
}