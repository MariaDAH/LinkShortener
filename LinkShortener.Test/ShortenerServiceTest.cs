using LinkShortener.Application.Models;
using LinkShortener.Application.Services;
using LinkShortener.Infrastructure.Daos;
using LinkShortener.Infrastructure.Repositories;
using LinkShortener.Infrastructure.Services;
using Moq;

namespace LinkShortShareTest;

[TestFixture]
public class ShortenerServiceTest
{
    private IShortenerService _shortenerService;
    private Mock<ApplicationDbContext> _dbContext;
    private Mock<IRepository<Url>> _repository;
    private Mock<IUnitOfWork> _uow;
    private Mock<IUserService> _userService;

    [OneTimeSetUp]
    public void Setup()
    {
        _repository = new Mock<IRepository<Url>>();
        _dbContext = new Mock<ApplicationDbContext>();
        _uow = new Mock<IUnitOfWork>();
        _userService = new Mock<IUserService>();
    }

    [Test]
    public async Task GetLinkAndHashNotFoundReturnNull()
    {
        //Arrange
        _dbContext.Setup(db => db.Urls);
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<object>)).ReturnsAsync((Url)null);
        _uow.Setup(x => x.GetRepository<Url>()).Returns(_repository.Object);
        var sut = new ShortenerService(_uow.Object, _userService.Object);
        
        
        //Act
        var link = await sut.GetLink("non-existing-hash", default);
        
        //Assert
        Assert.IsNull(link);
    }
    
    [Test]
    public async Task UnknownUserCannotCreateAShortLinkAndThrowException()
    {
     
        //Arrange
        var name = "non exists";
        _dbContext.Setup(db => db.Urls);
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<object>)).ReturnsAsync((Url)null);
        _uow.Setup(x => x.GetRepository<Url>()).Returns(_repository.Object);
        _userService.Setup(x => x.GetUserByNameOrEmail(name, null)).ReturnsAsync((User)null);
        
        var sut = new ShortenerService(_uow.Object, _userService.Object);
 
        //Act
         TestDelegate action = async () =>  await sut.AddShortLink(name, null, default);
        
        //Assert
        Assert.Throws<Exception>(() => throw new Exception("Non recognised user"));
    }

    [Test]
    public async Task CreateLinkReturnsAUrlWhenUserExists()
    {
        //Arrange
        var user = User.Create("test", "test@mail.com");
        var url = Url.Create("http://original.com", user);
        _dbContext.Setup(db => db.Urls);
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<object>)).ReturnsAsync(url);
        _uow.Setup(x => x.GetRepository<Url>()).Returns(_repository.Object);
        _userService.Setup(x => x.GetUserByNameOrEmail("test", null)).ReturnsAsync(user);
        var sut = new ShortenerService(_uow.Object, _userService.Object);
        
        //Act
        var link = await sut.AddShortLink(url.OriginalUrl, url.User.Name, default);
        
        //Assert
        Assert.That(url.User.Name, Is.EqualTo(user.Name)) ;
    }

}