using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Web.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StorageControl.Web.Controllers
{
    public class CategoriesController : Controller
    {
        public ICategoriesRepository CategoriesRepository { get; set; }

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            this.CategoriesRepository = categoriesRepository;
        }

        // GET: Categories
        public ViewResult Index()
        {
            CategoriesListModel model = new CategoriesListModel();
            model.Categories = CategoriesRepository.List().ToList();

            return View(model);
        }
        
        // GET: Categories/Create
        public ViewResult Create()
        {
            CategoriesCreateModel model = new CategoriesCreateModel();
            
            return View(model);
        }

        // POST: Categories/Create
        [HttpPost]
        public ViewResult Create(CategoriesCreateModel model)
        {
            try
            {
                int result = CategoriesRepository.Create(model.Category);
                return View("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            CategoriesEditModel model = new CategoriesEditModel();
            model.Category = CategoriesRepository.Get(id);

            return View(model);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CategoriesEditModel model)
        {
            try
            {
                model.Category.Id = id;
                int result = CategoriesRepository.Update(model.Category);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categories/Delete/5
        public ViewResult Delete(int id)
        {
            CategoriesDeleteModel model = new CategoriesDeleteModel();
            model.Category = CategoriesRepository.Get(id);

            return View(model);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CategoriesDeleteModel model)
        {
            try
            {
                int result = CategoriesRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
