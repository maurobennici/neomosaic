using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEO.Code.Model
{
    public class AsteroidModel
    {
        public virtual int Id { get; set; }

        public virtual string Des { get; set; }

        public virtual double H { get; set; }

        public virtual double G { get; set; }

        public virtual string Epoch { get; set; }

        public virtual double M { get; set; }

        public virtual double Peri { get; set; }

        public virtual double Node { get; set; }

        public virtual double Incl { get; set; }

        public virtual double e { get; set; }

        public virtual double n { get; set; }

        public virtual double a { get; set; }

        public virtual string Reference { get; set; }

        public virtual double Obs { get; set; }

        public virtual double Opp { get; set; }

        public virtual double Arc { get; set; }

        public virtual double rms { get; set; }

        public virtual double Perts { get; set; }

        public virtual string Computer { get; set; }

        public virtual string Classification { get; set; }

        public virtual string Name { get; set; }
    }
}