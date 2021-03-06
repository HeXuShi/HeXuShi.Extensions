# HeXuShi.Extensions


# HeXuShi.Extensions.NoWWW
[nuget install](https://www.nuget.org/packages/HeXuShi.Extensions.NoWWW/)

change www.site.com to site.com in asp.net core web app.

```csharp
using HeXuShi.Extensions;

public class Startup
{
    ...
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        ...
        app.UseNoWWW();
        ...
    }
}
```

# HeXuShi.Extensions.IsChinaIp

[nuget install](https://www.nuget.org/packages/HeXuShi.Extensions.IsChinaIp/)

[Use example](https://github.com/HeXuShi/HeXuShi.Extensions/blob/master/sample/TestChinaIp/Program.cs)

```csharp
IsChinaIp.Setup();//Setup can be called multiple times, but only on the first initialization
Console.WriteLine("ipv4 test:" + IsChinaIp.VerifyIPv4("183.192.62.65"));
Console.WriteLine("ipv6 test:" + IsChinaIp.VerifyIPv6("2400:da00::6666"));
```

[run test result](https://github.com/HeXuShi/HeXuShi.Extensions/blob/master/isChinaIp_runTestResult.md) :

```powershell
49) ipv4 test:True
runTime:00:00:00.1628871
49) ipv6 test:True
runTime:00:00:00.0010261
48) ipv4 test:True
runTime:00:00:00.0006312
48) ipv6 test:True
runTime:00:00:00.0001385
47) ipv4 test:True
runTime:00:00:00.0008418
47) ipv6 test:True
runTime:00:00:00.0001390
46) ipv4 test:True
runTime:00:00:00.0006808
46) ipv6 test:True
runTime:00:00:00.0001324
45) ipv4 test:True
runTime:00:00:00.0005952
45) ipv6 test:True
runTime:00:00:00.0001365
44) ipv4 test:True
runTime:00:00:00.0006064
44) ipv6 test:True
runTime:00:00:00.0001523
43) ipv4 test:True
runTime:00:00:00.0007299
43) ipv6 test:True
runTime:00:00:00.0001387
42) ipv4 test:True
runTime:00:00:00.0006187
```

# HeXuShi.Extensions.JumpToCN
[nuget install](https://www.nuget.org/packages/HeXuShi.Extensions.JumpToCN/)

[Demo site](http://jumptotest.killsb.com)

When you use China mainland ip access, change xx.com or xx.anything to xx.cn,help you jump to cn(china) domain suffix.


No longer based on this project https://github.com/lionsoul2014/ip2region/, But still very grateful lionsoul2014

Because we only need to simply query Chinese ip, so we don't need to pre-install any project files (such as db files).


Use examples:

normal example first :

```csharp
using HeXuShi.Extensions;

public class Startup
{
    ...
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        ...
        app.JumpToCN();
        ...
    }
}
```

normal example second :

```csharp
using HeXuShi.Extensions;

public class Startup
{
    ...
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        ...
        app.JumpToCN(".com");
        ...
    }
}
```

complex example first :

```csharp
using HeXuShi.Extensions;

public class Startup
{
    ...
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        ...
        app.ComplexJumpToCN(".cn", string.Empty, HeXuShi.Extensions.JumpOption.OnlyTo_SpecSuffix);
        ...
    }
}
```

complex example second :

```csharp
using HeXuShi.Extensions;

public class Startup
{
    ...
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        ...
        app.ComplexJumpToCN("asp.net", string.Empty, HeXuShi.Extensions.JumpOption.OnlyTo_SpecDomain);
        ...
    }
}
```
complex example third :

```csharp
using HeXuShi.Extensions;

public class Startup
{
    ...
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        ...
        app.ComplexJumpToCN("asp.net", "github.com", HeXuShi.Extensions.JumpOption.DomainTo_SpecDomain);
        ...
    }
}
```

[run test result](https://github.com/HeXuShi/HeXuShi.Extensions/blob/master/isChinaIp_runTestResult.md) :

```powershell
49) ipv4 test:True
runTime:00:00:00.1628871
49) ipv6 test:True
runTime:00:00:00.0010261
48) ipv4 test:True
runTime:00:00:00.0006312
48) ipv6 test:True
runTime:00:00:00.0001385
```
## HeXuShi.Extensions.JumpToCN normal api

This project normal api(app.JumpToCN) will run into 3 states:

first state:

```csharp
using HeXuShi.Extensions;

public class Startup
{
    ...
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        ...
        app.JumpToCN();
        ...
    }
}
```

When you use China mainland ip access, you will be redirected to the .cn suffix domain name, but if you currently use the .cn suffix domain name to access, regardless of any region's ip access, the current suffix domain name will be maintained.

second state:


```csharp
using HeXuShi.Extensions;

public class Startup
{
    ...
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        ...
        app.JumpToCN(".com");
        ...
    }
}
```

When you use China mainland ip access, you will be redirected to the .cn suffix domain name. If it is a non-Chinese mainland ip access, use the .cn suffix domain name to access, it will jump to the specified .com suffix domain name.

third state:

The current region is not found, the query module has an exception.the current suffix domain name will be maintained.

## HeXuShi.Extensions.JumpToCN complex api

This project complex api(app.ComplexJumpToCN) has four options to build multiple possibilities

```csharp
    public enum JumpOption
    {
        OnlyTo_SpecSuffix,//Only jump to specify suffix
        OnlyTo_SpecDomain,//Only jump to specify domain
        SuffixTo_SpecSuffix,//specify suffix to specify suffix,
        DomainTo_SpecDomain,//specify domain to specify domain,
    }
```

I hope that you have not been confused, in the final analysis, just check the suffix, domain name, and mainland China ip, judge the jump

[Learning through unit testing](https://github.com/HeXuShi/HeXuShi.Extensions/blob/master/Tests/JumpToCNTests/TestJumpToHandleRequest.cs)

### HeXuShi.Extensions.JumpToCN OnlyTo_SpecSuffix

## HeXuShi.Extensions.BlockChina normal api

run:

```csharp
using HeXuShi.Extensions;

public class Startup
{
    ...
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        ...
        app.BlockChina();
        ...
    }
}
```