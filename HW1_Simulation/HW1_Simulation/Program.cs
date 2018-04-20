using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(string.Format("Generation {0} has best fitness : {1}", world.currentGeneration, world.BestFitness()));

            int[,] bestFitnessOfEachRunGen = new int[10, 101];

            for (int run = 0; run < 10; run++)
            {
                World world = new World(); // currently generation 0
                bestFitnessOfEachRunGen[run, 0] = world.BestFitness();

                for (int generation = 1; generation <= 100; generation++)
                {
                    world.ToNextGeneration(World.SelectionType.Tournament);

                    Console.WriteLine(string.Format("Generation {0} has best fitness : {1}", world.currentGeneration, world.BestFitness()));

                    bestFitnessOfEachRunGen[run, generation] = world.BestFitness();
                }
            }

            StreamWriter streamWriter = new StreamWriter("output6.txt");

            for (int generation = 0; generation <= 100; generation++)
            {
                int totalFitness = 0;
                for (int run = 0; run < 10; run++)
                    totalFitness += bestFitnessOfEachRunGen[run, generation];

                streamWriter.WriteLine(string.Format("{0}\t{1}", generation, totalFitness / 10f));
                //Console.WriteLine(string.Format("Generation {0} has average best fitness : {1}", generation, totalFitness / 10));
            }
            streamWriter.Close();
            Console.WriteLine("DONE!");
            Console.ReadKey();
        }
    }
}