namespace HelpfulHaversack.Services.ContainerAPI.Data
{
    public class Encrypter
    {
        private readonly Encrypter _instance = new();

        private Encrypter()
        {
            _instance = new Encrypter();
        }

        private Encrypter getInstance { get { return _instance; } }

        public string Encrypt(string input)
        {
            return string.Empty;
        }

        public string Decrypt(string input)
        {
            return string.Empty;
        }
    }
}
