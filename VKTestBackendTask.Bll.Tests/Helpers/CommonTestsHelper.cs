using Mapster;
using MapsterMapper;
using VKTestBackendTask.Bll.Services.Implementations;

namespace VKTestBackendTask.Bll.Tests.Helpers;

public class CommonTestsHelper
{
    internal static Mapper CreateMapper()
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(UserService).Assembly);
        var mapper = new Mapper(config);
        return mapper;
    }
}