using StorageControl.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorageControl.Web.Models.Categories
{
    public class CategoriesListModel
    {
        public CategoriesListModel()
        {
            this.Categories = new List<Category>();
        }

        public List<Category> Categories { get; set; }
    }
}