using System;
using System.Collections.Generic;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using InsightArchitectures.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Tests.Http
{
    [TestFixture]
    public class ClientConfiguratorTests
    {
        [Test, CustomAutoData]
        public void Nulls_are_not_allowed(GuardClauseAssertion assertion) => assertion.Verify(typeof(TestClientConfigurator));

        [Test, CustomAutoData]
        public void ServiceCollection_action_is_added(TestClientConfigurator sut, Action<IServiceCollection> action)
        {
            sut.AddServiceConfiguration(action);
            
            Assert.That(sut.ServiceCollectionActions, Contains.Item(action));
        }

        [Test, CustomAutoData]
        public void HttpClientBuilder_action_is_added(TestClientConfigurator sut, Action<IHttpClientBuilder> action)
        {
            sut.AddHttpClientBuilderConfiguration(action);
            
            Assert.That(sut.HttpClientBuilderActions, Contains.Item(action));
        }

        [Test, CustomAutoData]
        public void ServiceCollection_action_is_added_and_applied(TestClientConfigurator sut, Action<IServiceCollection> action, IServiceCollection services)
        {
            sut.AddServiceConfiguration(action);

            sut.ApplyServiceConfigurations(services);
            
            Mock.Get(action).Verify(p => p(services));
        }

        [Test, CustomAutoData]
        public void HttpClientBuilder_action_is_added_and_applied(TestClientConfigurator sut, Action<IHttpClientBuilder> action, IHttpClientBuilder builder)
        {
            sut.AddHttpClientBuilderConfiguration(action);

            sut.ApplyHttpClientBuilderConfigurations(builder);

            Mock.Get(action).Verify(p => p(builder));
        }
    }

    public class TestClientConfigurator : ClientConfigurator
    {
        public IEnumerable<Action<IServiceCollection>> ServiceCollectionActions => ServiceCollectionConfigurations;

        public IEnumerable<Action<IHttpClientBuilder>> HttpClientBuilderActions => HttpClientBuilderConfigurations;
    }
}
