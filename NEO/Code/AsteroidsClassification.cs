using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using NEO.Code.DataAccess;
using NEO.Code.Enum;
using NEO.Code.Model;
using NEO.Code.SVM;

namespace NEO.Code
{
    public class AsteroidsClassification
    {
        private LinearSVM _algo;
        private string[] _features;
        private readonly AsteroidDataAccess _ada = new AsteroidDataAccess();
        private readonly string _featurePath;

        public AsteroidsClassification()
        {
            _featurePath = Path.Combine("C:\\temp\\nasa", "machine.svm.features");
        }

        public void LoadMachine(bool reload)
        {
            try
            {
                if (!reload)
                {
                    if (!File.Exists(_featurePath))
                        throw new Exception();

                    var fi = new FileInfo(_featurePath);
                    if (fi.LastWriteTime < DateTime.Now.AddDays(-15))
                        throw new Exception();

                    // reload machine
                    _algo = new LinearSVM(null, null, null, null);
                    _algo.LoadMachine(false);

                    return;
                }
            }
            catch (Exception ex)
            {
                // no machine reloaded!
            }

            var ts = _ada.LoadTrainingData(5000);
            var data = ConvertTrainingDataToFeatures(ts);

            var cutoff = data.Count*80/100;
            var trainingData = TakeTrainingData(data, cutoff);
            var testData = TakeTestData(data, trainingData);

            var trainingMulti = LoadTrainingDataMultiple(trainingData);
            var testMulti = LoadTrainingDataMultiple(testData);

            _algo = new LinearSVM(trainingMulti.Inputs, trainingMulti.Outputs, testMulti.Inputs, testMulti.Outputs);
            _algo.LoadMachine(true);

            File.WriteAllText(_featurePath, DateTime.Now.Millisecond.ToString());
        }

        private Dictionary<double[], int> TakeTrainingData(Dictionary<double[], int> data, int cutoff)
        {
            var list = new Dictionary<double[], int>();

            for (int i = 0; i <= 10; i++)
            {
                var opt = data.Select(x => x).Where(x => x.Value == i).Take(cutoff / 10).ToList();
                foreach (var item in opt)
                {
                    list.Add(item.Key, i);
                }
            }

            return list;
        }

        private Dictionary<double[], int> TakeTestData(Dictionary<double[], int> data, Dictionary<double[], int> trainingData)
        {
            foreach (var i in trainingData)
            {
                data.Remove(i.Key);
            }

            return data;
        }

        private HashSet<string> ExtractFeatures(List<AsteroidModel> ts)
        {
            var featues = new HashSet<string>();

            foreach (var asteroid in ts)
            {
               
            }

            return featues;
        }

        private Dictionary<double[], int> ConvertTrainingDataToFeatures(List<AsteroidModel> ts)
        {
            var trainingData = new Dictionary<double[], int>();

            foreach (var asteroid in ts)
            {
                var converted = ConvertToFeatures(asteroid);

                if (!trainingData.ContainsKey(converted))
                    trainingData.Add(converted, EnumToIndex(asteroid.Classification));
            }

            return trainingData;
        }

        private SvmModel LoadTrainingDataMultiple(Dictionary<double[], int> data)
        {
            var model = new SvmModel();
            model.Inputs = new double[data.Count][];
            model.Outputs = new int[data.Count];

            var i = 0;
            foreach (var item in data)
            {
                model.Inputs[i] = item.Key;
                model.Outputs[i] = item.Value;
                i++;
            }

            return model;
        }

        private double[] ConvertToFeatures(AsteroidModel asteroid)
        {
            var doubles = new double[] { asteroid.G, asteroid.H, asteroid.M, asteroid.Perts, asteroid.Incl, asteroid.Node, asteroid.Obs, asteroid.Opp, asteroid.a, asteroid.n, asteroid.e };
            return doubles;
        }

        public EnumAsteroidType Predict(AsteroidModel asteroidModel)
        {
            var converted = ConvertToFeatures(asteroidModel);
            var multi = IndexToEnum(_algo.Predict(converted));
            return multi;
        }

        public EnumAsteroidType IndexToEnum(int index)
        {
            switch (index)
            {
                case 1:
                    return EnumAsteroidType.Atira;
                case 2:
                    return EnumAsteroidType.Aten;
                case 3:
                    return EnumAsteroidType.Apollo;
                case 4:
                    return EnumAsteroidType.Amor;
                case 5:
                    return EnumAsteroidType.Q1665;
                case 6:
                    return EnumAsteroidType.Hungaria;
                case 7:
                    return EnumAsteroidType.Phocaea;
                case 8:
                    return EnumAsteroidType.Hilda;
                case 9:
                    return EnumAsteroidType.JupterTrojan;
                case 10:
                    return EnumAsteroidType.DistantObject;
                default:
                    return EnumAsteroidType.Unclassified;
            }
        }

        public int EnumToIndex(string item)
        {
            if (item == "0001") return 1;

            if (item == "0002") return 2;

            if (item == "0003") return 3;

            if (item == "0004") return 4;

            if (item == "0005") return 5;

            if (item == "0006") return 6;

            if (item == "0007") return 7;

            if (item == "0008") return 8;

            if (item == "0009") return 9;

            if (item == "0010") return 10;

            return 0;
        }
    }
}