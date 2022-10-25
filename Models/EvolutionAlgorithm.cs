using System;

namespace Evolution.Models
{
    public class EvolutionAlgorithm
    {
        private static double uniformRate = 0.5;
        private static double mutationRate = 0.3;
        private static bool elitism = true;

        public static Population evolvePopulation(Population pop)
        {
            Population newPopulation = new Population(pop.Individuals.Length);
            // Сохранение лучшей особи
            if (elitism)
            {
                newPopulation.SaveIndividual(0, pop.GetFittest());
            }
            // Кроссовер
            int elitismOffset;
            if (elitism)
            {
                elitismOffset = 1;
            }
            else
            {
                elitismOffset = 0;
            }
            // Цикл по популяции и создание новых особей путём
            // кроссовера
            for (int i = elitismOffset; i < pop.Individuals.Length; i++)
            {
                Individual indiv1 = pop.GerRandomParent();
                Individual indiv2 = pop.GerRandomParent();
                var breakcount = 20;
                while(indiv1.GrayGene == indiv2.GrayGene && breakcount!=0)
                {
                    indiv2 = pop.GerRandomParent();
                    breakcount--;
                }
                Individual newIndiv = Crossover(indiv1, indiv2);
                newPopulation.SaveIndividual(i, newIndiv);
            }
            // Мутация популяции
            for (int i = elitismOffset; i < newPopulation.Individuals.Length; i++)
            {
                Mutate(newPopulation.Individuals[i]);
            }
            return newPopulation;
        }

        private static Individual Crossover(Individual indiv1, Individual indiv2)
        {
            Individual newSol = new Individual();
            newSol.Parents[0] = indiv1;
            newSol.Parents[1] = indiv2;
            byte newGene = 0;
            // Цикл по геному
            var rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                double chance = rnd.Next(0, 10000) / 10000.0;
                // Кроссовер
                if (chance <= uniformRate)
                {
                    var gene = indiv1.GrayGene;
                    byte mask = (byte)Math.Pow(2, i);
                    if ((gene & mask) != 0)
                    {
                        newGene += mask;
                    }
                }
                else
                {
                    byte gene =(byte)indiv2.GrayGene;
                    byte mask = (byte)Math.Pow(2, i);
                    if ((gene & mask) != 0)
                    {
                        newGene += mask;
                    }                
                }
            }
            newSol.GrayGene = newGene;
            return newSol;
        }

        // Мутация особи
        private static void Mutate(Individual indiv)
        {
            var rnd = new Random();
            double chance = rnd.Next(0, 10000) / 10000.0;
            if (chance <= mutationRate)
            {
                var geneIndex = rnd.Next(0, 8);
                indiv.Type = IndividualType.Mutant;
                byte gene = (byte)indiv.GrayGene;
                // Создаем маску, чтобы узнать наличие бита
                byte mask = (byte)Math.Pow(2, geneIndex);
                if ((gene & mask) != 0)
                {
                    indiv.GrayGene -= mask;
                }
                else indiv.GrayGene += mask;
            }
        }
    }


}
