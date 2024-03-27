namespace WindowsFormsApp1.entity
{
    internal class RedisServer
    {
        private string host;
        private int port;
        private string username;
        private string password;

        public string Host { get => host; set => host = value; }
        public int Port { get => port; set => port = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}
