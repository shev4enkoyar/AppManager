using System.Reflection;
using System.Runtime.CompilerServices;
using AppManager.Application.Branches.Queries.GetBranch;
using AppManager.Application.Branches.Queries.GetBranchesWithPagination;
using AppManager.Application.Common.Interfaces;
using AppManager.Application.Versions.Queries.GetVersionsWithPagination;
using AppManager.Domain.Entities;
using AutoMapper;
using Version = AppManager.Domain.Entities.Version;

namespace Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Theory]
    [InlineData(typeof(Version), typeof(VersionBriefDto))]
    [InlineData(typeof(Branch), typeof(BranchBriefDto))]
    [InlineData(typeof(Branch), typeof(BranchDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
