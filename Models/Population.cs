namespace Evolution.Models
{
    public class Population
    {
        private Individual[] individuals;
        public Individual[] Individuals { get => individuals; }

        public Population(int populationSize, bool isInitial=false)
        {
            individuals = new Individual[populationSize];
            if (isInitial)
            {
                for (int i = 0; i < populationSize; i++)
                {
                    individuals[i] = new Individual();
                }
            }
        }
        
        public bool Contains(int x)
        {
            return individuals.Select(ind => ind.Gene).Contains((sbyte)x);
        }
        public Individual GetFittest()
        {
            var fittest = individuals[0];
            for (int i = 0; i < individuals.Length; i++)
            {
                if (fittest.GetFittness() >= individuals[i].GetFittness())
                {
                    fittest = individuals[i];
                }
            }
            return fittest;
        }

        public void SaveIndividual(int index,Individual individual)
        {
            individuals[index]=individual;
        }

        public Individual GerRandomParent()
        {
            var rnd=new Random();
            return individuals[rnd.Next(0,individuals.Length-1)];
        }
    }
}
