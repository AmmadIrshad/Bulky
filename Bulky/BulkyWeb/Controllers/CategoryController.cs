using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Security.AccessControl;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        //applicationDbContext is needed in order retrive data from database
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //retreiving list of categories from db
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        //create new category (for displaying category page)
        //not defined then its GetActionMethod
        public IActionResult Create()
        {
            //can return new Category
            return View();
        }
        [HttpPost]
        //httpPost for submit form that form will return an obj to added
        public IActionResult Create(Category obj)
        {
            //Custom validation
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the string");
            }
            //key is not defined on show in summary
            if (obj.Name!=null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value");
            }

            //if block will check server side validation
            if (ModelState.IsValid) { 
                _db.Categories.Add(obj);
                _db.SaveChanges();
                //temp data for displaying message
                TempData["success"] = "Category Created Successfully";
                //now redirect to the categorylist instead of adding category
                return RedirectToAction("Index", "Category");
            }
            else { return View(); }
           
        }


        //Edit Category
        //edit based on that id which passed through edit button
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Category? categoryfromDb = _db.Categories.Find(id);
            //first or default works on other prop too like name
            //Category? categoryfromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //if need some filtering
            //Category? categoryfromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryfromDb == null)
            {
                return NotFound();
            }
            return View(categoryfromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index", "Category");
            }
            else { return View(); }

        }

        //Delete a category
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryfromDb = _db.Categories.Find(id);
            if (categoryfromDb == null)
            {
                return NotFound();
            }
            return View(categoryfromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index", "Category");
           

        }
    }
}