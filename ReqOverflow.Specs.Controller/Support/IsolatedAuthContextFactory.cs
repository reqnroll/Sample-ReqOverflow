using System;
using System.Collections.Generic;
using BoDi;
using ReqOverflow.Specs.Support;
using Reqnroll.Infrastructure;

namespace ReqOverflow.Specs.Controller.Support
{
    public class IsolatedAuthContextFactory
    {
        private readonly IObjectContainer _scenarioContainer;
        private readonly Dictionary<AuthContext, IObjectContainer> _isolatedContextContainer = new();
        
        public IsolatedAuthContextFactory(IObjectContainer scenarioContainer)
        {
            _scenarioContainer = scenarioContainer;
        }

        public TDriver CreateDriver<TDriver>(AuthContext authContext)
        {
            if (!_isolatedContextContainer.TryGetValue(authContext, out var container))
                throw new InvalidOperationException("No isolated context has created for this AuthContext!");

            return container.Resolve<TDriver>();
        }
        
        public AuthContext CreateAuthContext()
        {
            var container = CreateSpecFlowLikeContainer();
            // to provide some core dependencies, like TestLogger, we use the the one in
            // scenario container
            container.RegisterInstanceAs(_scenarioContainer.Resolve<TestLogger>());
            var authContext = container.Resolve<AuthContext>();
            _isolatedContextContainer.Add(authContext, container);
            return authContext;
        }

        private ObjectContainer CreateSpecFlowLikeContainer()
        {
            var container = new ObjectContainer();
            // simulate IContainerDependentObject support of SpecFlow containers
            container.ObjectCreated += obj =>
            {
                if (obj is IContainerDependentObject containerDependentObject)
                {
                    containerDependentObject.SetObjectContainer(container);
                }
            };
            return container;
        }
    }
}
