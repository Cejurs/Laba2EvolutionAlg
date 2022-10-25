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
        //Gray
        public byte GrayGene { get; set; }

        public Individual[] Parents { get;set; }
        public IndividualType Type { get; set; }

        public Individual(IndividualType IndividualType=IndividualType.Normal)
        {
            var random = new Random();
            var randomInt=random.Next(0, Function.Right + Function.Left);
            GrayGene =(byte) BinToGray(randomInt);
            Type = IndividualType;
            Parents = new Individual[2];
        }

        public int GetFittness()
        {
            return Function.GetY(GetX());
        }
        public override string ToString()
        {
            var builder=new StringBuilder();
            builder.Append($"Гены: {GeneToString()} ; x ={GrayDecode(GrayGene)-Function.Left}; Type: {Type} Приближение: {GetFittness()}");
            if (Parents[0] != null && Parents[1] != null)
                builder.Append($"\n Родители: {Parents[0].GeneToString()} {Parents[1].GeneToString()}");
            return builder.ToString();
        }
        public int GetX()
        {
            return GrayDecode(GrayGene) - Function.Left;
        }
        private int BinToGray(int b)
        {
            return b ^ (b >> 1);
        }
        private int GrayDecode(int gray)
        {
            var g= 0;
            while (gray > 0)
            {
                g= g^gray;
                gray=  gray >> 1;
            }
            return g;
        }
        private string GeneToString()
        {
            var bits = GrayGene;
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
