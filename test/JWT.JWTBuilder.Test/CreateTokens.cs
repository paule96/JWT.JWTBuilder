using System;
using JWT.Algorithms;
using JWT.JWTBuilder.Enums;
using JWT.Serializers;
using Xunit;

namespace JWT.JWTBuilder.Test
{
    public class CreateTokens
    {
        [Fact]
        public void CreateToken()
        {
            var token = new Builder()
                .AddAlgorithm(new HMACSHA256Algorithm())
                .AddSerializer(new JsonNetSerializer())
                .AddUrlEncoder(new JwtBase64UrlEncoder())
                .AddSecret("gsdhjfkhdfjklhjklgfsdhgfbsdgfvsdvfghjdjfgb")
                .Build();
            Assert.True(token.Length > 0 && token.Split('.').Length == 3);
        }

        [Fact]
        public void CreateTokenWithPayload()
        {
            var token = new Builder()
                .AddAlgorithm(new HMACSHA256Algorithm())
                .AddSerializer(new JsonNetSerializer())
                .AddUrlEncoder(new JwtBase64UrlEncoder())
                .AddSecret("gsdhjfkhdfjklhjklgfsdhgfbsdgfvsdvfghjdjfgb")
                .AddClaim(PublicClaimsNames.ExpirationTime, DateTime.UtcNow.AddHours(5).ToString())
                .Build();
            Assert.True(token.Length > 0 && token.Split('.').Length == 3);
        }
    }
}