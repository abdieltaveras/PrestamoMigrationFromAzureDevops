using System;

namespace apiBonosElectronicos.Authorization.Attributtes
{
	[AttributeUsage(AttributeTargets.Method)]
	public class AllowAnonymousAttribute : Attribute
	{
	}
}
