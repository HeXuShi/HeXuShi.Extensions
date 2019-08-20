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

# HeXuShi.Extensions.JumpToCN
[nuget install](https://www.nuget.org/packages/HeXuShi.Extensions.JumpToCN/)

change xx.com or xx.anything to xx.cn,help you jump to cn(china) domain suffix.

No longer based on this project https://github.com/lionsoul2014/ip2region/, But still very grateful lionsoul2014

Because we only need to simply query Chinese ip, so we don't need to pre-install any project files (such as db files).

This project will run into 3 states:

first state:

After installing the db(ip2region.db) file, 

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

After installing the db(ip2region.db) file, 

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

The current region is not found, the query module has an exception, such as the db file is not installed.the current suffix domain name will be maintained.

