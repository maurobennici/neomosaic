using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NEO.Code.Model;
using NEO.Code.Utils;
using NHibernate.Linq;

namespace NEO.Code.DataAccess
{
    public class AsteroidDataAccess
    {
        internal void Save(Code.Model.AsteroidModel asteroid)
        {
            var nh = NHibernateHelper.GetCurrentSession();
            nh.Save(asteroid);
            nh.Flush();
        }

        internal List<AsteroidModel> LoadTrainingData(int numbers)
        {
            var sa = new string[] { "0000", "0001", "0002", "0003", "0004", "0005", "0006", "0007", "0008", "0009", "0010" };

            var list = new List<AsteroidModel>();
            foreach (var s in sa)
            {
                list.AddRange(LoadClass(s, numbers));
            }

            return list;
        }

        private List<AsteroidModel> LoadClass(string clx, int numbers)
        {
            var nh = NHibernateHelper.GetCurrentSession();
            var list = nh.Query<AsteroidModel>().Where(x => x.Classification == clx).Take(numbers).ToList().OrderBy(x => Guid.NewGuid()).Take(numbers / 10).ToList();

            for (int i = list.Count; i < numbers / 10; i++)
            {
                list.Add(new AsteroidModel() { Classification = clx });
            }

            return list;
        }
    }
}