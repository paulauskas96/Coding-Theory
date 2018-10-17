using System;
using System.Collections.Generic;

namespace CodingTheory
{
    class Channel
    {
        public double Probability { get; set; }
        private Random Randomizer;
        
        public Channel(double probability)
        {
            Probability = probability;
            Randomizer = new Random();
        }

        public int[] Send(int[] bits)
        {
            var receivedBits = new List<int>();

            foreach (var bit in bits)
            {
                if (IsCorrupted())
                    receivedBits.Add((bit + 1) % 2);
                else
                    receivedBits.Add(bit);
            }

            return receivedBits.ToArray();
        }

        private bool IsCorrupted()
        {
            if (Randomizer.NextDouble() <= Probability)
                return Probability != 0;

            return false;
        }
    }
}
