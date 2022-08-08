using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Baseline;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Sample.Infrastructure.Persistence.Model;

namespace Sample.Tests.Containers;

public sealed class DynamoDbFixture : IAsyncLifetime, IDisposable
{
    private readonly TestcontainerDatabaseConfiguration configuration = new DynamoDbTestcontainerConfiguration();

    public DynamoDbTestcontainer DynamoDbContainer { get; }
    internal IAmazonDynamoDB DynamoDb { get; private set; }

    public DynamoDbFixture()
    {
        this.DynamoDbContainer = new TestcontainersBuilder<DynamoDbTestcontainer>()
            .WithDatabase(this.configuration)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await this.DynamoDbContainer.StartAsync()
            .ConfigureAwait(false);

        DynamoDb = new AmazonDynamoDBClient(new AmazonDynamoDBConfig()
        {
            ServiceURL = DynamoDbContainer.ConnectionString,
            UseHttp = true,
        });
        
        await DynamoDb.CreateTableAsync(new CreateTableRequest()
        {
            TableName = "ProductCatalog",
            AttributeDefinitions = new List<AttributeDefinition>()
            {
              new(nameof(DynamoDbProduct.Id), ScalarAttributeType.N)
            },
            KeySchema = new List<KeySchemaElement>() { new(nameof(DynamoDbProduct.Id), KeyType.HASH) },
            ProvisionedThroughput = new ProvisionedThroughput(1,1)
        });
    }

    public async Task DisposeAsync()
    {
        await this.DynamoDbContainer.DisposeAsync()
            .ConfigureAwait(false);
    }

    public void Dispose()
    {
        this.DynamoDb.SafeDispose();
        this.configuration.Dispose();
    }
}