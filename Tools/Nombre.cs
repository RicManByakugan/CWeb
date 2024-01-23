namespace CWeb.Tools
{
    public class Nombre
    {
        public string GenerateRandomNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(100, 10000);
            return randomNumber.ToString();
        }
    }
}
