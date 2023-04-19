using _1151.Cross.Util.Outputs;
using Microsoft.Extensions.DependencyInjection;

namespace _1151.Cross.DepedencyInjection
{
    public class CrossDI
    {
        private static IServiceProvider? _serviceProvider;

        public static T? GetValue<T>() where T : class
        {
            return _serviceProvider?.GetService<T>();
        }

        public static IEnumerable<T>? GetValues<T>() where T : class
        {
            return _serviceProvider?.GetServices<T>();
        }

        public static void Initialize()
        {
            Build();
        }

        private static void Build()
        {
            var services = new ServiceCollection();

            services.AddTransient(typeof(IOutput), typeof(ConsoleOutput));
            services.AddTransient(typeof(IOutput), typeof(DebugOutput));

            _serviceProvider = services.BuildServiceProvider();
        }

    }
}
