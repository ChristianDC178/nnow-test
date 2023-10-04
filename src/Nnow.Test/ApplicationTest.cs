using Nnow.Api.Controllers;
using Nnow.Application;
using Nnow.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NuGet.Frameworks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Elastic.Clients.Elasticsearch.Core.Reindex;

namespace Nnow.Test;

public class Tests
{

    HostApplicationBuilder builder;
    CancellationTokenSource source = new CancellationTokenSource();
    CancellationToken token;

    [SetUp]
    public void Setup()
    {
        builder = new HostApplicationBuilder();

        builder.Services.AddApplicationDependencies();

        builder.Services.Configure<Nnow.Settings.NnowAppKeys>(
                builder.Configuration.GetSection("NnowAppKeys"));

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        token = source.Token;
    }

    [Test]
    public void Can_GetPermission()
    {

        IHost host = builder.Build();

        IMediator mediator = host.Services.GetRequiredService<IMediator>();

        var result = mediator.Send(new GetPermissionCommand() { PermissionId = 3 }, token).Result;

        Assert.IsTrue(result != null);
    }

    [Test]
    public void Can_ModifyPermission()
    {
        IHost host = builder.Build();

        IMediator mediator = host.Services.GetRequiredService<IMediator>();

        var result = mediator.Send(
            new ModifyPermissionCommand()
            {
                Forename = "Bill",
                Surname = "Gates",
                PermissionId = 1,
                PermissionTypeId = 2,
            }
            , token).Result;

        Assert.IsTrue(result != null);
        Assert.IsTrue(result.Valid);

    }


    [Test]
    public void Can_RequestPermission()
    {
        IHost host = builder.Build();

        IMediator mediator = host.Services.GetRequiredService<IMediator>();

        var result = mediator.Send(
            new RequestPermissionCommand()
            {
                Forename = "Steve",
                Surname = "Jobs",
                PermissionTypeId = 5,
            }
            , token).Result;

        Assert.IsTrue(result != null);
        Assert.IsTrue(result.Valid);
    }


    //Example of a test with thread cancellation
    [Test]
    public void Can_Cancell_RequestPermission()
    {
        try
        {

            source.Cancel();

            IHost host = builder.Build();
            IMediator mediator = host.Services.GetRequiredService<IMediator>();

            var result = mediator.Send(
                new RequestPermissionCommand()
                {
                    Forename = "Richard",
                    Surname = "Stallman",
                    PermissionTypeId = 3
                }
                , token).Result;

            Assert.Fail();
        }
        catch (Exception ex)
        {
            Assert.IsTrue(ex is AggregateException);

                var inner = ((AggregateException)ex).InnerExceptions[0];

            Assert.IsTrue(inner is TaskCanceledException);
        }
    }


}