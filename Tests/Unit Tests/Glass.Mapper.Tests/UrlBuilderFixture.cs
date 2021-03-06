﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Glass.Mapper.Tests
{
    [TestFixture]
    public class UrlBuilderFixture
    {
        [Test]
        [TestCase("http://glass.co/test/somevalue", "http://glass.co/test/somevalue")]
        [TestCase("http://glass.co/test/somevalue?test=value&test1=another", "http://glass.co/test/somevalue?test=value&test1=another")]
        [TestCase("http://glass.co/test/somevalue?test=value&test1=another&test=value3", "http://glass.co/test/somevalue?test=value&test1=another&test=value3")]
        [TestCase("http://glass.co/test/somevalue?test=value&test1=another&test=value3&", "http://glass.co/test/somevalue?test=value&test1=another&test=value3")]
        [TestCase(null, "")]
        [TestCase("http://glass.co/test/somevalue?test=value&test1=", "http://glass.co/test/somevalue?test=value&test1=")]
        [TestCase("http://glass.co/test/somevalue?test=value&=another&test=value3", "http://glass.co/test/somevalue?test=value&test=value3")]
        //tests for issue 222
        [TestCase("http://glass.co/test/somevalue?test%3dvalue&%3danother&test=value3", "http://glass.co/test/somevalue?test=value&test=value3")]
        [TestCase("http://glass.co/test/somevalue?test%3Dvalue&test1%3D", "http://glass.co/test/somevalue?test=value&test1=")]
        public void UrlBuilder_TestUrlShouldRemainTheSame_NoAdditionalQueryParameters(string url, string expected)
        {
            //Arrange
            
            //Act
            var builder = new UrlBuilder(url);

            //Assert
            Assert.AreEqual(expected, builder.ToString());
        }

        [Test]
        [TestCase("http://glass.co/test/somevalue", "http://glass.co/test/somevalue?k1=v1&v2=k2", "k1=v1&v2=k2")]
        [TestCase("http://glass.co/test/somevalue?test=value&test1=another", "http://glass.co/test/somevalue?test=value&test1=another&k1=v1&v2=k2", "?k1=v1&v2=k2")]
        [TestCase("http://glass.co/test/somevalue?test=value&test1=another&test=value3", "http://glass.co/test/somevalue?test=value&test1=another&test=value3&k1=v1&v2=k2", "?k1=v1&v2=k2")]
        [TestCase("http://glass.co/test/somevalue?test=value&test1=another&test=value3&", "http://glass.co/test/somevalue?test=value&test1=another&test=value3&k1=v1&v2=k2", "?k1=v1&v2=k2")]
        [TestCase(null, "?k1=v1&v2=k2", "?k1=v1&v2=k2")]
        [TestCase("http://glass.co/test/somevalue?test=value&test1=", "http://glass.co/test/somevalue?test=value&test1=&k1=v1&v2=k2", "?k1=v1&v2=k2")]
        [TestCase("http://glass.co/test/somevalue?test=value&=another&test=value3", "http://glass.co/test/somevalue?test=value&test=value3&k1=v1&v2=k2", "?k1=v1&v2=k2")]
        public void AddToQueryString_AddsCompleteString_ReturnsValueUrl(string url, string expected, string query)
        {
            //Arrange
            var builder = new UrlBuilder(url);

            //Act
            builder.AddToQueryString(query);

            //Assert
            Assert.AreEqual(expected, builder.ToString());
        }


    }
}
