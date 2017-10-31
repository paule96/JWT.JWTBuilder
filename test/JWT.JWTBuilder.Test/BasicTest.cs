using System;
using System.Collections.Generic;
using JWT.JWTBuilder.Models;
using Xunit;

namespace JWT.JWTBuilder.Test
{
    public class BasicTest
    {

        [Fact]
        public void CanCreateANewJWTDataWithoutParams()
        {
            try
            {
                var jwtData = new JWTData();
                jwtData.Header.Add("test", "test");
                jwtData.PayLoad.Add("payload01", "payload02");
            }
            catch (System.Exception)
            {
                Assert.True(false);
                throw;
            }
            Assert.True(true);
        }

        [Fact]
        public void CanCreateANewJWTDataWithParams()
        {
            try
            {
                var headers = new Dictionary<string, string>();
                headers.Add("test", "test");
                var payload = new Dictionary<string, object>();
                payload.Add("test", "payload");
                var jwtData = new JWTData(headers, payload);
                Assert.True(jwtData.Header["test"] == "test");
                Assert.True(jwtData.PayLoad["test"].ToString() == "payload");
                jwtData.PayLoad.Add("payload01", "payload02");
            }
            catch (System.Exception)
            {
                Assert.True(false);
                throw;
            }
        }

        [Fact]
        public void CanCreateANewBuilder()
        {
            try
            {
                var builder = new Builder();
            }
            catch (System.Exception)
            {
                Assert.True(false);
                throw;
            }
            Assert.True(true);
        }
    }
}
