using NLayer.MVC_PAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.IO;
using NLayer.MVC_PAL.Util;
using System.Net;
using NLayer.BLL.DTO;
using NLayer.BLL.Interfaces;

namespace NLayer.MVC_PAL.Controllers
{
    public class AccountController : Controller
    {
        private IPhotoAlbumsService _photoAlbumsService;
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public AccountController(IPhotoAlbumsService photoAlbumsService)
        {
            _photoAlbumsService = photoAlbumsService;
        }

        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null && uploadImage.ContentLength > 0)
                {
                    string extension = Path.GetExtension(uploadImage.FileName).ToLower();
                    if (!(extension == ".jpg" || extension == ".jpeg" ||
                        extension == ".png" || extension == ".gif"))
                    {
                        ViewBag.Message = "Error. You can upload just image files.";
                        return View(model);
                    }      
                }

                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "user");
                    await SignInAsync(user, isPersistent: false);

                    string pathToAvatar = SaveAvatarPhoto(uploadImage);

                    UserDTO userDTO = new UserDTO() { Name = model.Email, PathToAvatar = pathToAvatar, FullName = model.Name };
                    _photoAlbumsService.CreateUser(userDTO);

                    return RedirectToAction("MyPhoto", "Home");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Login or password is wrong");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private string SaveAvatarPhoto(HttpPostedFileBase uploadImage)
        {
            string pathToAvatar = "";

            if (uploadImage != null)
            {
                string extension = Path.GetExtension(uploadImage.FileName).ToLower();

                string PathToFolderRelative = "/PhotoStorage/Avatars";
                string PathToFolder = Server.MapPath(PathToFolderRelative);

                if (!Directory.Exists(PathToFolder))
                {
                    Directory.CreateDirectory(PathToFolder);
                }

                string fileName = "" + Guid.NewGuid() + extension;
                string pathToFile = Path.Combine(PathToFolder, fileName);

                uploadImage.SaveAs(pathToFile);

                try
                {
                    ImageHelper imHelper = new ImageHelper();
                    imHelper.CreateThumbFile(pathToFile, pathToFile);
                    pathToAvatar = PathToFolderRelative + "/" + fileName;
                }
                catch (Exception ex)
                {
                    //Log an error     
                    throw ex;
                }
            }

            return pathToAvatar;
        }
    }
}