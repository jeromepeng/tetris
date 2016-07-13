using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace SIFTTest.Util
{
    public interface IServiceLocator
    {
        T GetInstance<T>();
    }

    public class ServiceLocator : IServiceLocator
    {
        private CompositionContainer container;
        public static readonly ServiceLocator Instance = new ServiceLocator();

        private ServiceLocator()
        {
            InitializeContainer();
        }

        public T GetInstance<T>()
        {
            return container.GetExportedValue<T>();
        }

        private void InitializeContainer()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}