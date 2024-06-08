﻿using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Queue.Test
{
    public class TestBase
    {
        protected IConfiguration Configuration;
        protected string QueueConnectionString;

        [SetUp]
        public void Setup()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            QueueConnectionString = Configuration.GetConnectionString("MyConnectionString");
        }
    }
}