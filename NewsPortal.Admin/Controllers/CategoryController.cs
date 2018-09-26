using NewsPortal.Admin.Class;
using NewsPortal.Core.Infstructure;
using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsPortal.Admin.Controllers
{
    public class CategoryController : Controller
    {
        #region Category
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #endregion

        public ActionResult Index()
        {
            return View(_categoryRepository.GetAll().ToList());
        }

        #region Insert Category
        [HttpGet]
        public ActionResult Insert()
        {
            SetCategoryList();
            return View();
        }

        [HttpPost]
        public JsonResult Insert(Category category)
        {
            try
            {
                _categoryRepository.Insert(category);
                _categoryRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Category add operation successful." });
            }
            catch (Exception ex)
            {
                return Json(new ResultJson { Success = false, Message = "Category add operation don' t successful." });
            }
        } 
        #endregion

        #region Set Category
        public void SetCategoryList()
        {
            var categoryList = _categoryRepository.GetMany(x => x.ParentID == 0).ToList();
            ViewBag.CategoryList = categoryList;
        }
        #endregion

        #region Delete Category
        public ActionResult Delete(int ID)
        {
            Category deleteCategory = _categoryRepository.GetById(ID);
            if(deleteCategory == null)
            {
                throw new Exception("Category not found");
            }
            _categoryRepository.Delete(ID);
            _categoryRepository.Save();

            return RedirectToAction("Index", "Category");
        }
        #endregion
    }
}