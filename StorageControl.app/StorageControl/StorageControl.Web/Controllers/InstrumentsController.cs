using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Web.Models.Instruments;
using System;
using System.Linq;
using System.Web.Mvc;

namespace StorageControl.Web.Controllers
{
    public class InstrumentsController : Controller
    {
        public IInstrumentsRepository InstrumentsRepository { get; set; }
        public ICategoriesRepository CategoriesRepository { get; set; }
        public IInstrumentTypesRepository InstrumentTypesRepository { get; set; }

        public InstrumentsController(IInstrumentsRepository instrumentsRepository,
            ICategoriesRepository categoriesRepository, IInstrumentTypesRepository instrumentTypesRepository)
        {
            this.InstrumentsRepository = instrumentsRepository;
            this.CategoriesRepository = categoriesRepository;
            this.InstrumentTypesRepository = instrumentTypesRepository;
        }
        
        // GET: StorageControl
        public ActionResult Index()
        {
            InstrumentsListModel model = new InstrumentsListModel();
            model.Instruments = InstrumentsRepository.List().ToList();

            return View(model);
        }

        // GET: StorageControl/Create
        public ActionResult Create()
        {
            InstrumentsCreateModel model = new InstrumentsCreateModel();
            model.Categories.AddRange(CategoriesRepository.List().ToList());
            model.InstrumentTypes.AddRange(InstrumentTypesRepository.List().ToList());

            return View(model);
        }

        // POST: StorageControl/Create
        [HttpPost]
        public ActionResult Create(InstrumentsCreateModel model)
        {
            try
            {
                // TODO: Add insert logic here
                int result = InstrumentsRepository.Create(model.Instrument);

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                throw e;
                //return View(model);
            }
        }

        // GET: StorageControl/Edit/5
        public ActionResult Edit(int id)
        {
            InstrumentsEditModel model = new InstrumentsEditModel();
            model.Instrument = InstrumentsRepository.Get(id);
            model.Categories.AddRange(CategoriesRepository.List().ToList());
            model.InstrumentTypes.AddRange(InstrumentTypesRepository.List().ToList());

            return View(model);
        }

        // POST: StorageControl/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, InstrumentsEditModel model)
        {
            try
            {
                model.Instrument.Id = id;
                int result = InstrumentsRepository.Update(model.Instrument);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: StorageControl/Delete/5
        public ActionResult Delete(int id)
        {
            InstrumentsDeleteModel model = new InstrumentsDeleteModel();
            model.Instrument = InstrumentsRepository.Get(id);

            return View(model);
        }

        // POST: StorageControl/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, InstrumentsDeleteModel model)
        {
            try
            {
                int result = InstrumentsRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
