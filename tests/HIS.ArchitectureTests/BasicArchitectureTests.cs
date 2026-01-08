using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace HIS.ArchitectureTests;

public class BasicArchitectureTests
{
    [Fact]
    public void All_Assemblies_Should_Load_Successfully()
    {
        // Arrange & Act
        var apiAssembly = Assembly.Load("HIS.Api");
        var applicationAssembly = Assembly.Load("HIS.Application");
        var domainAssembly = Assembly.Load("HIS.Domain");
        var infrastructureAssembly = Assembly.Load("HIS.Infrastructure");

        // Assert
        apiAssembly.Should().NotBeNull();
        applicationAssembly.Should().NotBeNull();
        domainAssembly.Should().NotBeNull();
        infrastructureAssembly.Should().NotBeNull();
    }

    [Fact]
    public void Test_Framework_Is_Working()
    {
        // Arrange
        var assembly = Assembly.Load("HIS.Api");

        // Act
        var result = Types.InAssembly(assembly)
            .That()
            .AreClasses()
            .GetTypes();

        // Assert
        result.Should().NotBeEmpty();
    }
}
