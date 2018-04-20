using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("######################################");
            Console.WriteLine("Simulating with conditions : ");
            Console.WriteLine("s_isUsingSingleStepSize = " + Configuration.s_isUsingSingleStepSize);
            Console.WriteLine("s_stepSizeMutationType = " + Configuration.s_stepSizeMutationType);
            Console.WriteLine("s_initialStepSize = " + Configuration.s_initialStepSize);

            if (Configuration.s_stepSizeMutationType == Configuration.StepSizeMutationType.UnCorrelated)
            {
                Console.WriteLine("s_constantT = " + Configuration.s_constantT);
                Console.WriteLine("s_constantTprime = " + Configuration.s_constantTprime);
                Console.WriteLine("s_stepSizeLowerBound = " + Configuration.s_stepSizeLowerBound);
            }

            if(Configuration.s_stepSizeMutationType == Configuration.StepSizeMutationType.OneFifthRule)
            {
                Console.WriteLine("s_constantA = " + Configuration.s_constantA);
            }

            Console.WriteLine("s_isParentCandidate = " + Configuration.s_isParentCandidate);
            Console.WriteLine("######################################");
            Console.WriteLine();

            for (int run = 0; run < 10; run++)
            {
                World world = new World(1, TerminateCondition);

                while (!world.IsTerminateConditionTrue())
                {
                    //Console.WriteLine("Best Fitness of generation" + world.CurrentGeneration + " is : " + world.BestIndividual.GetFitness());

                    world.ToNextGeneration();
                }
                // output best fitness
                Console.WriteLine("Best Fitness is : " + world.BestIndividual.GetFitness());
                // output current generation
                Console.WriteLine("Termination Generation is : " + world.CurrentGeneration);
            }

            Console.WriteLine();
            Console.WriteLine("OK!");

            // pause the screen
            Console.ReadKey();
        }

        private static bool TerminateCondition(World world)
        {
            if (world.CurrentGeneration >= 10000000) return true;
            if (world.BestIndividual.GetFitness() <= 0.005) return true;

            return false;
        }
    }
}