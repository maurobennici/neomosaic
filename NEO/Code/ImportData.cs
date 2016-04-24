using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using NEO.Code.DataAccess;
using NEO.Code.Model;

namespace NEO.Code
{
    public class ImportData
    {
        public void Start()
        {
            var fileS = "c:\\temp\\nasa\\MPCORB.DAT";

            using (var fs = new StreamReader(fileS))
            {
                var da = new AsteroidDataAccess();
                string line;

                while((line = fs.ReadLine()) != null)
                {
                    var asteroid = ConvertLineToModel(line);
                    if (asteroid != null)
                        da.Save(asteroid);
                }
            }
        }

        private CultureInfo cu = new CultureInfo("en-us");

        private AsteroidModel ConvertLineToModel(string line)
        {
            try
            {
                var model = new AsteroidModel();

                model.Des = line.Substring(0, 6).Trim();
                model.H = Double.Parse(line.Substring(8, 5).Trim(), cu);
                model.G = Double.Parse(line.Substring(14, 5).Trim(), cu);

                model.Epoch = line.Substring(20, 5);
                model.M = Double.Parse(line.Substring(26, 9).Trim(), cu);
                model.Peri = Double.Parse(line.Substring(37, 9).Trim(), cu);
                model.Node = Double.Parse(line.Substring(48, 9).Trim(), cu);

                model.Incl = Double.Parse(line.Substring(59, 9).Trim(), cu);
                model.e = Double.Parse(line.Substring(70, 9).Trim(), cu);
                model.n = Double.Parse(line.Substring(80, 11).Trim(), cu);

                model.a = Double.Parse(line.Substring(92, 11).Trim(), cu);
                model.Reference = line.Substring(107, 11);
                model.Obs = Double.Parse(line.Substring(117, 4).Trim(), cu);

                model.Opp = Double.Parse(line.Substring(123, 3).Trim(), cu);
                //model.Arc = line.Substring();
                model.rms = Double.Parse(line.Substring(137, 4).Trim(), cu);

                //model.Perts = line.Substring();
                model.Computer = line.Substring(150, 10).Trim();
                model.Classification = line.Substring(161, 4).Trim();

                model.Name = line.Substring(175, 10).Trim();

                return model;
            }
            catch (Exception)
            {
                return null;
            }
           
        }
    }
}