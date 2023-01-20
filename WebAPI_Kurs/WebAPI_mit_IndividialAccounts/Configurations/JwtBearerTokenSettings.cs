namespace WebAPI_mit_IndividialAccounts.Configurations
{
    public class JwtBearerTokenSettings
    {
        //Properties müssen selben Namen vorweisen, wie in der appsettings.json
        /*
         *  "JwtBearerTokenSettings": {
                "SecretKey": "ThisIsSomeSampleSymmetricEncryptionKey",
                "Audience": "https://localhost:44322/",
                "Issuer": "https://localhost:44322/",
                "ExpiryTimeInSeconds": 60
            }
         */

        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiryTimeInSeconds { get; set; }
    }
}
