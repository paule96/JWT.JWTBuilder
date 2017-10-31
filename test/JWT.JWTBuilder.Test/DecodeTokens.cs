using System.Collections.Generic;
using JWT.Serializers;
using Xunit;

namespace JWT.JWTBuilder.Test
{
    public class DecodeTokens
    {
        [Fact]
        public void DecodeToken()
        {
            var payload = new Builder()
                .AddSerializer(new JsonNetSerializer())
                .AddUrlEncoder(new JwtBase64UrlEncoder())
                .AddTimeProvider(new UtcDateTimeProvider())
                .Decode("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJjbGFpbTEiOjAsImNsYWltMiI6ImNsYWltMi12YWx1ZSJ9.8pwBI_HtXqI3UgQHQ_rDRnSQRxFL1SR8fbQoS-5kM5s");

            Assert.True(payload.Length > 0);
        }

        [Fact]
        public void CreateTokenWithVerifyCheck()
        {
            var payload = new Builder()
                .AddSerializer(new JsonNetSerializer())
                .AddUrlEncoder(new JwtBase64UrlEncoder())
                .AddSecret("GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk")
                .MustVerify()
                .Decode("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJjbGFpbTEiOjAsImNsYWltMiI6ImNsYWltMi12YWx1ZSJ9.8pwBI_HtXqI3UgQHQ_rDRnSQRxFL1SR8fbQoS-5kM5s");
            Assert.True(payload.Length > 0);
        }

        [Fact]
        public void DecodeTokenToADictionary()
        {
            var payload = new Builder()
                .AddSerializer(new JsonNetSerializer())
                .AddUrlEncoder(new JwtBase64UrlEncoder())
                .AddSecret("GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk")
                .MustVerify()
                .Decode<Dictionary<string, string>>("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJjbGFpbTEiOjAsImNsYWltMiI6ImNsYWltMi12YWx1ZSJ9.8pwBI_HtXqI3UgQHQ_rDRnSQRxFL1SR8fbQoS-5kM5s");
            Assert.True(payload.Count == 2 && payload["claim1"] == 0.ToString());
        }
    }
}