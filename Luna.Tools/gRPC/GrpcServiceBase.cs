using Microsoft.Extensions.Configuration;

namespace Luna.Tools.gRPC;

public class GrpcServiceBase
{
	public GrpcServiceBase(IConfiguration configuration)
	{
		Host = configuration["Host"] ?? throw new ArgumentNullException(nameof(Host));
	}

	public GrpcServiceBase(String host)
	{
		Host = host;
	}

	public String Host { get; }
}