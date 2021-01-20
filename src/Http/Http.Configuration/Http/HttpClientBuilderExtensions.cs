// ReSharper disable CheckNamespace

using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Http
{
    /// <summary>
    /// A set of extension methods for <see cref="IHttpClientBuilder" />.
    /// </summary>
    public static class HttpClientBuilderExtensions
    {
#if NET5_0 || NETSTANDARD
        /// <summary>
        /// Configures the primary HTTP message handler to validate SSL certificates using the specified <paramref name="callback"/>.
        /// </summary>
        /// <param name="builder">The instance of <see cref="IHttpClientBuilder" /> to extend.</param>
        /// <param name="callback">The callback to be used to validate SSL certificates.</param>
        /// <returns>The same instance of <see cref="IHttpClientBuilder" /> passed in <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> cannot be null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="callback"/> cannot be null.</exception>
        public static IHttpClientBuilder ConfigureSslCertificateValidation(this IHttpClientBuilder builder, Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> callback)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            _ = callback ?? throw new ArgumentNullException(nameof(callback));

            _ = builder.ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = callback,
                };

                return handler;
            });

            return builder;
        }

        /// <summary>
        /// Configures the primary HTTP message handler to always accept incoming SSL certificates.
        /// </summary>
        /// <param name="builder">The instance of <see cref="IHttpClientBuilder" /> to extend.</param>
        /// <returns>The same instance of <see cref="IHttpClientBuilder" /> passed in <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> cannot be null.</exception>
        public static IHttpClientBuilder DisableSslCertificateValidation(this IHttpClientBuilder builder) => ConfigureSslCertificateValidation(builder, (_, _, _, _) => true);
#endif
    }
}
