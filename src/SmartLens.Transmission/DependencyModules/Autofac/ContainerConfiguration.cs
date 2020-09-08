﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartLens.Business.Abstract;
using SmartLens.Business.Concrete;
using SmartLens.Listener.Abstract;
using SmartLens.Listener.Concrate;
using SmartLens.Transmission.Abstract;
using SmartLens.Transmission.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLens.Transmission.DependencyModules.Autofac
{
    static class ContainerConfiguration
    {
        public static IServiceProvider Configure()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(l => l.AddConsole())
                .Configure<LoggerFilterOptions>(c => c.MinLevel = LogLevel.Trace);
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(serviceCollection);

            containerBuilder.RegisterType<ImageDetectedManager>().As<IImageDetectedManager>().SingleInstance();

            containerBuilder.RegisterType<Server>().As<IServer>().SingleInstance();
            containerBuilder.RegisterType<ResponseClient>().As<IResponseClient>().SingleInstance();
            containerBuilder.RegisterType<AsyncUdpListener>().As<IListener>().SingleInstance();

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
