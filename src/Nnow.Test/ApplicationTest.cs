using Nnow.Api.Controllers;
using Nnow.Application;
using Nnow.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NuGet.Frameworks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace Nnow.Test;

public class Tests
{

    HostApplicationBuilder builder;

    [SetUp]
    public void Setup()
    {
        builder = new HostApplicationBuilder();

        builder.Services.AddApplicationDependencies();

        builder.Services.Configure<Nnow.Settings.NnowAppKeys>(
                builder.Configuration.GetSection("NnowAppKeys"));

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
    }

    [Test]
    public void Can_GetPermission()
    {

        IHost host = builder.Build();

        IMediator mediator = host.Services.GetRequiredService<IMediator>();

        CancellationToken cancellationToken = new CancellationToken();
        var result = mediator.Send(new GetPermissionCommand() { PermissionId = 3 }, cancellationToken).Result;

        Assert.IsTrue(result != null);
    }

    [Test]
    public void Can_ModifyPermission()
    {
        IHost host = builder.Build();

        IMediator mediator = host.Services.GetRequiredService<IMediator>();

        CancellationToken cancellationToken = new CancellationToken();

        var result = mediator.Send(
            new ModifyPermissionCommand()
            {
                Forename = "Bill",
                Surname = "Gates",
                PermissionId = 1,
                PermissionTypeId = 2,
            }
            , cancellationToken).Result;

        Assert.IsTrue(result != null);
        Assert.IsTrue(result.Valid);

    }


    [Test]
    public void Can_RequestPermission()
    {
        IHost host = builder.Build();

        IMediator mediator = host.Services.GetRequiredService<IMediator>();

        CancellationToken cancellationToken = new CancellationToken();

        var result = mediator.Send(
            new RequestPermissionCommand()
            {
                Forename = "Steve",
                Surname = "Jobs",
                PermissionTypeId = 5,
            }
            , cancellationToken).Result;
        
        
        Assert.IsTrue(result != null);
        Assert.IsTrue(result.Valid);
    }
}