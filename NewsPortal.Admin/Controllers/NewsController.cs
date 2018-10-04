using NewsPortal.Admin.CustomFilter;
using NewsPortal.Core.Infstructure;
using NewsPortal.Data.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
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
        [LoginFilter]
        public ActionResult Index(int page = 1)
        {
            var newsList = _newsRepository.GetAll();
            return View(newsList.OrderByDescending(x => x.ID).ToPagedList(page,20));
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
                if(multipleImages != "")
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
                TempData["Information"] = "News add operation is successful";
                return RedirectToAction("Index", "News");
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

        #region Delete
        public ActionResult Delete(int id)
        {
            News dbNews = _newsRepository.GetById(id);
            var dbDetailImages = _imageRepository.GetMany(x => x.NewsID == id);
            if(dbNews == null)
            {
                throw new Exception("News not found");
            }
         

            string fileName = dbNews.ImageStr;
            string filePath = Server.MapPath(fileName);
            FileInfo file = new FileInfo(filePath);
            if(file.Exists)
            {
                file.Delete();
            }
            
            if(dbDetailImages != null)
            {
                foreach (var item in dbDetailImages)
                {
                    string detailImagePath = Server.MapPath(item.ImageUrl);
                    FileInfo detailImage = new FileInfo(detailImagePath);
                    if(detailImage.Exists)
                    {
                        detailImage.Delete();
                    }
                }
            }

            _newsRepository.Delete(id);
            _newsRepository.Save();

            TempData["Information"] = "Image delete operation successful.";
            return RedirectToAction("Index", "News");
        }
        #endregion
    }
}