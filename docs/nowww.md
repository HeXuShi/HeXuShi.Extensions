# HeXuShi.Extensions.NoWWW
[nuget](https://www.nuget.org/packages/HeXuShi.Extensions.NoWWW/)
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

# HeXuShi.Extensions.NoWWW preview
> warning, this is preview
how install dependencies?
[Http.Abstractions](https://dotnet.myget.org/feed/aspnetcore-dev/package/nuget/Microsoft.AspNetCore.Http.Abstractions)
[Http.Extensions](https://dotnet.myget.org/feed/aspnetcore-dev/package/nuget/Microsoft.AspNetCore.Http.Extensions)
[Mvc.Abstractions](https://dotnet.myget.org/feed/aspnetcore-dev/package/nuget/Microsoft.AspNetCore.Mvc.Abstractions)

```
PM> Install-Package Microsoft.AspNetCore.Mvc.Abstractions -Version 3.0.0-preview4-19123-01 -Source https://dotnet.myget.org/F/aspnetcore-dev/api/v3/index.json
```