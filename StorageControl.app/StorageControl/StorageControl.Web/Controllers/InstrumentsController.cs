using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Web.Controllers.Base;
using StorageControl.Web.Models.Instruments;
using System;
using System.Linq;
using System.Web.Mvc;

namespace StorageControl.Web.Controllers
{
    public class InstrumentsController : BaseController
    {
        public IInstrumentsRepository InstrumentsRepository { get; set; }
        public ICategoriesRepository CategoriesRepository { get; set; }
        public IInstrumentTypesRepository InstrumentTypesRepository { get; set; }

        public InstrumentsController(IInstrumentsRepository instrumentsRepository,
            ICategoriesRepository categoriesRepository,
            IInstrumentTypesRepository instrumentTypesRepository)
        {
            this.InstrumentsRepository = instrumentsRepository;
            this.CategoriesRepository = categoriesRepository;
            this.InstrumentTypesRepository = instrumentTypesRepository;
        }
        
        public ActionResult Index()
        {
            InstrumentsListModel model = new InstrumentsListModel();
            model.Instruments = InstrumentsRepository.List().ToList();

            return View(model);
        }
        
        public ActionResult Create()
        {
            InstrumentsCreateModel model = new InstrumentsCreateModel();
            model.Categories.AddRange(CategoriesRepository.List().ToList());
            model.InstrumentTypes.AddRange(InstrumentTypesRepository.List().ToList());

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(InstrumentsCreateModel model)
        {
            ValidateModel(model);
            if (ModelState.IsValid)
            {
                try
                {
                    int result = InstrumentsRepository.Create(model.Instrument);

                    Success(string.Format("Instrumento '{0} - {1}' criado. ;)",
                        model.Instrument.Manufacturer, model.Instrument.Model));
                    return RedirectToAction("Index");
                }
                catch
                {
                    Error("Ocorreu um erro ao processar sua requisição. ;( Tente novamente.");
                    model.Categories = CategoriesRepository.List().ToList();
                    model.InstrumentTypes = InstrumentTypesRepository.List().ToList();
                    return View(model);
                }
            }
            else
            {
                Warning("Parece que campos não foram preenchidos. ;( Tente novamente.");
                return RedirectToAction("Create");
            }
        }

        public ActionResult Edit(int id)
        {
            InstrumentsEditModel model = new InstrumentsEditModel();
            model.Instrument = InstrumentsRepository.Get(id);
            model.Categories.AddRange(CategoriesRepository.List().ToList());
            model.InstrumentTypes.AddRange(InstrumentTypesRepository.List().ToList());

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, InstrumentsEditModel model)
        {
            ValidateModel(model);

            if (ModelState.IsValid)
            {
                try
                {
                    model.Instrument.Id = id;
                    int result = InstrumentsRepository.Update(model.Instrument);

                    Success(string.Format("Instrumento '{0} - {1}' atualizado. :)",
                        model.Instrument.Manufacturer, model.Instrument.Model));
                    return RedirectToAction("Index");
                }
                catch
                {
                    Error("Houve um erro ao processar sua requisição. :( Tente novamente.");
                    model.Categories = CategoriesRepository.List().ToList();
                    model.InstrumentTypes = InstrumentTypesRepository.List().ToList();
                    return View(model);
                }
            }
            else
            {
                Warning("Parece que alguns campos não foram preenchidos. :( Tente novamente.");
                model.Categories = CategoriesRepository.List().ToList();
                model.InstrumentTypes = InstrumentTypesRepository.List().ToList();

                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                InstrumentsDeleteModel model = new InstrumentsDeleteModel();
                model.Instrument = InstrumentsRepository.Get(id);

                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, InstrumentsDeleteModel model)
        {
            ValidateModel(id);

            if (ModelState.IsValid)
            {
                try
                {
                    model.Instrument = InstrumentsRepository.Get(id);
                    int result = InstrumentsRepository.Delete(id);

                    Success(string.Format("Instrumento '{0} - {1}' excluído. :)",
                        model.Instrument.Manufacturer, model.Instrument.Model));

                    return RedirectToAction("Index");
                }
                catch
                {
                    Error("Ocorreu um erro ao processar sua requisição. :(");
                    return View("Error");
                }
            }
            else
            {
                Error("Ocorreu um erro ao processar sua requisição. :(");
                return View("Error");
            }
        }

        #region [Model Validation]

        public void ValidateModel(InstrumentsCreateModel model)
        {
            var instrument = model.Instrument;

            if (instrument.Manufacturer == null || instrument.Manufacturer == string.Empty)
            {
                ModelState.AddModelError("Null Manufacturer", new ArgumentNullException());
            }
            if (instrument.Model == null || instrument.Model == string.Empty)
            {
                ModelState.AddModelError("Null model", new ArgumentNullException());
            }
            if (instrument.Amount < 0)
            {
                ModelState.AddModelError("Invalid amount", new ArgumentOutOfRangeException());
            }
            if (instrument.UnitPrice < 0)
            {
                ModelState.AddModelError("Invalid unit price", new ArgumentOutOfRangeException());
            }
            if (instrument.Type.Id <= 0)
            {
                ModelState.AddModelError("Invalid Instrument Type", new ArgumentException());
            }
            if (instrument.Category.Id <= 0)
            {
                ModelState.AddModelError("Invalid Category", new ArgumentException());
            }
        }

        public void ValidateModel(InstrumentsEditModel model)
        {
            var instrument = model.Instrument;

            if (instrument.Manufacturer == null || instrument.Manufacturer == string.Empty)
            {
                ModelState.AddModelError("Null Manufacturer", new ArgumentNullException());
            }
            if (instrument.Model == null || instrument.Model == string.Empty)
            {
                ModelState.AddModelError("Null model", new ArgumentNullException());
            }
            if (instrument.Amount < 0)
            {
                ModelState.AddModelError("Invalid amount", new ArgumentOutOfRangeException());
            }
            if (instrument.UnitPrice < 0)
            {
                ModelState.AddModelError("Invalid unit price", new ArgumentOutOfRangeException());
            }
            if (instrument.Type.Id <= 0)
            {
                ModelState.AddModelError("Invalid Instrument Type", new ArgumentException());
            }
            if (instrument.Category.Id <= 0)
            {
                ModelState.AddModelError("Invalid Category", new ArgumentException());
            }
        }

        public void ValidateModel(int id)
        {
            if(id <= 0)
            {
                ModelState.AddModelError("Invalid id", new ArgumentOutOfRangeException());
            }
        }

        #endregion
    }
}
