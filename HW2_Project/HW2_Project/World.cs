using System;

namespace HW2_Project
{
    class World
    {
        private int m_populationSize;
        public int PopulationSize { get { return m_populationSize; } }

        private int m_currentGeneration;
        public int CurrentGeneration { get { return m_currentGeneration; } }

        private Func<World, bool> m_terminateCondition;

        private Chromesome[] m_population;

        private Chromesome m_bestIndividual;
        public Chromesome BestIndividual { get { return m_bestIndividual; } }

        private int m_successfulMutationCount = 0;
        public int SuccessfulMutationCount { get { return m_successfulMutationCount; } }

        public World(int _populationSize, Func<World, bool> _terminateCondition)
        {
            // fixed initialization
            m_currentGeneration = 0;
            m_successfulMutationCount = 0;
            // customed initialization
            m_populationSize = _populationSize;
            m_terminateCondition = _terminateCondition;

            // initialization base on customed variables
            m_population = new Chromesome[m_populationSize];
            for (int index = 0; index < m_populationSize; index++)
            {
                m_population[index] = new Chromesome(this);
            }

            UpdateBestIndividual();
        }

        public bool IsTerminateConditionTrue()
        {
            return m_terminateCondition.Invoke(this);
        }

        public void UpdateBestIndividual()
        {
            // in ES, the best individual is the one with minimal fitness
            double minFitness = double.PositiveInfinity;
            int bestIndividualIndex = -1;
            // iterate through all individuals
            for (int index = 0; index < m_populationSize; index++)
            {
                double fitness = m_population[index].GetFitness();
                if (fitness <= minFitness)
                {
                    bestIndividualIndex = index;
                    minFitness = fitness;
                }
            }

            // set the best individual base on bestIndividualIndex
            if (bestIndividualIndex < 0)
                throw new Exception("bestIndividualIndex is negative!");
            else
                m_bestIndividual = m_population[bestIndividualIndex];
        }

        public void ToNextGeneration()
        {
            m_currentGeneration++;
            // (1,1) , always use children
            if (!Configuration.s_isParentCandidate)
            {
                for (int index = 0; index < m_populationSize; index++)
                {
                    double originalFitness = m_population[index].GetFitness();

                    m_population[index].Mutate();

                    double mutatedFitness = m_population[index].GetFitness();
                }
            }
            else // (1+1)
            {
                for (int index = 0; index < m_populationSize; index++)
                {
                    Chromesome parent = m_population[index];
                    Chromesome child = new Chromesome(parent);
                    // mutate the child
                    child.Mutate();
                    // in this case, parent wins
                    if (parent.GetFitness() < child.GetFitness())
                    {
                        m_population[index] = parent;
                    }
                    else // in this case, child (after mutation) wins
                    {
                        m_population[index] = child;
                    }
                }
            }

            // set up for successful mutation
            double originalBestFitness = BestIndividual.GetFitness();
            // Update world status
            UpdateBestIndividual();

            double mutatedBestFitness = BestIndividual.GetFitness();

            if(mutatedBestFitness < originalBestFitness)
            {
                m_successfulMutationCount++;
            }
        }
    }
}