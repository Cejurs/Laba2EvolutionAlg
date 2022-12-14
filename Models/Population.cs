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
                var random = new Random();
                for (int i = 0; i < populationSize; i++)
                {
                    var randomInt = random.Next(0, Function.Right + Function.Left);
                    individuals[i] = new Individual(randomInt);
                }
            }
        }
        
        public bool Contains(int x)
        {
            return individuals.Select(ind => ind.GetX()).Contains(x);
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
