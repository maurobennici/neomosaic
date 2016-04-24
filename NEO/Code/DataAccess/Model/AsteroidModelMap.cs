using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using NEO.Code.Model;

namespace NEO.Code.DataAccess.Model
{
    public class AsteroidModelMap : ClassMap<AsteroidModel>
    {
        public AsteroidModelMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Classification);

            Map(x => x.Computer);
            Map(x => x.Epoch);

            Map(x => x.Des);
            Map(x => x.G);
            Map(x => x.H);

            Map(x => x.Incl);
            Map(x => x.M);

            Map(x => x.Peri);
            Map(x => x.Node);

            Map(x => x.e);
            Map(x => x.n);

            Map(x => x.a);
            Map(x => x.Reference);
            Map(x => x.Obs);

            Map(x => x.Opp);
            Map(x => x.Arc);

            Map(x => x.Perts);
            Map(x => x.rms);

            Map(x => x.Name);

            Table("Nea");
        }
    }
}