namespace Identity.Test
{
    using Controllers;
    using Data;
    using Data.Wrapper;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using MockQueryable.Moq;
    using Models;
    using Moq;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using ViewModels;
    using IConfiguration=Microsoft.Extensions.Configuration.IConfiguration;

    
    public class AccountControllerTest
    {
        [Fact]
        public async void GenerateTokenMethodEncryptsNecessaryClaims()
        {
            //arrange
            var user = new User
            {
                Id = Guid.Parse("d28cef72-6179-4f1b-838c-83111b5d31da"), 
                UserName = "Dmytro", 
                Role = "Master", 
                PasswordHash = "hello",
                ReturnUrl = "string",
            };
            
            const string password = "mypassword";
            var mockDbSet = new List<User>{user}.AsQueryable().BuildMockDbSet();
            
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            
            var mockContext = new Mock<AuthDbContext>(options)
            {
                Object = {Users = mockDbSet.Object},
            };

            var unitOfWork = new UnitOfWorkAuth(mockContext.Object);

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["Jwt:Issuer"]).Returns(() => "CamelotIssuer");
            mockConfig.Setup(x => x["Jwt:Audience"]).Returns(() => "CamelotAudience");
            mockConfig.Setup(x => x["Jwt:SecretKey"]).Returns(() => "camelotsecretkey");

            var mockHasher = new Mock<IPasswordHashService>();
            mockHasher.Setup(x => x.Verify(password, user.PasswordHash)).Returns(true);

            var mockLog = new Mock<ILogger<AccountController>>(); 
            
            var controller = new AccountController(mockConfig.Object, unitOfWork, mockHasher.Object, mockLog.Object);
            
            
            //act
            var actionResult = await controller.GenerateToken(user.UserName, password);
            var okResult = (OkObjectResult)actionResult.Result!;
            var response = (AuthenticationResponse)okResult.Value!;
            
            var claims = new List<Claim>();
            if (!response.Token.IsNullOrEmpty())
                claims = new JwtSecurityTokenHandler().ReadJwtToken(response.Token).Claims.ToList();


            //assert
            Assert.IsType<AuthenticationResponse>(okResult.Value);
            Assert.NotNull(response.Token);
            Assert.NotEmpty(claims);
            Assert.Equal(response.UserName, user.UserName);
            Assert.Equal("id: " + user.Id, claims[0].ToString());
            Assert.Equal("name: "+ user.UserName, claims[1].ToString());
            Assert.Equal("role: " + user.Role, claims[2].ToString());
        }
    }
}
