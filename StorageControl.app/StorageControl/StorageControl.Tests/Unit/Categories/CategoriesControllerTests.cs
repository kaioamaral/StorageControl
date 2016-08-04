using Moq;
using System.Linq;
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
        [InlineData(0, null)]
        [InlineData(1, null)]
        [InlineData(0, "")]
        [InlineData(1, "")]
        [InlineData(0, "Whatever")]
        [InlineData(1, "Whatever")]
        public void CreateConfirm(int rowsAffected, string name)
        {
            var mockedCategoriesRepository = new Mock<ICategoriesRepository>();
            var categoryTemplate = new Category() { Id = 0, Name = name };
            var modelTemplate = new CategoriesCreateModel() { Category = categoryTemplate };

            mockedCategoriesRepository.Setup(k => k.Create(categoryTemplate))
                .Returns(rowsAffected)
                .Verifiable();

            var controller = new CategoriesController(mockedCategoriesRepository.Object);
            var result = controller.Create(modelTemplate);
            Assert.IsType<RedirectToRouteResult>(result);

            if (name == null || name == string.Empty)
            {
                Assert.Equal("Create",
                    (result as RedirectToRouteResult).RouteValues.Values.First());
            }
            else
            {
                if (rowsAffected > 0)
                {
                    Assert.Equal("Index",
                    (result as RedirectToRouteResult).RouteValues.Values.First());

                }
                else
                {
                    Assert.Equal("Create", (result as RedirectToRouteResult).RouteValues.Values.First());
                }

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
        [InlineData(0, 0, "")]
        [InlineData(0, 0, "Whatever")]
        [InlineData(1, 1, "Whatever")]
        [InlineData(0, 1, "")]
        public void EditConfirm(int rowsAffected, int id, string name)
        {
            var mockedCategoriesRepository = new Mock<ICategoriesRepository>();
            var categoryTemplate = new Category() { Id = id, Name = name };
            var modelTemplate = new CategoriesEditModel() { Category = categoryTemplate };

            mockedCategoriesRepository.Setup(k => k.Update(new Category()))
                .Returns(It.IsAny<int>())
                .Verifiable();
            
            var controller = new CategoriesController(mockedCategoriesRepository.Object);
            var result = controller.Edit(id, modelTemplate);

            if (id < 0 || name == string.Empty || name == null)
            {
                if (rowsAffected > 0)
                {
                    Assert.IsType<RedirectToRouteResult>(result);
                }
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
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        public void DeleteConfirm(int rowsAffeted, int id)
        {
            var mockedCategoriesRepository = new Mock<ICategoriesRepository>();
            var categoryTemplate = new Category() { Id = id, Name = It.IsAny<string>()};
            var modelTemplate = new CategoriesDeleteModel() { Category = categoryTemplate };

            mockedCategoriesRepository.Setup(k => k.Delete(id))
                .Returns(rowsAffeted)
                .Verifiable();

            var controller = new CategoriesController(mockedCategoriesRepository.Object);
            var result = controller.Delete(id, modelTemplate);
            
            if (id <= 0)
            {
                Assert.IsType<ViewResult>(result);
                Assert.Equal("Error", (result as ViewResult).ViewName);
            }
            else
            {
                Assert.IsType<RedirectToRouteResult>(result);
                
                if (rowsAffeted > 0)
                {
                    Assert.Equal("Index",
                        (result as RedirectToRouteResult).RouteValues.Values.First());
                }
                else
                {
                    Assert.Equal(1, (result as RedirectToRouteResult).RouteValues.Values.First());
                }               
            }
        }
    }
}
