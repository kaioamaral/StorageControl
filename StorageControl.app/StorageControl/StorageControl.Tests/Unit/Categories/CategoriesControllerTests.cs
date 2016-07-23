using Moq;
using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Domain.Model.Entities;
using StorageControl.Web.Controllers;
using StorageControl.Web.Models.Categories;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace StorageControl.Tests.Unit.Categories
{
    public class CategoriesControllerTests
    {
        [Fact]
        public void Index()
        {
            var mockedCategoriesRepository = new Mock<ICategoriesRepository>();
            var templateCategoriesList = new List<Category>();

            mockedCategoriesRepository.Setup(k => k.List())
                .Returns(templateCategoriesList)
                .Verifiable();

            var controller = new CategoriesController(mockedCategoriesRepository.Object);
            var response = controller.Index();
            Assert.IsType<ViewResult>(response);
            Assert.IsType<CategoriesListModel>(response.Model);

            var receivedModel = response.Model as CategoriesListModel;
            Assert.Equal(templateCategoriesList.Count, receivedModel.Categories.Count);

            mockedCategoriesRepository.VerifyAll();
        }

        [Fact]
        public void Create()
        {
            var mockedCategoriesRepository = new Mock<ICategoriesRepository>();

            var controller = new CategoriesController(mockedCategoriesRepository.Object);
            var result = controller.Create();
            Assert.IsType<CategoriesCreateModel>(result.Model);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Whatever")]
        public void CreateConfirm(string name)
        {
            var mockedCategoriesRepository = new Mock<ICategoriesRepository>();
            var categoryTemplate = new Category() { Id = 0, Name = name };
            var modelTemplate = new CategoriesCreateModel() { Category = categoryTemplate };

            mockedCategoriesRepository.Setup(k => k.Create(categoryTemplate))
                .Returns(It.IsAny<int>())
                .Verifiable();

            var controller = new CategoriesController(mockedCategoriesRepository.Object);
            var result = controller.Create(modelTemplate);
            Assert.IsType<ViewResult>(result);

            if (name == null || name == string.Empty || name == "")
            {
                Assert.Equal("Error", (result as ViewResult).ViewName);
            }
            else
            {
                mockedCategoriesRepository.VerifyAll();
            }
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(0, "Whatever")]
        [InlineData(1, "")]
        [InlineData(1, "Whatever")]
        public void Edit(int id, string name)
        {
            var mockedCategoriesRepository = new Mock<ICategoriesRepository>();
            var categoryTemplate = new Category() { Id = id, Name = name };

            mockedCategoriesRepository.Setup(k => k.Get(It.IsAny<int>()))
                .Returns(categoryTemplate)
                .Verifiable();

            var controller = new CategoriesController(mockedCategoriesRepository.Object);
            var result = controller.Edit(id);

            if (id <= 0)
            {
                Assert.Equal("Error", result.ViewName);
                Assert.Null(result.Model);
            }
            else
            {
                var receivedModel = (result.Model) as CategoriesEditModel;

                Assert.IsType<CategoriesEditModel>(result.Model);
                Assert.Equal(categoryTemplate.Id, receivedModel.Category.Id);
                Assert.Equal(categoryTemplate.Name, receivedModel.Category.Name);

                mockedCategoriesRepository.Verify(k => k.Get(It.IsAny<int>()));
            }
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(0, "Whatever")]
        [InlineData(1, "Whatever")]
        [InlineData(1, "")]
        public void EditConfirm(int id, string name)
        {
            var mockedCategoriesRepository = new Mock<ICategoriesRepository>();
            var categoryTemplate = new Category() { Id = id, Name = name };
            var modelTemplate = new CategoriesEditModel() { Category = categoryTemplate };

            mockedCategoriesRepository.Setup(k => k.Update(new Category()))
                .Returns(It.IsAny<int>())
                .Verifiable();
            
            var controller = new CategoriesController(mockedCategoriesRepository.Object);
            var result = controller.Edit(id, modelTemplate);
            Assert.IsType<ViewResult>(result);

            if (id < 0 || name == "" || name == string.Empty || name == null)
            {
                Assert.IsType<CategoriesEditModel>((result as ViewResult).Model);
                var receivedModel = (result as ViewResult).Model as CategoriesEditModel;
                Assert.Equal(categoryTemplate.Id, receivedModel.Category.Id);
                Assert.Equal(categoryTemplate.Name, receivedModel.Category.Name);
            }
            else
            {
                Assert.IsType<ViewResult>(result);
            }
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(0, "Whatever")]
        [InlineData(1, "")]
        [InlineData(1, "Whatever")]
        public void Delete(int id, string name)
        {
            var mockedCategoriesRepository = new Mock<ICategoriesRepository>();
            var categoryTemplate = new Category() { Id = id, Name = name };

            mockedCategoriesRepository.Setup(k => k.Get(id))
                .Returns(categoryTemplate)
                .Verifiable();

            var controller = new CategoriesController(mockedCategoriesRepository.Object);
            var result = controller.Delete(id);

            if (id <= 0)
            {
                Assert.Equal("Error", result.ViewName);
                Assert.Null(result.Model);
            }
            else
            {
                var receivedModel = (result.Model) as CategoriesDeleteModel;

                Assert.IsType<CategoriesDeleteModel>(result.Model);
                Assert.Equal(categoryTemplate.Id, receivedModel.Category.Id);
                Assert.Equal(categoryTemplate.Name, receivedModel.Category.Name);

                mockedCategoriesRepository.Verify(k => k.Get(It.IsAny<int>()));
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteConfirm(int id)
        {
            var mockedCategoriesRepository = new Mock<ICategoriesRepository>();
            var categoryTemplate = new Category() { Id = id, Name = It.IsAny<string>()};
            var modelTemplate = new CategoriesDeleteModel() { Category = categoryTemplate };

            mockedCategoriesRepository.Setup(k => k.Update(new Category()))
                .Returns(It.IsAny<int>())
                .Verifiable();

            var controller = new CategoriesController(mockedCategoriesRepository.Object);
            var result = controller.Delete(id, modelTemplate);
            Assert.IsType<ViewResult>(result);

            if (id <= 0)
            {
                Assert.IsType<CategoriesDeleteModel>((result as ViewResult).Model);
                var receivedModel = (result as ViewResult).Model as CategoriesDeleteModel;
                Assert.Equal(categoryTemplate.Id, receivedModel.Category.Id);
                Assert.Equal(categoryTemplate.Name, receivedModel.Category.Name);
            }
            else
            {
                Assert.Equal("Index", (result as ViewResult).ViewName);
            }
        }

    }
}
