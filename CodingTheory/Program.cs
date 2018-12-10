namespace CodingTheory
{
    class Program
    {
        static void Main(string[] args)
        {
            var uiManager = new UIManager();
            var originalBits = uiManager.ReadInput();
            var bits = originalBits;

            var channel = new Channel(uiManager.ReadProbability());

            if (uiManager.Choice != 1)
            {
                uiManager.Log("SIUNČIAMA BE KODAVIMO");
                uiManager.Log("Pradinis vektorius", originalBits);
                bits = channel.Send(originalBits);
                uiManager.Log("Po siuntimo kanalu gautas vektorius", bits);

                uiManager.ShowOutput(bits, 1);
            }

            uiManager.Log("SIUNČIAMA SU KODAVIMU");
            uiManager.Log("Pradinis vektorius", originalBits);

            bits = ConvolutionalCode.Encode(originalBits);
            uiManager.Log("Užkoduotas vektorius", bits);

            bits = channel.Send(bits);
            uiManager.Log("Po siuntimo kanalu gautas vektorius", bits);

            if (uiManager.Choice == 1)
            {
                bits = uiManager.EditBeforeDecoding(bits);
            }

            bits = ConvolutionalCode.Decode(bits);
            uiManager.ShowOutput(bits, 2);
        }
    }
}
