using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using StorageControl.DataAccess.Repositories;
using StorageControl.Domain.Contracts.Interfaces;

namespace StorageControl.Web.App_Start
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion
        
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IInstrumentsRepository, InstrumentsRepository>();
            container.RegisterType<ICategoriesRepository, CategoriesRepository>();
            container.RegisterType<IInstrumentTypesRepository, InstrumentTypesRepository>();
        }
    }
}
