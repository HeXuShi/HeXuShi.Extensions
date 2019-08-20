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