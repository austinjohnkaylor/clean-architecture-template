using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.EntityFrameworkCore.Storage;
using Source.Infrastructure.EntityFramework;
using Tests.Shared.CustomXunitTraits;

namespace Tests.Infrastructure.UnitTests;

[UnitTest]
public class UnitOfWorkTests
{
    [Fact]
    public async Task SaveChangesAsync_CallsSaveChangesOnDbContext()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UnitOfWork>>();
        var mockContext = new Mock<DatabaseContext>();
        UnitOfWork unitOfWork = new(mockLogger.Object, mockContext.Object);

        // Act
        await unitOfWork.SaveChangesAsync();

        // Assert
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(Skip = "Does not work as expected. Need to investigate further.")]
    public async Task BeginTransactionAsync_CallsBeginTransactionOnDbContext()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UnitOfWork>>();
        var mockContext = new Mock<DatabaseContext>();
        UnitOfWork unitOfWork = new(mockLogger.Object, mockContext.Object);

        // Act
        await unitOfWork.BeginTransactionAsync();

        // Assert
        mockContext.Verify(c => c.Database.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(Skip = "Does not work as expected. Need to investigate further.")]
    public async Task CommitTransactionAsync_CallsCommitOnTransaction()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UnitOfWork>>();
        var mockContext = new Mock<DatabaseContext>();
        var mockTransaction = new Mock<IDbContextTransaction>();
        mockContext.Setup(c => c.Database.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockTransaction.Object);
        UnitOfWork unitOfWork = new(mockLogger.Object, mockContext.Object);
        await unitOfWork.BeginTransactionAsync();

        // Act
        await unitOfWork.CommitTransactionAsync();

        // Assert
        mockTransaction.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(Skip = "Does not work as expected. Need to investigate further.")]
    public async Task RollbackTransactionAsync_CallsRollbackOnTransaction()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UnitOfWork>>();
        var mockContext = new Mock<DatabaseContext>();
        var mockTransaction = new Mock<IDbContextTransaction>();
        mockContext.Setup(c => c.Database.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockTransaction.Object);
        UnitOfWork unitOfWork = new(mockLogger.Object, mockContext.Object);
        await unitOfWork.BeginTransactionAsync();

        // Act
        await unitOfWork.RollbackTransactionAsync();

        // Assert
        mockTransaction.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}