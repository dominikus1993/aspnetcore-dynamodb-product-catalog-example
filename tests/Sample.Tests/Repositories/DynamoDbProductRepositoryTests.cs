using Amazon.DynamoDBv2.DataModel;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Sample.Core.Model;
using Sample.Infrastructure.Persistence.Repositories;
using Sample.Tests.Containers;

namespace Sample.Tests.Repositories;

public class DynamoDbProductRepositoryTests : IClassFixture<DynamoDbFixture>
{
    private DynamoDbFixture _dynamodb;

    public DynamoDbProductRepositoryTests(DynamoDbFixture fixture)
    {
        _dynamodb = fixture;
    }
    [Fact]
    public async Task TestRead()
    {
        // arrange
        using var contex = new DynamoDBContext(_dynamodb.DynamoDb);
        var repo = new DynamoDbProductRepository(contex, LoggerFactory.Create(log => log.AddConsole()).CreateLogger<DynamoDbProductRepository>());

        await repo.AddProductsAsync(new[] { new Product(1, "xD") }, CancellationToken.None);
        
        // act

        var product = await repo.GetProduct(1, CancellationToken.None);
        
        // assert

        product.Should().NotBeNull();
        product.Id.Should().Be(1);
        product.Name.Should().NotBeNullOrEmpty();
        product.Name.Should().Be("xD");
    }
}