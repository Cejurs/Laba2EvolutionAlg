using System;

namespace Evolution.Models
{
    public class EvolutionAlgorithm
    {
        private static double uniformRate = 0.5;
        private static double[] mutationRate = new double[8] { 0.15, 0.15, 0.15, 0.1, 0.07,0.05,0,0};
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
                while(indiv1.Gene == indiv2.Gene && breakcount!=0)
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
                    byte gene =(byte)indiv1.Gene;
                    byte mask = (byte)Math.Pow(2, i);
                    if ((gene & mask) != 0)
                    {
                        newGene += mask;
                    }
                }
                else
                {
                    byte gene =(byte)indiv2.Gene;
                    byte mask = (byte)Math.Pow(2, i);
                    if ((gene & mask) != 0)
                    {
                        newGene += mask;
                    }                
                }
            }
            newSol.Gene = (sbyte)newGene;
            return newSol;
        }

        // Мутация особи
        private static void Mutate(Individual indiv)
        {
            var rnd = new Random();
            for(int i = 0; i < 8; i++)
            {
                double chance = rnd.Next(0, 10000) / 10000.0;
                if(chance <= mutationRate[i])
                {
                    indiv.Type = IndividualType.Mutant;
                    byte gene =(byte)indiv.Gene;
                    // Создаем маску, чтобы узнать наличие бита
                    byte mask = (byte)Math.Pow(2, i);
                    if ((gene&mask)==0)
                    {
                        indiv.Gene +=(sbyte)mask;
                    }
                    else indiv.Gene-= (sbyte)mask;
                }
            }
        }
    }


}
