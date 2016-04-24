using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;

namespace NEO.Code.SVM
{
    public class LinearSVM
    {
        private readonly double[][] _learnInputs;
        private readonly int[] _learnOutputs;

        private readonly double[][] _testInputs;
        private readonly int[] _testOutputs;

        private readonly int _classes;

        private MulticlassSupportVectorMachine _machine;
        private string MachinePath = Path.Combine(@"C:\\temp\\nasa", "machine.svm.features");

        public LinearSVM(double[][] learnInputs, int[] learnOutputs, double[][] testInputs, int[] testOutputs)
        {
            this._learnInputs = learnInputs;
            this._learnOutputs = learnOutputs;

            this._testInputs = testInputs;
            this._testOutputs = testOutputs;
        }

        /// <summary>
        /// Prepare the prediction machine
        /// </summary>
        /// <param name="reload">if true restart the machine from scratch</param>
        public void LoadMachine(bool reload)
        {
            if (!reload && File.Exists(MachinePath))
            {
                try
                {
                    _machine = MulticlassSupportVectorMachine.Load(MachinePath);
                    return;
                }
                catch (Exception ex)
                {
                    // log
                    throw ex;
                }
            }

            // Create a new kernel
            IKernel kernel = new Gaussian();

            // Create a new Multi-class Support Vector Machine with one input, using the linear kernel and for four disjoint classes.
            _machine = new MulticlassSupportVectorMachine(_learnInputs.First().Length, kernel, 11);

            // Create the Multi-class learning algorithm for the machine
            var teacher = new MulticlassSupportVectorLearning(_machine, _learnInputs, _learnOutputs);

            // Configure the learning algorithm to use SMO to train the underlying SVMs in each of the binary class subproblems.
            teacher.Algorithm = (svm, classInputs, classOutputs, i, j) => new SequentialMinimalOptimization(svm, classInputs, classOutputs);

            // Run the learning algorithm
            double error = teacher.Run(true);

            // test
            var ok = 0D;
            for (int i = 0; i < _testInputs.Count(); i++)
            {
                var result = _machine.Compute(_testInputs[i]);
                var expected = _testOutputs[i];

                if (result == expected)
                    ok++;
            }
            var ends = (ok / _testInputs.Count()) * 100;

            Save();
        }

        public int Predict(double[] guineaPig)
        {
            // predict
            int result = _machine.Compute(guineaPig);
            return result;
        }

        public void Save()
        {
            _machine.Save(MachinePath);
        }
    }
}