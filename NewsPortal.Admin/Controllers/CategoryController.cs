using NewsPortal.Admin.Class;
using NewsPortal.Core.Infstructure;
using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using NewsPortal.Admin.CustomFilter;

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

        #region List Category
        public ActionResult Index(int page = 1)
        {
            return View(_categoryRepository.GetAll().OrderByDescending(x => x.ID).ToPagedList(page, 10));
        } 
        #endregion

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
        public JsonResult Delete(int ID)
        {
            Category deleteCategory = _categoryRepository.GetById(ID);
            if(deleteCategory == null)
            {
                return Json(new ResultJson { Success = false, Message = "Category not found!" });
            }
            _categoryRepository.Delete(ID);
            _categoryRepository.Save();

            return Json(new ResultJson { Success = true, Message = "Delete category operation is successful!" });
        }
        #endregion

        #region Update Category
        [HttpGet]
        [LoginFilter]
        public ActionResult Update(int id)
        {
            Category dbCategory = _categoryRepository.GetById(id);
            if(dbCategory == null)
            {
                throw new Exception("Category not found");
            }
            SetCategoryList();
            return View(dbCategory);
        }

        [HttpPost]
        [LoginFilter]
        public JsonResult Update(Category category)
        {
            //if(ModelState.IsValid)
            //{
                Category dbCategory = _categoryRepository.GetById(category.ID);
                dbCategory.IsActive = category.IsActive;
                dbCategory.CategoryName = category.CategoryName;
                dbCategory.ParentID = category.ParentID;
                dbCategory.URL = category.URL;
                _categoryRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Update category operation successful!" });
            //}
            //return Json(new ResultJson { Success = false, Message = "An error occured. Update category operation unsuccessful!" });
        }
        #endregion
    }
}