using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_Simulation
{
    class World
    {
        public enum SelectionType
        {
            RouletteWheel, Tournament
        }

        static Random random = new Random();

        public int currentGeneration;
        public Creature[] creatures = new Creature[200];

        public World()
        {
            // initializing creatures
            currentGeneration = 0;

            for (int creatureIndex = 0; creatureIndex < creatures.Length; creatureIndex++)
            {
                creatures[creatureIndex] = new Creature();
            }

            Array.Sort(creatures);
        }

        public int BestFitness()
        {
            int maxFitness = 0;
            for (int i = 0; i < creatures.Length; i++)
            {
                if (creatures[i].Fitness() > maxFitness)
                    maxFitness = creatures[i].Fitness();
            }
            return maxFitness;
        }

        public void ToNextGeneration(SelectionType selectionType)
        {
            Creature[] nextGeneration = new Creature[creatures.Length];
            for (int i = 0; i < nextGeneration.Length; i++)
            {
                // select parents and reproduce until generate same number of original population
                ParentSelection(out int parent1, out int parent2, selectionType);

                //Console.WriteLine("parent1 = " + parent1 + " , parent2 = " + parent2);
                //Console.WriteLine("parent1 fitness = " + creatures[parent1].Fitness() + " , parent2 fitness = " + creatures[parent2].Fitness());

                nextGeneration[i] = new Creature(creatures[parent1], creatures[parent2]);
            }
            // replace old population with new creatures
            creatures = nextGeneration;
            Array.Sort(creatures);
            currentGeneration++;
        }

        public void ParentSelection(out int parentIndex1, out int parentIndex2, SelectionType selectionType)
        {
            // TODO
            parentIndex1 = -1;
            parentIndex2 = -1;

            // Roulette wheel selection with replacement
            if (selectionType == SelectionType.RouletteWheel)
            {
                int totalFitness = 0;
                for (int i = 0; i < creatures.Length; i++)
                {
                    totalFitness += creatures[i].Fitness();
                }

                int rndNumber1 = random.Next(totalFitness); // Utility.LongRandom(0, totalFitness, random);
                int rndNumber2 = random.Next(totalFitness);
                int offset = 0;

                for (int i = 0; i < creatures.Length; i++)
                {
                    offset += creatures[i].Fitness();
                    if (rndNumber1 < offset && parentIndex1 == -1)
                    {
                        parentIndex1 = i;
                    }
                    if (rndNumber2 < offset && parentIndex2 == -1)
                    {
                        parentIndex2 = i;
                    }
                    // break when both parents are found
                    if (parentIndex1 > 0 && parentIndex2 > 0)
                        break;
                }
            }
            else if (selectionType == SelectionType.Tournament)
            {
                // Tournament selection with replacement
                int candidate1 = random.Next(creatures.Length);
                int candidate2 = random.Next(creatures.Length);

                if (creatures[candidate1].Fitness() > creatures[candidate2].Fitness())
                {
                    parentIndex1 = candidate1;
                }
                else
                {
                    parentIndex1 = candidate2;
                }

                candidate1 = random.Next(creatures.Length);
                candidate2 = random.Next(creatures.Length);

                if (creatures[candidate1].Fitness() > creatures[candidate2].Fitness())
                {
                    parentIndex2 = candidate1;
                }
                else
                {
                    parentIndex2 = candidate2;
                }
            }
        }
    }
}