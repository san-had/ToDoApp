using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Ninject;
using NUnit.Framework;
using ToDo.Domain;
using ToDo.Extensibility.Dto;
using ToDo.Service;

namespace ToDo.Test
{
    public class TestBase : IDisposable
    {
        private bool disposed = false;

        protected StandardKernel kernel = new StandardKernel();

        protected Mock<IOptionsSnapshot<ConfigurationSettings>> optionsSnapShotMock;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            kernel.Load<ServiceNinjectModule>();
            kernel.Load<DomainNinjectModule>();

            optionsSnapShotMock = new Mock<IOptionsSnapshot<ConfigurationSettings>>();
            kernel.Bind<IOptionsSnapshot<ConfigurationSettings>>().ToConstant(optionsSnapShotMock.Object);
            optionsSnapShotMock.SetupGet(m => m.Value).Returns(new ConfigurationSettings { SqlLiteDbFilePath = ":memory:" });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                kernel.Dispose();
            }
            disposed = true;
        }
    }
}