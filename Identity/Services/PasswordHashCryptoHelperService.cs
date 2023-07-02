namespace Identity.Services
{
    using CryptoHelper;
    using Interfaces;

    public class PasswordHashCryptoHelperService : IPasswordHashService
    { 
        public string HashPassword(string password) =>
            Crypto.HashPassword(password);

        public bool Verify(string password, string hash) =>
            Crypto.VerifyHashedPassword(hash, password);
    }
}
