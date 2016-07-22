using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Web.Models.InstrumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StorageControl.Web.Controllers
{
    public class InstrumentTypesController : Controller
    {
        public IInstrumentTypesRepository InstrumentTypesRepository { get; set; }

        public InstrumentTypesController(IInstrumentTypesRepository instrumentTypesRepository)
        {
            this.InstrumentTypesRepository = instrumentTypesRepository;
        }

        // GET: InstrumentTypes
        public ActionResult Index()
        {
            InstrumentTypesListModel model = new InstrumentTypesListModel();
            model.InstrumentTypes.AddRange(InstrumentTypesRepository.List().ToList());

            return View(model);
        }

        // GET: InstrumentTypes/Create
        public ActionResult Create()
        {
            InstrumentTypesCreateModel model = new InstrumentTypesCreateModel();
            return View(model);
        }

        // POST: InstrumentTypes/Create
        [HttpPost]
        public ActionResult Create(InstrumentTypesCreateModel model)
        {
            try
            {
                int result = InstrumentTypesRepository.Create(model.InstrumentType);

                if (result > 0)
                {
                    //show success alert
                }
                else
                {
                    //show warning alert
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: InstrumentTypes/Edit/5
        public ActionResult Edit(int id)
        {
            InstrumentTypesEditModel model = new InstrumentTypesEditModel();
            model.InstrumentType = InstrumentTypesRepository.Get(id);

            return View(model);
        }

        // POST: InstrumentTypes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, InstrumentTypesEditModel model)
        {
            try
            {
                model.InstrumentType.Id = id;
                int result = InstrumentTypesRepository.Update(model.InstrumentType);

                if (result > 0)
                {
                    //show success result
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: InstrumentTypes/Delete/5
        public ActionResult Delete(int id)
        {
            InstrumentTypesDeleteModel model = new InstrumentTypesDeleteModel();
            model.InstrumentType = InstrumentTypesRepository.Get(id);

            return View(model);
        }

        // POST: InstrumentTypes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, InstrumentTypesDeleteModel model)
        {
            try
            {
                model.InstrumentType.Id = id;

                int result = InstrumentTypesRepository.Delete(model.InstrumentType.Id);

                if (result > 0)
                {
                    //show success message
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}
