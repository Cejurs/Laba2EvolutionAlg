using System.Text;
using System;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Evolution.Models
{
    public enum IndividualType {
        Normal,
        Mutant
    }
    // Особь для эволюции
    public class Individual
    {
        public sbyte Gene { get; set; }

        public Individual[] Parents { get;set; }
        public IndividualType Type { get; set; }

        public Individual(IndividualType IndividualType=IndividualType.Normal)
        {
            var random = new Random();
            var intvalue= random.Next(-10, 53);
            Gene = (sbyte)intvalue;
            Type = IndividualType;
            Parents = new Individual[2];
        }

        public int GetFittness()
        {
            return Math.Abs(29-Gene);
        }

        public override string ToString()
        {
            var builder=new StringBuilder();
            builder.Append($"Гены: {GeneToString()} ; x ={Gene}; Type: {Type} Приближение: {GetFittness()}");
            if (Parents[0] != null && Parents[1] != null)
                builder.Append($"\n Родители: {Parents[0].GeneToString()} {Parents[1].GeneToString()}");
            return builder.ToString();
        }

        private string GeneToString()
        {
            var bits = (byte)Gene;
            var bitsString = new StringBuilder(8);

            bitsString.Append(Convert.ToString((bits / 128) % 2));
            bitsString.Append(Convert.ToString((bits / 64) % 2));
            bitsString.Append(Convert.ToString((bits / 32) % 2));
            bitsString.Append(Convert.ToString((bits / 16) % 2));
            bitsString.Append(Convert.ToString((bits / 8) % 2));
            bitsString.Append(Convert.ToString((bits/ 4) % 2));
            bitsString.Append(Convert.ToString((bits / 2) % 2));
            bitsString.Append(Convert.ToString((bits / 1) % 2));

            return bitsString.ToString();
        }
    }
}
