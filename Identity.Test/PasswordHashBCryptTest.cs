namespace Identity.Test
{
    using Services;

    public class PasswordHashBCryptTest
    {
        [Fact]
        public void HashGeneratedInHashPasswordFunctionMustBeVerified()
        {
            //arrange
            const string password = "5#budJ#nBR";
            var hashedPassword = new List<string>();
            var hasher = new PasswordHashBCryptService();

            //act
            for(int i = 0; i < 10; i++)
            {
                hashedPassword.Add(hasher.HashPassword(password));
            }

            //assert
            foreach (string str in hashedPassword)
            {
                Assert.True(hasher.Verify(password, str));
            }
        }
    }
}
