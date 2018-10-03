using NewsPortal.Admin.CustomFilter;
using NewsPortal.Core.Infstructure;
using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsPortal.Admin.Controllers
{
    public class NewsController : Controller
    {
        #region Database
        private readonly INewsRepository _newsRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageRepository _imageRepository;
        #endregion

        public NewsController(INewsRepository newsRepository, IUserRepository userRepository, ICategoryRepository categoriRespository, IImageRepository imageRepository)
        {
            _newsRepository = newsRepository;
            _userRepository = userRepository;
            _categoryRepository = categoriRespository;
            _imageRepository = imageRepository;
        }

        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        [LoginFilter]
        [HttpGet]
        public ActionResult Insert()
        {
            ListCategories();
            return View();
        }


        [LoginFilter]
        [HttpPost]
        public ActionResult Insert(News news, int categoryID, HttpPostedFileBase showcaseImage, IEnumerable<HttpPostedFileBase> detailImages)
        {
            var sessionControl = HttpContext.Session["UserID"];

            if(ModelState.IsValid)
            {
                User user = _userRepository.GetById(Convert.ToInt32(sessionControl));
                news.UserID = user.ID;
                news.CategoryID = categoryID;

                if(showcaseImage != null)
                {
                    string fileName = Guid.NewGuid().ToString().Replace("-", "");
                    string extension = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string fullPath = "/External/News/" + fileName + extension;
                    Request.Files[0].SaveAs(Server.MapPath(fullPath));
                    news.ImageStr = fullPath;
                }
                _newsRepository.Insert(news);
                _newsRepository.Save();

                string multipleImages = System.IO.Path.GetExtension(Request.Files[1].FileName);
                if(detailImages != null)
                {
                    foreach (var file in detailImages)
                    {
                        if(file.ContentLength > 0)
                        {
                            string fileName = Guid.NewGuid().ToString().Replace("-", "");
                            string extension = System.IO.Path.GetExtension(Request.Files[1].FileName);
                            string fullPath = "/External/News/" + fileName + extension;
                            file.SaveAs(Server.MapPath(fullPath));

                            var image = new Image
                            {
                                ImageUrl = fullPath
                            };
                            image.NewsID = news.ID;

                            _imageRepository.Insert(image);
                            _imageRepository.Save();
                        }

                      
                    }
                }

                return View();
            }    

            return View();
        }

        #region List Categories
        public void ListCategories(object category = null)
        {
            var categoryList = _categoryRepository.GetMany(x => x.ParentID == 0).ToList();
            ViewBag.Category = categoryList;
        }
        #endregion
    }
}