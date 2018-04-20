using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Project
{
    class Chromesome
    {
        private double[] data;

        // if using one step-size
        private double singleStepSize;
        // if using n step-sizes
        private double[] stepSizes;

        private World world;

        public Chromesome(World _world)
        {
            world = _world;

            data = new double[10];
            stepSizes = new double[10];

            for (int index = 0; index < 10; index++)
            {
                data[index] = 1;
                stepSizes[index] = Configuration.s_initialStepSize;
            }
            singleStepSize = Configuration.s_initialStepSize;
        }

        public Chromesome(Chromesome parent)
        {
            world = parent.world;

            data = new double[10];
            stepSizes = new double[10];

            parent.data.CopyTo(data, 0);
            parent.stepSizes.CopyTo(stepSizes, 0);
            singleStepSize = parent.singleStepSize;
        }

        public double GetFitness()
        {
            double fitness = 0;
            // calculate fitness
            for (int index = 0; index < data.Length; index++)
            {
                double value = data[index];
                fitness += value * value;
            }

            return fitness;
        }

        public void Mutate()
        {
            double[] normalI_01 = new double[10];
            for (int index = 0; index < 10; index++)
                normalI_01[index] = RandomUtility.SampleGaussian01();

            // Do mutation on step size(s)
            if (Configuration.s_stepSizeMutationType == Configuration.StepSizeMutationType.UnCorrelated)
            {
                if (Configuration.s_isUsingSingleStepSize)
                {
                    // single step size, uncorrelated mutation
                    double tMultiplyNormal01 = Configuration.s_constantT * RandomUtility.SampleGaussian01();

                    singleStepSize *= Math.Pow(Math.E, tMultiplyNormal01);

                    singleStepSize = Math.Max(singleStepSize, Configuration.s_stepSizeLowerBound);
                }
                else
                {
                    // n step sizes, uncorrelated mutation
                    for (int index = 0; index < 10; index++)
                    {
                        double tMultiplyNormal01 = Configuration.s_constantT * normalI_01[index];
                        double tPrimeMultiplyNormal01 =
                            Configuration.s_constantTprime * RandomUtility.SampleGaussian01();

                        stepSizes[index] *= Math.Pow(Math.E, tMultiplyNormal01 + tPrimeMultiplyNormal01);

                        //if (Double.IsInfinity(stepSizes[index]))
                        //    Console.WriteLine("infinity after mul with" + Math.Pow(Math.E, tMultiplyNormal01 + tPrimeMultiplyNormal01));

                        stepSizes[index] = Math.Max(stepSizes[index], Configuration.s_stepSizeLowerBound);
                    }
                }
            }
            else if (Configuration.s_stepSizeMutationType == Configuration.StepSizeMutationType.OneFifthRule)
            {
                if (Configuration.s_isUsingSingleStepSize)
                {
                    // single step size, 1/5 rule
                    double Ps = world.SuccessfulMutationCount / (world.CurrentGeneration + 1.0);
                    if (Ps > 0.2)
                    {
                        singleStepSize /= Configuration.s_constantA;
                    }
                    else if (Ps < 0.2)
                    {
                        singleStepSize *= Configuration.s_constantA;
                    }
                }
                else
                {
                    // n step sizes, 1/5 rule
                    double Ps = world.SuccessfulMutationCount / (world.CurrentGeneration + 1.0);

                    for (int index = 0; index < 10; index++)
                    {
                        if (Ps > 0.2)
                        {
                            stepSizes[index] /= Configuration.s_constantA;
                        }
                        else if (Ps < 0.2)
                        {
                            stepSizes[index] *= Configuration.s_constantA;
                        }
                    }
                }
            }
            else // fixed step size
            {
                // do nothing
            }

            // Do mutation on data (decision variables)
            if (Configuration.s_isUsingSingleStepSize)
            {
                // single step size, data mutation
                for (int index = 0; index < 10; index++)
                {
                    double delta = singleStepSize * RandomUtility.SampleGaussian01();
                    data[index] += delta;
                }
            }
            else
            {
                // n step sizes, data mutation
                for (int index = 0; index < 10; index++)
                {
                    double delta = stepSizes[index] * normalI_01[index];
                    data[index] += delta;
                }
            }
        }
    }
}