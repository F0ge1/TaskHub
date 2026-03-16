namespace Api.Services
{
    public static class ServiceProviderExtensions
    {
        public static void CompareInstances<TService>(this IServiceProvider serviceProvider, string scopeName = "Root")
        where TService : IHasInstanceId
        {
            Console.WriteLine($"\n--- Comparison {typeof(TService).Name} in {scopeName} ---");

            // Резолвим два экземпляра
            var first = serviceProvider.GetService<TService>();
            var second = serviceProvider.GetService<TService>();

            if (first == null || second == null)
            {
                Console.WriteLine("Service has not been registered!");
                return;
            }

            // Выводим InstanceId
            Console.WriteLine($"First:  {first.InstanceId}");
            Console.WriteLine($"Second: {second.InstanceId}");

            // Проверяем, один и тот же это объект или нет
            var areSame = ReferenceEquals(first, second);
            Console.WriteLine($"The same object: {areSame}");

            if (areSame)
            {
                Console.WriteLine("✓ It's Singleton or Scoped in one scope");
            }
            else
            {
                Console.WriteLine("✗ It's Transient or Scoped in different scope");
            }
        }
    }
}
