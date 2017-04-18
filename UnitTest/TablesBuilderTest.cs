﻿using BenzeneSoft.QBuild;
using BenzeneSoft.QBuild.Builders;
using NUnit.Framework;
using UnitTest.Entities;

namespace UnitTest
{
    [TestFixture]
    public class TablesBuilderTest
    {
        [Test]
        public void FromString()
        {
            var fromBuilder = new TablesBuilder<Product>(new LowerSnakeCaseNameResolver());
            
            Assert.AreEqual("product_table", fromBuilder.Table("product_table").Build().SqlText);
        }
    }
}