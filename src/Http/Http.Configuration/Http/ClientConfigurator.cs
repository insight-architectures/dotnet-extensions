using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace InsightArchitectures.Extensions.Http
{
    /// <summary>
    /// An abstract class that can be used to collect changes to configurations of <see cref="IServiceCollection" /> and <see cref="IHttpClientBuilder" />.
    /// </summary>
    public abstract class ClientConfigurator
    {
        /// <summary>
        /// A collection of actions to be applied on an instance of <see cref="IHttpClientBuilder" />.
        /// </summary>
        protected IList<Action<IHttpClientBuilder>> HttpClientBuilderConfigurations { get; } = new List<Action<IHttpClientBuilder>>();

        /// <summary>
        /// A collection of actions to be applied on an instance of <see cref="IServiceCollection" />.
        /// </summary>
        protected IList<Action<IServiceCollection>> ServiceCollectionConfigurations { get; } = new List<Action<IServiceCollection>>();

        /// <summary>
        /// Adds the <paramref name="configuration"/> to <see cref="HttpClientBuilderConfigurations" />.
        /// </summary>
        /// <param name="configuration">The configuration to be added.</param>
        public void AddHttpClientBuilderConfiguration(Action<IHttpClientBuilder> configuration)
        {
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));

            HttpClientBuilderConfigurations.Add(configuration);
        }

        /// <summary>
        /// Adds the <paramref name="configuration"/> to <see cref="ServiceCollectionConfigurations" />.
        /// </summary>
        /// <param name="configuration">The configuration to be added.</param>
        public void AddServiceConfiguration(Action<IServiceCollection> configuration)
        {
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));

            ServiceCollectionConfigurations.Add(configuration);
        }

        /// <summary>
        /// Applies all the configuration in <see cref="HttpClientBuilderConfigurations" /> to the given <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">The instance of <see cref="IHttpClientBuilder" /> where the configurations are applied.</param>
        public void ApplyHttpClientBuilderConfigurations(IHttpClientBuilder builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            foreach (var action in HttpClientBuilderConfigurations)
            {
                action(builder);
            }
        }

        /// <summary>
        /// Applies all the configuration in <see cref="ServiceCollectionConfigurations" /> to the given <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The instance of <see cref="IServiceCollection" /> where the configurations are applied.</param>
        public void ApplyServiceConfigurations(IServiceCollection services)
        {
            _ = services ?? throw new ArgumentNullException(nameof(services));

            foreach (var action in ServiceCollectionConfigurations)
            {
                action(services);
            }
        }
    }
}
