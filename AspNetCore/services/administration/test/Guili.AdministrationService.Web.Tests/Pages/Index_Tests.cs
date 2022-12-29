using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Guili.AdministrationService.Pages;

public class Index_Tests : AdministrationServiceWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
