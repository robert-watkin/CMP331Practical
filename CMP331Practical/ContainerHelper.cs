using CMP331Practical.Contracts;
using CMP331Practical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;


namespace CMP331Practical
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;
        static ContainerHelper()
        {
            _container = new UnityContainer();
            _container.RegisterType<IRepository<Invoice>, SQLRepository<Invoice>>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRepository<Property>, SQLRepository<Property>>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRepository<Role>, SQLRepository<Role>>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRepository<User>, SQLRepository<User>>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}
