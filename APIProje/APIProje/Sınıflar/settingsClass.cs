using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISocialList;
namespace APIProje
{
    class settingsClass
    {
        private CompositionContainer _container;

        [ImportMany(typeof(ListAPI))]
        public IEnumerable<Lazy<ListAPI>> dllFiles;
        string hata = "";
        public settingsClass()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog("C:\\Users\\ataberk.dagdelen\\source\\repos\\APIProje\\APIProje\\Eklentiler"));
            _container = new CompositionContainer(catalog);

            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compExcep) { hata = "DLL Hatası!"; }
        }

        public double countDLL()
        {
            settingsClass mySettings = new settingsClass();
            int count = 0;
            foreach (var dll in mySettings.dllFiles) count++;

            return count;
        }
    }
}
