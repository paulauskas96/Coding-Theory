using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodingTheory
{
    class BitSequenceConverter
    {
        public static string ToText(int[] bits)
        {
            return Encoding.UTF8.GetString(GetByteArray(bits));
        }

        //Writes bits to .bmp file and returns its location
        public static string ToImage(int[] bits, int no)
        {
            String path = Directory.GetCurrentDirectory() + @"\output" + no +".bmp";
            byte[] bytes = GetByteArray(bits);
            File.WriteAllBytes(path, bytes);
            return path;
        }

        public static int[] FromText(string text)
        {
            var bits = string.Join("", Encoding.UTF8.GetBytes(text)
                .Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')))
                .Select(chr => int.Parse(chr.ToString()));

            return GetBitArray(Encoding.UTF8.GetBytes(text));
        }

        public static int[] FromImage(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);

            return GetBitArray(bytes);
        }

        public static int[] GetBitArray(string input)
        {
            var bits = new List<int>();

            foreach (var bit in input.ToArray())
                bits.Add(int.Parse(bit.ToString()));

            return bits.ToArray();
        }

        public static int[] GetBitArray(byte[] bytes)
        {
            return string.Join("", bytes
                .Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')))
                .Select(chr => int.Parse(chr.ToString())).ToArray();
        }

        private static byte[] GetByteArray(int[] bits)
        {
            var bitList = bits.ToList();

            byte[] bytes = new byte[bitList.Count / 8];

            for (int i = 0; i < bitList.Count / 8; i++)
                bytes[i] = BitsToByte(bitList.GetRange(i * 8, 8));

            return bytes;
        }

        private static byte BitsToByte(List<int> byteBits)
        {
            int byteSum = 0;

            for (int i = 0, factor = 128; i < 8; i++, factor /= 2)
                byteSum += byteBits[i] == 1 ? factor : 0;

            return (byte)byteSum;
        }
    }
}
