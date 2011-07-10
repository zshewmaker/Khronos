﻿using System;
using System.Collections.Generic;
using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;

namespace Khronos.UnitTests
{
public class CastleServiceProvider : IServiceLocator
{
	private readonly IWindsorContainer container;

	public CastleServiceProvider(IWindsorContainer container)
	{
		this.container = container;
	}

	public object GetService(Type serviceType)
	{
		return container.Resolve(serviceType);
	}

	public object GetInstance(Type serviceType)
	{
		return container.Resolve(serviceType);
	}

	public object GetInstance(Type serviceType, string key)
	{
		return container.Resolve(key, serviceType);
	}

	public IEnumerable<object> GetAllInstances(Type serviceType)
	{
		return (object[])container.ResolveAll(serviceType);
	}

	public TService GetInstance<TService>()
	{
		return container.Resolve<TService>();
	}

	public TService GetInstance<TService>(string key)
	{
		return container.Resolve<TService>(key);
	}

	public IEnumerable<TService> GetAllInstances<TService>()
	{
		return container.ResolveAll<TService>();
	}
}
}