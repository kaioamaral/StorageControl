using Moq;
using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Domain.Model.Entities;
using StorageControl.Web.Controllers;
using StorageControl.Web.Models.InstrumentTypes;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace StorageControl.Tests.Unit.InstrumentTypes
{
    public class InstrumentTypesControllerTests
    {
        [Fact]
        public void Index()
        {
            var mockedInstrumentTypesRepository = new Mock<IInstrumentTypesRepository>();
            var instrumentTypesListTemplate = new List<InstrumentType>();

            mockedInstrumentTypesRepository.Setup(k => k.List())
                .Returns(instrumentTypesListTemplate)
                .Verifiable();

            var controller = new InstrumentTypesController(mockedInstrumentTypesRepository.Object);

            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
            Assert.IsType<InstrumentTypesListModel>(result.Model);

            var receivedModel = result.Model as InstrumentTypesListModel;

            Assert.Equal(instrumentTypesListTemplate.Count, receivedModel.InstrumentTypes.Count);

            mockedInstrumentTypesRepository.VerifyAll();
        }

        [Fact]
        public void Create()
        {
            var mockedInstrumentTypesRepository = new Mock<IInstrumentTypesRepository>();
            var controller = new InstrumentTypesController(mockedInstrumentTypesRepository.Object);

            var result = controller.Create();
            Assert.IsType<InstrumentTypesCreateModel>(result.Model);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Whatever")]
        public void CreateConfirm(string name)
        {
            var mockedInstrumentTypesRepository = new Mock<IInstrumentTypesRepository>();
            var modelTemplate = new InstrumentTypesCreateModel() { InstrumentType = new InstrumentType { Name = name } };

            mockedInstrumentTypesRepository.Setup(k => k.Create(new InstrumentType()))
                .Returns(It.IsAny<int>());

            var controller = new InstrumentTypesController(mockedInstrumentTypesRepository.Object);
            var result = controller.Create(modelTemplate);

            if (name != null && name != "")
            {
                Assert.IsType<RedirectToRouteResult>(result);
            }
            else
            {
                Assert.IsType<ViewResult>(result as ViewResult);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        public void Edit(int id)
        {
            var mockedInstrumentTypesRepository = new Mock<IInstrumentTypesRepository>();
            var instrumentTypeTemplate = new InstrumentType() { Id = id, Name = It.IsAny<string>()};

            mockedInstrumentTypesRepository.Setup(k => k.Get(id))
                .Returns(instrumentTypeTemplate)
                .Verifiable();

            var controller = new InstrumentTypesController(mockedInstrumentTypesRepository.Object);
            var result = controller.Edit(id);
            Assert.IsType<ViewResult>(result);

            if (id > 0)
            {
                Assert.IsType<InstrumentTypesEditModel>((result as ViewResult).Model);
                var receivedModel = ((result as ViewResult).Model) as InstrumentTypesEditModel;

                Assert.Equal(instrumentTypeTemplate.Id, receivedModel.InstrumentType.Id);
                Assert.Equal(instrumentTypeTemplate.Name, receivedModel.InstrumentType.Name);
                mockedInstrumentTypesRepository.VerifyAll();
            }
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(1, null)]
        [InlineData(0, "")]
        [InlineData(1, "")]
        [InlineData(0, null)]
        [InlineData(1, "Whatever")]
        public void EditConfirm(int id, string name)
        {
            var mockedInstrumentTypesRepository = new Mock<IInstrumentTypesRepository>();
            var instrumentTypeTemplate = new InstrumentType() { Id = id, Name = name };
            var modelTemplate = new InstrumentTypesEditModel()
            { InstrumentType = instrumentTypeTemplate };

            mockedInstrumentTypesRepository.Setup(k => k.Update(instrumentTypeTemplate))
                .Returns(It.IsAny<int>());

            var controller = new InstrumentTypesController(mockedInstrumentTypesRepository.Object);
            var result = controller.Edit(id, modelTemplate);

            if (id > 0 && name != null && name != string.Empty)
            {
                Assert.IsType<RedirectToRouteResult>(result);
            }
            else
            {
                Assert.IsType<ViewResult>(result);
                Assert.Null((result as ViewResult).Model);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        public void Delete(int id)
        {
            var mockedInstrumentTypesRepository = new Mock<IInstrumentTypesRepository>();
            var instrumentTypeTemplate = new InstrumentType();
            var modelTemplate = new InstrumentTypesDeleteModel()
            { InstrumentType = instrumentTypeTemplate };

            mockedInstrumentTypesRepository.Setup(k => k.Get(id))
                .Returns(instrumentTypeTemplate)
                .Verifiable();

            var controller = new InstrumentTypesController(mockedInstrumentTypesRepository.Object);
            var result = controller.Delete(id);

            if (id > 0)
            {
                Assert.IsType<ViewResult>(result as ViewResult);
                Assert.IsType<InstrumentTypesDeleteModel>((result as ViewResult).Model);
                var receivedModel = (result as ViewResult).Model as InstrumentTypesDeleteModel;

                Assert.Equal(modelTemplate.InstrumentType.Id, receivedModel.InstrumentType.Id);
                Assert.Equal(modelTemplate.InstrumentType.Name, receivedModel.InstrumentType.Name);

                mockedInstrumentTypesRepository.VerifyAll();
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        public void DeleteConfirm(int id)
        {
            var mockedInstrumentTypesRepository = new Mock<IInstrumentTypesRepository>();
            var instrumentTypeTemplate = new InstrumentType();
            var modelTemplate = new InstrumentTypesDeleteModel()
            { InstrumentType = instrumentTypeTemplate };

            mockedInstrumentTypesRepository.Setup(k => k.Get(id))
                .Returns(instrumentTypeTemplate)
                .Verifiable();

            var controller = new InstrumentTypesController(mockedInstrumentTypesRepository.Object);
            var result = controller.Delete(id, modelTemplate);

            if (id > 0)
            {
                Assert.IsType<RedirectToRouteResult>(result as RedirectToRouteResult);
            }
            else
            {
                Assert.Null((result as ViewResult).Model);
                Assert.Equal("Error", (result as ViewResult).ViewName);    
            }
        }
    }
}
