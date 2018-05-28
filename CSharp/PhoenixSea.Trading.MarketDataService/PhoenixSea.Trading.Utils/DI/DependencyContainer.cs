using System;
using Unity;
using Unity.Lifetime;
using Unity.Registration;

namespace PhoenixSea.Trading.Utils.DI
{
    public static class DependencyContainer
    {
        private static readonly Lazy<UnityContainer> Container = new Lazy<UnityContainer>(() => new UnityContainer());

        public static UnityContainer Instance => Container.Value;

        public static void Register(Type from, Type to, LifetimeManager lifetimeManager)
        {
            Instance.RegisterType(from, to, lifetimeManager);
        }

        public static void Register<TInterface, TClass>(LifetimeManager lifetimeManager, params InjectionMember[] members) where TClass : TInterface
        {
            Instance.RegisterType<TInterface, TClass>(lifetimeManager, members);
        }
        public static void Register<TInterface, TClass>(string name, LifetimeManager lifetimeManager, params InjectionMember[] members) where TClass : TInterface
        {
            Instance.RegisterType<TInterface, TClass>(name, lifetimeManager, members);
        }

        public static void Register<TClass>(LifetimeManager lifetimeManager, params InjectionMember[] members) where TClass : class
        {
            Instance.RegisterType<TClass>(lifetimeManager, members);
        }

        public static void RegisterInstance(Type type, object instance, LifetimeManager lifetimeManager)
        {
            Instance.RegisterInstance(type, instance, lifetimeManager);
        }

        public static void RegisterInstance<T>(string name, object instance, LifetimeManager lifetimeManager)
        {
            Instance.RegisterInstance(typeof(T), name, instance, lifetimeManager);
        }

        public static void RegisterInstance<T>(object instance, LifetimeManager lifetimeManager) where T : class
        {
            Instance.RegisterInstance(typeof(T), instance, lifetimeManager);
        }

        public static T Resolve<T>()
        {
            return Instance.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            return Instance.Resolve<T>(name);
        }

        public static T Resolve<T>(Type type) where T : class
        {
            return Instance.Resolve(type) as T;
        }

        public static T Resolve<T>(Type type, string name) where T : class
        {
            return Instance.Resolve(type, name) as T;
        }
    }
}