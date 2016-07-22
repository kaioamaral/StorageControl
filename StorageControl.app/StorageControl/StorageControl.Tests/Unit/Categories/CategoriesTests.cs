using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Domain.Model.Entities;
using StorageControl.Web.Controllers;
using System.Web.Mvc;
using StorageControl.Web.Models.Categories;

namespace StorageControl.Tests.Unit.Categories
{
    public class CategoriesTests
    {
        [Fact]
        public void List()
        {
            var mockedCategoriesRepository = new Mock<ICategoriesRepository>();

            mockedCategoriesRepository.Setup(k => k.List())
                .Returns(new List<Category>());

            var controller = new CategoriesController(mockedCategoriesRepository.Object);
            var response = controller.Index();
            var result = response.Model as CategoriesListModel;
            Assert.IsType<ActionResult>(response);
        }
    }
}
