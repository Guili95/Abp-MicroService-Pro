<<<<<<< HEAD
﻿using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Guili.SaasService.Pages;

public class Index_Tests : SaasServiceWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
=======
﻿using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Guili.SaasService.Pages;

public class Index_Tests : SaasServiceWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
>>>>>>> git/ids4
