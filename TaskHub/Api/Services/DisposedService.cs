namespace Api.Services
{
    /// <summary>
    /// Интерфейс для получения InstanceId
    /// </summary>
    public interface IHasInstanceId
    {
        Guid InstanceId { get; }
    }

    /// <summary>
    /// Базовый класс, который логирует создание и удаление
    /// </summary>
    public abstract class DisposedService : IHasInstanceId, IDisposable
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
        protected string ServiceName => GetType().Name;

        protected DisposedService()
        {
            Console.WriteLine($"[CREATE] {ServiceName} - InstanceId: {InstanceId}");
        }

        public virtual void Dispose()
        {
            Console.WriteLine($"[DISPOSE] {ServiceName} - InstanceId: {InstanceId}");
        }
    }
}
