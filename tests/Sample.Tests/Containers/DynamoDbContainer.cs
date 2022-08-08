using System.Text;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Logging;

namespace Sample.Tests.Containers;


public sealed class DynamoDbTestcontainer : TestcontainerDatabase
{
    internal DynamoDbTestcontainer(ITestcontainersConfiguration configuration, ILogger logger)
        : base(configuration, logger)
    {
    }

    /// <inheritdoc />
    public override string ConnectionString
        => $"http://{Hostname}:{Port}";
    
}

public class DynamoDbTestcontainerConfiguration : TestcontainerDatabaseConfiguration
{
    private const string DynamoDbImage = "amazon/dynamodb-local:latest";

    private const int DynamoDbPort = 8000;
    
    /// <inheritdoc />
    public override string Database
    {
        get => string.Empty;
        set => throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override string Username
    {
        get => string.Empty;
        set => throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override string Password
    {
        get => string.Empty;
        set => throw new NotImplementedException();
    }
    
    public DynamoDbTestcontainerConfiguration()
        : this(DynamoDbImage)
    {
    }
    
    public DynamoDbTestcontainerConfiguration(string image)
        : base(image, DynamoDbPort)
    {
    }


    /// <inheritdoc />
    public override IWaitForContainerOS WaitStrategy => Wait.ForUnixContainer();
}
