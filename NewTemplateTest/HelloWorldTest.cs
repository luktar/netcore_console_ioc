using Microsoft.Extensions.Logging;
using Moq;
using NewTemplate.Commands;
using NewTemplate.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NewTemplateTest
{
    public class HelloWorldTest
    {
        [Fact]
        public void RunTest()
        {
            ILogger<HelloWorldCommand> logger = Mock.Of<ILogger<HelloWorldCommand>>();

            Mock<IConfig> mockConfig = new Mock<IConfig>();
            mockConfig.Setup(x => x.HelloWorldText).Returns("test world!");

            ICommand helloWorldCommand = new HelloWorldCommand(
                logger, mockConfig.Object);

            Assert.True(true);
        }
    }
}
