using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutomatedTestingApp.Areas.Identity.Controllers;
using AutomatedTestingApp.Areas.Identity.Models;
using AutomatedTestingApp.Infrastructure.Repositories;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AutomatedTestingApp.Tests.Areas.Identity.Controllers;

[TestSubject(typeof(AccountController))]
public class AccountControllerTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IRepository<IdentityUser>> _userRepositoryMock = new();

    [Fact]
    public async Task Login_WhenUserDoesNotExists_ReturnView()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.Get(
            It.IsAny<Expression<Func<IdentityUser, bool>>>(), It.IsAny<Func<IQueryable<IdentityUser>,
                IOrderedQueryable<IdentityUser>>>())).Returns(new List<IdentityUser>());
        _unitOfWorkMock.Setup(x => x.GetRepository<IdentityUser>()).Returns(_userRepositoryMock.Object);
        var controller = new AccountController(_unitOfWorkMock.Object);

        // Act
        var result = await controller.Login("Pepa", "Heslo1234", "returnUrl");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ViewResult>(result);
        Assert.Equal("returnUrl", ((ViewResult)result).ViewData["ReturnUrl"]);
    }

    [Fact]
    public void Register_WhenUserAlreadyExists_ReturnBadRequest()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.Get(
            It.IsAny<Expression<Func<IdentityUser, bool>>>(), It.IsAny<Func<IQueryable<IdentityUser>,
                IOrderedQueryable<IdentityUser>>>())).Returns(new List<IdentityUser>
                {
                    new() { UserId = Guid.NewGuid(), Username = "Pepa", Password = "Heslo1234"}
                });
        _unitOfWorkMock.Setup(x => x.GetRepository<IdentityUser>()).Returns(_userRepositoryMock.Object);
        var controller = new AccountController(_unitOfWorkMock.Object);

        // Act
        var result = controller.Register("Pepa", "Heslo1234");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("User already exist", ((BadRequestObjectResult)result).Value);
    }
}