namespace BlockRouter.WebAPI.Models
{
    public class User
    {
        public string Token { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public User()
        {
            Login = string.Empty;
            Password = string.Empty;
        }

        public User(string token, string login, string password)
        {
            Token = token;
            Login = login;
            Password = password;
        }
    }
}
