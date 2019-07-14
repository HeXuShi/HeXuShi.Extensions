# HeXuShi.Extensions.NoWWW
https://www.nuget.org/packages/HeXuShi.Extensions.NoWWW/
change www.site.com to site.com in asp.net core web app.

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