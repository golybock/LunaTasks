using Microsoft.Extensions.Configuration;

namespace Luna.Tools.gRPC;

public abstract class GrpcServiceBase
{
	protected GrpcServiceBase(IConfiguration configuration)
	{
		Host = configuration["Host"] ?? throw new ArgumentNullException(nameof(Host));
	}

	protected GrpcServiceBase(String host)
	{
		Host = host;
	}

	public String Host { get; }
}