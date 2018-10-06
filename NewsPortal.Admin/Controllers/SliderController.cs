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
            if(ModelState.IsValid)
            {
                if(imageURL.ContentLength > 0)
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
    }
}