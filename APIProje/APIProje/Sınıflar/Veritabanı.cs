using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Driver;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Dialect;

namespace APIProje.Sınıflar
{
    class Veritabanı
    {
        private void AddToDatabase(string myKeyword, string myType)
        { // Duruma göre TextBox değişkeni ve tip alıyor.
            var sefact = NHibernateSettings(); //Config dosyası fonksiyondan alınıyor.
            //NHibernate başlangıç
            using (var session = sefact.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var verim = new Database
                    {
                        Keyword = myKeyword,
                        Type = myType
                    };
                    session.Save(verim); // Veritabanına kaydediliyor.
                    tx.Commit(); // İşlemler gerçekleştiriliyor.
                }
            }
            //NHibernate bitiş
        }

        private NHibernate.ISessionFactory NHibernateSettings()
        { // NHibernate bağlantısı için gerekli olan dosyayı return ediyor.
            //NHibernate için cfg tanımlandı.
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=127.0.0.1;Port=5432;Database=Calisma;User Id=postgres;Password=1";
                x.Driver<NpgsqlDriver>();
                x.Dialect<PostgreSQLDialect>();
            });

            cfg.AddAssembly(Assembly.GetExecutingAssembly());
            var sefact = cfg.BuildSessionFactory();
            return sefact;
            //NHibernate ayarları sonu
        }
    }
}
