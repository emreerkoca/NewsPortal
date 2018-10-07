using NewsPortal.Admin.Class;
using NewsPortal.Admin.CustomFilter;
using NewsPortal.Core.Infstructure;
using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using NewsPortal.Admin.Helper;
using System.IO;

namespace NewsPortal.Admin.Controllers
{
    public class SliderController : Controller
    {
        private ISliderRepository _sliderRepository;

        public SliderController(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        // GET: Slider
        [LoginFilter]
        public ActionResult Index(int page = 1)
        {
            var slider = _sliderRepository.GetAll().OrderByDescending(x => x.ID).ToPagedList(page,10);
            return View(slider);
        }

        #region Insert
        [LoginFilter]
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [LoginFilter]
        [HttpPost]
        public JsonResult Insert(Slider slider, HttpPostedFileBase imageURL)
        {
            if (ModelState.IsValid)
            {
                if (imageURL.ContentLength > 0)
                {
                    string file = Guid.NewGuid().ToString().Replace("-", "");
                    string ext = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string imgPath = "/External/Slider/" + file + ext;

                    imageURL.SaveAs(Server.MapPath(imgPath));
                }
                _sliderRepository.Insert(slider);
                try
                {
                    _sliderRepository.Save();
                    return Json(new ResultJson { Success = false, Message = "Slider add operation successful!" });

                }
                catch (Exception)
                {
                    return Json(new ResultJson { Success = false, Message = "An error occured add slider process!" });
                }
            }

            return Json(new ResultJson { Success = false, Message = "An error occured add slider process!" });
        }
        #endregion

        #region Edit
        [HttpGet]
        [LoginFilter]
        public ActionResult Edit(int ID)
        {
            var slider = _sliderRepository.GetById(ID);
            if (slider != null)
            {
                return View(slider);
            }
            return View();
        } 
 
        [HttpPost]
        [LoginFilter]
        public ActionResult Edit(Slider slider, HttpPostedFileBase imageURL)
        {
            if(ModelState.IsValid)
            {
                Slider dbSlider = _sliderRepository.GetById(slider.ID);
                dbSlider.IsActive = slider.IsActive;
                dbSlider.Title = slider.Title;
                dbSlider.Description = slider.Description;
                dbSlider.UploadDate = slider.UploadDate;
                dbSlider.Url = slider.Url;
                if(imageURL != null && imageURL.ContentLength > 0)
                {
                    if(dbSlider.ImageURL != null)
                    {
                        string URL = dbSlider.ImageURL;
                        string imagePath = Server.MapPath(URL);
                        FileInfo files = new FileInfo(imagePath);
                        if(files.Exists)
                        {
                            files.Delete();
                        }
                    }

                    ImageUpload.Image(imageURL, slider);
                    dbSlider.ImageURL = slider.ImageURL;
                }

                try
                {
                    _sliderRepository.Save();
                    return Json(new ResultJson { Success = true, Message = "Slider edit operation successful" });
                }
                catch (Exception)
                {
                    return Json(new ResultJson { Success = false, Message = "An error occured while slider edit operation" });
                }
            }
            return Json(new ResultJson { Success = false, Message = "An error occured while slider edit operation" });
        }
        #endregion

        #region Delete
        public JsonResult Delete(Slider slider)
        {
            var dbSlider = _sliderRepository.GetById(slider.ID);
            if(dbSlider == null)
            {
                return Json(new ResultJson { Success = false, Message = "Slider not found." });
            }

            try
            {
                if (dbSlider.ImageURL != null)
                {
                    string URL = dbSlider.ImageURL;
                    string imagePath = Server.MapPath(URL);
                    FileInfo files = new FileInfo(imagePath);
                    if (files.Exists)
                    {
                        files.Delete();
                    }
                }
                _sliderRepository.Delete(slider.ID);
                _sliderRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Slider deleted." });
            }
            catch (Exception)
            {
                return Json(new ResultJson { Success = false, Message = "An error occured while deleting slider!" });
            }
        }
        #endregion
    }
}