# HeXuShi.Extensions.JumpToCN
[nuget install](https://www.nuget.org/packages/HeXuShi.Extensions.JumpToCN/)

change xx.com or xx.anything to xx.cn,help you jump to cn(china) domain suffix.

https://github.com/lionsoul2014/ip2region/ Based on this open source project

Before you start everything, please create db folder in your web project wwwroot folder.

and download https://github.com/lionsoul2014/ip2region/blob/master/data/ip2region.db, to you web project wwwroot/db/ip2region.db

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
        app.JumpToCN(".com"); // or app.JumpToCN(".anything");
        ...
    }
}
```

When you use China mainland ip access, you will be redirected to the .cn suffix domain name. If it is a non-Chinese mainland ip access, use no .com (or .anything) suffix domain name to access, it will jump to the specified .com (or .anything) suffix domain name.

third state:

The current region is not found, the query module has an exception, such as the db file is not installed.the current suffix domain name will be maintained.
