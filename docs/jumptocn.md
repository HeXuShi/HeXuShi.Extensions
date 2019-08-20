# HeXuShi.Extensions.JumpToCN
[nuget install](https://www.nuget.org/packages/HeXuShi.Extensions.JumpToCN/)

change xx.com or xx.anything to xx.cn,help you jump to cn(china) domain suffix.

No longer based on this project https://github.com/lionsoul2014/ip2region/

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