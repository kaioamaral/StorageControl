using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Web.Controllers.Base;
using StorageControl.Web.Models.Categories;
using System.Linq;
using System.Web.Mvc;

namespace StorageControl.Web.Controllers
{
    public class CategoriesController : BaseController
    {
        public ICategoriesRepository CategoriesRepository { get; set; }

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            this.CategoriesRepository = categoriesRepository;
        }

        public ViewResult Index()
        {
            CategoriesListModel model = new CategoriesListModel();
            model.Categories = CategoriesRepository.List().ToList();

            return View(model);
        }
        
        public ViewResult Create()
        {
            CategoriesCreateModel model = new CategoriesCreateModel();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CategoriesCreateModel model)
        {
            ValidateModel(model);
            if (ModelState.IsValid)
            {
                try
                {
                    int result = CategoriesRepository.Create(model.Category);

                    if (result > 0)
                    {
                        Success(string.Format("Categoria '{0}' criada. :)",
                        model.Category.Name));

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Warning("Houve um problema ao processar sua requisição. :( Tente novamente.");
                        return RedirectToAction("Create");
                    }
                }
                catch
                {
                    Error("Oops! Ocorreu um erro ao processar sua requisição. ;( Tente novamente.");
                    return RedirectToAction("Create");
                }
            }
            else
            {
                Warning(BuildErrorMessage(GetErrors()));
                return RedirectToAction("Create");
            }
        }

        public ViewResult Edit(int id)
        {
            CategoriesEditModel model = new CategoriesEditModel();

            if (id > 0)
            {
                model.Category = CategoriesRepository.Get(id);
                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, CategoriesEditModel model)
        {
            ValidateModel(id, model);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Category.Id = id;
                    int result = CategoriesRepository.Update(model.Category);

                    if (result > 0)
                    {

                        Success("Categoria atualizada.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Warning("Houve um problema ao processar sua requisição. :( Tente novamente");
                        return View(model);
                    }
                }
                catch
                {
                    Error("Ocorreu um erro ao processar sua requisição. ;( Tente novamente.");
                    return View("Error");
                }
            }
            else
            {
                Warning(BuildErrorMessage(GetErrors()));
                return RedirectToAction("Edit", model);
            }
        }

        public ViewResult Delete(int id)
        {
            if (id > 0)
            {
                CategoriesDeleteModel model = new CategoriesDeleteModel();
                model.Category = CategoriesRepository.Get(id);

                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, CategoriesDeleteModel model)
        {
            if (id > 0)
            {
                try
                {
                    int result = CategoriesRepository.Delete(id);

                    if (result > 0)
                    {
                        Success(string.Format("Categoria {0} excluída.", model.Category.Name));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Warning("Houve um problema ao processar sua requisição. :( Tente novamente");
                        return RedirectToAction("Delete", new { id = id });
                    }
                }
                catch
                {
                    Error("Ocorreu um erro ao processar sua requisição. ;( Tente novamente.");
                    return RedirectToAction("Delete", model);
                }
            }
            else
            {
                Error("Categoria inexistente.");
                return View("Error");
            }
        }
        
        #region [Model Validation]
        private void ValidateModel(CategoriesCreateModel model)
        {
            if (model.Category.Name == string.Empty || model.Category.Name == null)
            {
                ModelState.AddModelError("Name",
                    "Parece que o campo Nome não foi preenchido. :( Preencha-o e tente novamente.");
            }
        }

        private void ValidateModel(int id, CategoriesEditModel model)
        {
            if (id < 0)
            {
                ModelState.AddModelError("Id", "Categoria inexistente. :(");
            }
            else if (model.Category.Name == string.Empty || model.Category.Name == null)
            {
                ModelState.AddModelError("Name",
                    "Parece que o campo Nome não foi preenchido. :( Preencha-o e tente novamente.");
            }
        }
        #endregion
    }
}
