using System;
using Microsoft.Practices.Unity;

namespace OrderManagementEngine.Core.Utils
{
    /// <summary>
    /// Singleton class for the dependency injection pattern, Unity (3rd party lib)
    /// is used to manage the dependencies
    /// </summary>
    public sealed class DIContainer
    {
        private static readonly Lazy<UnityContainer> Lazy =
            new Lazy<UnityContainer>(() => new UnityContainer());

        public static UnityContainer Instance => Lazy.Value;

        public static T Resolve<T>()
        {
            return Instance.Resolve<T>();
        }

        public static T ResolveByName<T>(string name)
        {
            return Instance.Resolve<T>(name);
        }
    }
}