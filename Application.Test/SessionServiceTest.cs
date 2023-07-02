namespace Application.Test;

using global::Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using Services.SessionService;

public class SessionServiceTest
{

    [Fact]
    public void SetItemShouldSetValueInSession()
    {
        // Arrange
        var contextAccessorMock = new Mock<IHttpContextAccessor>();
        var httpContextMock = new Mock<HttpContext>();
        var sessionMock = new Mock<ISessionWrapper>();
        
        contextAccessorMock.SetupGet(a => a.HttpContext).Returns(httpContextMock.Object);
        httpContextMock.SetupGet(c => c.Session).Returns(sessionMock.Object);

        var sessionService = new SessionService(contextAccessorMock.Object);
        var key = "TestKey";
        var value = "TestValue";

        // Act
        sessionService.SetItem(key, value);

        // Assert
        sessionMock.Verify(s => s.SetString(key, It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void GetItemWithInvalidKeyShouldReturnDefaultValue()
    {
        //arrange
        var contextAccessorMock = new Mock<IHttpContextAccessor>();
        var sessionMock = new Mock<ISession>();
        contextAccessorMock.SetupGet(a => a.HttpContext!.Session).Returns(sessionMock.Object);
        var sessionService = new SessionService(contextAccessorMock.Object);
        var key = "TestKey";

        //act
        var result = sessionService.GetItem<string>(key);

        //assert
        Assert.Equal(default(string), result);
    }
}
