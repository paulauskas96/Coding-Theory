using System.Collections.Generic;

namespace CodingTheory
{
    class ConvolutionalCode
    {
        //Implements encoding algorithm for bit sequence
        public static int[] Encode(int[] bits)
        {
            List<int> encodedBits = new List<int>();

            int[] shiftRegisters = new int[6];

            for (int i = 0; i < bits.Length + 6; i++)
            {
                int bit = (i < bits.Length) ? bits[i] : 0;

                int sumBit = (bit + shiftRegisters[1] + shiftRegisters[4] + shiftRegisters[5]) % 2;
                ShiftArray(shiftRegisters, bit);
                encodedBits.Add(bit);
                encodedBits.Add(sumBit);
            }

            return encodedBits.ToArray();
        }

        //Implements decoding algorithm for bit sequence
        public static int[] Decode(int[] bits)
        {
            List<int> decodedBits = new List<int>();
            int[] bitRegisters = new int[6];
            int[] shiftRegisters = new int[6];

            for (int i = 0; i < (bits.Length - 12); i += 2)
            {
                int bit = bits[i];
                int nextBit = bits[i + 1];

                int sumBit = (bit + nextBit + bitRegisters[1] + bitRegisters[4] + bitRegisters[5]) % 2;

                int mde;
                if (sumBit + shiftRegisters[0] + shiftRegisters[3] + shiftRegisters[5] <= 2)
                    mde = 0;
                else
                    mde = 1;

                int decodedBit = (bit + mde) % 2;
                decodedBits.Add(decodedBit);

                ShiftArray(bitRegisters, bit);
                ShiftArray(shiftRegisters, sumBit);
            }

            return decodedBits.ToArray();
        }

        //Pushes an element to start of array and removes last element
        private static void ShiftArray(int[] arr, int firstBit)
        {
            for (int j = arr.Length - 1; j >0; j--)
                arr[j] = arr[j - 1];

            arr[0] = firstBit;
        }
    }
}                                