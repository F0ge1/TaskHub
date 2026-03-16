namespace Api.Services
{
    // Singleton сервисы
    public interface ISingletonService1 : IHasInstanceId { }
    public interface ISingletonService2 : IHasInstanceId { }

    public class SingletonService1 : DisposedService, ISingletonService1 { }
    public class SingletonService2 : DisposedService, ISingletonService2 { }

    // Scoped сервисы
    public interface IScopedService1 : IHasInstanceId { }
    public interface IScopedService2 : IHasInstanceId { }

    public class ScopedService1 : DisposedService, IScopedService1 { }
    public class ScopedService2 : DisposedService, IScopedService2 { }

    // Transient сервисы
    public interface ITransientService1 : IHasInstanceId { }
    public interface ITransientService2 : IHasInstanceId { }

    public class TransientService1 : DisposedService, ITransientService1 { }
    public class TransientService2 : DisposedService, ITransientService2 { }
}
