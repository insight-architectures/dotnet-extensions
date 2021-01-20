using AutoFixture.Idioms;
using Microsoft.Extensions.Http;
using NUnit.Framework;

namespace Tests.Http
{
    [TestFixture]
    public class HttpClientBuilderExtensionsTests
    {
#if NET5_0 || NETCOREAPP
        [Test, CustomAutoData]
        public void ConfigureSslCertificateValidation_does_not_accept_null_parameters(GuardClauseAssertion assertion) => assertion.Verify(typeof(HttpClientBuilderExtensions).GetMethod(nameof(HttpClientBuilderExtensions.ConfigureSslCertificateValidation)));

        [Test, CustomAutoData]
        public void DisableSslCertificateValidation_does_not_accept_null_parameters(GuardClauseAssertion assertion) => assertion.Verify(typeof(HttpClientBuilderExtensions).GetMethod(nameof(HttpClientBuilderExtensions.DisableSslCertificateValidation)));
#endif
    }
}
