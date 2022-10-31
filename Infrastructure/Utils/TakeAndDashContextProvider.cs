using Infrastructure.Ef;

namespace Infrastructure.Utils;

public class TakeAndDashContextProvider
{
    private readonly IConnectionStringProvider _connectionStringProvider;

    public TakeAndDashContextProvider(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public TakeAndDashContext NewContext()
    {
        return new TakeAndDashContext(_connectionStringProvider);
    }
}