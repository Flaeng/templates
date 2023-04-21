using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace CSharp.GraphQL.Tests.Mutations;

public class CreateListTests
{
    [Test]
    public async Task Can_not_create_list_when_unauthorized()
    {
        // Arrange
        var mutation = new Mutation();
        var todoListService = new Mock<ITodoListService>();
        var userService = new Mock<IUserService>();
        var contextAccessor = new Mock<IHttpContextAccessor>();

        // Act
        var result = await mutation.CreateList(
            todoListService.Object,
            userService.Object,
            contextAccessor.Object,
            new CreateListModel("Titel"),
            CancellationToken.None
        );

        // Assert
        Assert.IsNull(result);
        todoListService.Verify(x => x.CreateAsync(It.IsAny<TodoList>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task Can_create_list()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mutation = new Mutation();
        var todoListService = new Mock<ITodoListService>();
        todoListService
            .Setup(x => x.CreateAsync(It.IsAny<TodoList>(), It.IsAny<CancellationToken>()))
            .Returns<TodoList, CancellationToken>((list, token) =>
            {
                list.Id = Guid.NewGuid();
                return Task.FromResult(list);
            });

        var userService = new Mock<IUserService>();
        userService
            .Setup(x => x.FindByIdAsync(It.Is<Guid>(x => x == userId), It.IsAny<CancellationToken>()))
            .Returns(async () => await ValueTask.FromResult(new User { }));

        var contextAccessor = new Mock<IHttpContextAccessor>();
        var ctx = new Mock<HttpContext>();
        ctx.Setup(x => x.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        })));
        contextAccessor
            .Setup(x => x.HttpContext)
            .Returns(ctx.Object);

        // Act
        var result = await mutation.CreateList(
            todoListService.Object,
            userService.Object,
            contextAccessor.Object,
            new CreateListModel("Titel"),
            CancellationToken.None
        );

        // Assert
        Assert.IsNotNull(result);
        todoListService
            .Verify(x => x.CreateAsync(It.IsAny<TodoList>(), It.IsAny<CancellationToken>()), Times.Once());
    }

}