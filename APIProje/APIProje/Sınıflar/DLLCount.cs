using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using ISocialList;
namespace APIProje.Sınıflar
{
    class DLLCount
    {
        private CompositionContainer _container;

        [ImportMany(typeof(ListAPI))]
        public IEnumerable<Lazy<ListAPI>> dllFiles;

        public ListAPI Social { get; set; }

        public DLLCount()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog("C:\\Users\\ataberk.dagdelen\\Documents\\StajProje\\APIProje\\APIProje\\Eklentiler"));
            _container = new CompositionContainer(catalog);

            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compExcep) { }
        }
    }
}
