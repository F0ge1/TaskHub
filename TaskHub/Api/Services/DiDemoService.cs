namespace Api.Services
{
    public class DiDemoService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _appLifetime;

        public DiDemoService(IServiceProvider serviceProvider, IHostApplicationLifetime appLifetime)
        {
            _serviceProvider = serviceProvider;
            _appLifetime = appLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _ = Task.Run(async () => await DemonstrateDiLifetimesAsync());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private async Task DemonstrateDiLifetimesAsync()
        {
            // Ждем, пока приложение полностью запустится
            await Task.Delay(1000);

            Console.WriteLine("\n=========================================");
            Console.WriteLine("DEMONSTRATION OF DI'S LIFE CYCLES ");
            Console.WriteLine("=========================================\n");

            // Scope 1
            Console.WriteLine("\n=== SCOPE 1 ===");
            using (var scope1 = _serviceProvider.CreateScope())
            {
                Console.WriteLine("\n--- Singleton (Should be the same in all of the scopes) ---");
                scope1.ServiceProvider.CompareInstances<ISingletonService1>("Scope 1");
                scope1.ServiceProvider.CompareInstances<ISingletonService2>("Scope 1");

                Console.WriteLine("\n--- Scoped (Should be the same inside scope) ---");
                scope1.ServiceProvider.CompareInstances<IScopedService1>("Scope 1");
                scope1.ServiceProvider.CompareInstances<IScopedService2>("Scope 1");

                Console.WriteLine("\n--- Transient (Always different) ---");
                scope1.ServiceProvider.CompareInstances<ITransientService1>("Scope 1");
                scope1.ServiceProvider.CompareInstances<ITransientService2>("Scope 1");

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
            Console.WriteLine("Scope 1 finished - Scoped services should be disposed");

            // Scope 2
            Console.WriteLine("\n=== SCOPE 2 ===");
            using (var scope2 = _serviceProvider.CreateScope())
            {
                Console.WriteLine("\n--- Singleton (the same as in Scope 1) ---");
                scope2.ServiceProvider.CompareInstances<ISingletonService1>("Scope 2");
                scope2.ServiceProvider.CompareInstances<ISingletonService2>("Scope 2");

                Console.WriteLine("\n--- Scoped (new copies for the new scope) ---");
                scope2.ServiceProvider.CompareInstances<IScopedService1>("Scope 2");
                scope2.ServiceProvider.CompareInstances<IScopedService2>("Scope 2");

                Console.WriteLine("\n--- Transient (again, everything is new and different) ---");
                scope2.ServiceProvider.CompareInstances<ITransientService1>("Scope 2");
                scope2.ServiceProvider.CompareInstances<ITransientService2>("Scope 2");

                Console.WriteLine("\nPress Enter to finish...");
                Console.ReadLine();
            }
            Console.WriteLine("Scope 2 finished - Scoped services should be disposed");

            // Завершаем приложение
            _appLifetime.StopApplication();
        }
    }
}
