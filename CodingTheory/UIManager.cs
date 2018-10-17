using System;
using System.IO;
using System.Linq;

namespace CodingTheory
{
    class UIManager
    {
        public int Choice;

        public int[] ReadInput()
        {
            while (true)
            {
                Console.WriteLine("Pasirinkite:\n1. Įvesti bitų seką.\n2. Įvesti tekstą.\n3. Nurodyti paveiksliuką.");
                var text = Console.ReadLine();

                int.TryParse(text, out Choice);
                switch (this.Choice)
                {
                    case 1:
                        return ReadBitSequence();
                    case 2:
                        return ReadText();
                    case 3:
                        return ReadImage();
                    default:
                        Console.WriteLine("Tokio pasirinkimo nėra!\n");
                        continue;
                }
            }
        }

        public double ReadProbability()
        {
            while (true)
            {
                Console.WriteLine("Įveskite tikimybę (nuo 0 iki 1 imtinai):");
                var text = Console.ReadLine();

                double probability;

                if (double.TryParse(text, out probability) && probability >= 0 && probability <= 1)
                    return probability;

                Console.WriteLine("Įvestis " + text + " nėra tikimybė!\n");
            }
        }

        private static int[] ReadBitSequence()
        {
            return ReadBitSequence(null);
        }
        private static int[] ReadBitSequence(int? length)
        {
            while (true)
            {
                if (length == null)
                    Console.WriteLine("Įveskite bitų seką:");
                else
                    Console.WriteLine("Įveskite bitų seką (simbolių: " + length + ")");

                var text = Console.ReadLine();

                var inputCorrect = true;

                if (length != null && text.Length != length)
                {
                    Console.WriteLine("Neatitinka simbolių eilutės ilgis.");
                    continue;
                }

                foreach (var bit in text.ToArray())
                {
                    if (bit != '0' && bit != '1')
                    {
                        inputCorrect = false;
                        Console.WriteLine("Simbolis " + bit + " nėra bitas!\n");
                        break;
                    }
                }

                if (inputCorrect)
                    return BitSequenceConverter.GetBitArray(text);
            }
        }

        private static int[] ReadText()
        {
            Console.WriteLine("Įveskite tekstą:");
            var text = Console.ReadLine();

            return BitSequenceConverter.FromText(text);
        }

        private static int[] ReadImage()
        {
            while (true)
            {
                Console.WriteLine("Įveskite .bmp paveikslėlio adresą:");
                var path = Console.ReadLine();

                if (!File.Exists(path))
                {
                    Console.WriteLine("Failo tokiu adresu nėra.");
                    continue;
                }

                if (Path.GetExtension(path) != ".bmp")
                {
                    Console.WriteLine("Tai nėra .bmp failas.");
                    continue;
                }

                return BitSequenceConverter.FromImage(path);
            }
        }

        public void Log(string label, int[] bits)
        {
            Log(label, string.Join("", bits));
        }

        public void Log(string label, string text)
        {
            Console.WriteLine(string.Format("{0}:\n{1}", label, text));
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowOutput(int[] bits, int no)
        {
            Log("Atkoduotas vektorius", bits);

            if (Choice == 2)
                Log("Tekstas po siuntimo", BitSequenceConverter.ToText(bits));
            else if (Choice == 3)
                Log("Paveikslėlis po siuntimo išsaugotas adresu", BitSequenceConverter.ToImage(bits, no));

            if (no == 2)
              Console.ReadLine();
        }

        public int[] EditBeforeDecoding(int[] bits)
        {
            Console.WriteLine("Ar norite redaguoti vektorių rankiniu būdu prieš dekoduojant? (Y/N)");
            if (!string.Equals(Console.ReadLine(), "Y", StringComparison.InvariantCultureIgnoreCase))
                return bits;

            return ReadBitSequence(bits.Length);
        }
    }
}
