namespace Auth
{
    public class JsonWebToken
    {
        public required string Token { get;  set; }
        public string? RefreshToken { get; set; }
        public DateTime Expiry { get;  set; }

        //public JsonWebToken(string token, DateTime expiry)
        //{
        //    Token = token;
        //    Expiry = expiry;
        //}
    }


}
