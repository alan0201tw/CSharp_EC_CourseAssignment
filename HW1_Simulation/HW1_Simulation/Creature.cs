using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_Simulation
{
    class Creature : IComparable<Creature>
    {
        static Random random = new Random();

        public int generation;

        private BitArray data;
        public BitArray Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
                
                int f = 0;
                for (int i = 0; i < data.Count; i++)
                {
                    f += (data.Get(i) == true ? 1 : 0);
                }

                fitness = f;
            }
        }

        private int fitness = 0;
        public int Fitness()
        {
            return fitness;
        }

        public override string ToString()
        {
            string tmp = string.Empty;
            for (int i = 0; i < Data.Count; i++)
            {
                tmp += Data.Get(i) == true ? "1" : "0";
            }
            return tmp;
        }

        public int CompareTo(Creature other)
        {
            int myFitness = this.Fitness();
            int otherFitness = other.Fitness();

            if (myFitness == otherFitness)
                return 0;
            else if (myFitness < otherFitness)
                return 1;
            else
                return -1;
        }

        // no parameter : 0 generation and random data
        public Creature()
        {
            generation = 0;
            Data = new BitArray(50);
            for (int i = 0; i < 50; i++)
            {
                Data.Set(i, random.NextDouble() >= 0.5 ? true : false);
            }

            int f = 0;
            for (int i = 0; i < data.Count; i++)
            {
                f += (data.Get(i) == true ? 1 : 0);
            }

            fitness = f;
        }

        // parameters as parents : 1 - point crossover
        public Creature(Creature parentA, Creature parentB)
        {
            // for length n of bits, there ara n-1 spaces to cut
            int cuttingPoint = random.Next(parentA.Data.Length);

            Data = new BitArray(50);

            generation = parentA.generation + 1;
            for (int i = 0; i < parentA.Data.Count; i++)
            {
                // if still before cutting point, get data from parent a
                // else , get data from parent b
                if (i < cuttingPoint)
                {
                    Data.Set(i, parentA.Data.Get(i));
                }
                else
                {
                    Data.Set(i, parentB.Data.Get(i));
                }
            }

            int f = 0;
            for (int i = 0; i < data.Count; i++)
            {
                f += (data.Get(i) == true ? 1 : 0);
            }

            fitness = f;

            //Console.WriteLine(parentA.ToString());
            //Console.WriteLine(parentB.ToString());
            //Console.WriteLine("cut at " + cuttingPoint);
            //Console.WriteLine(this.ToString());
        }
    }
}