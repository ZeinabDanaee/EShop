namespace Auth
{
    public class JwtOptions
    {
        /// <summary>
        /// صادر کننده
        /// </summary>
        public string? Issuer { get; set; }
        /// <summary>
        /// مخاطب
        /// </summary>
        public string? Audience { get; set; }
        public  string? SecretKey { get; set; }
        public int ExpiryMinutes { get; set; }


    }


}
