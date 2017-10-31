using System;
using JWT.JWTBuilder.Enums;
using JWT.JWTBuilder.Helper;
using JWT.JWTBuilder.Models;
using JWT.Serializers;

namespace JWT.JWTBuilder
{
    public class Builder
    {
        private JWTData jwt = new JWTData();
        private IJsonSerializer serializer = new JsonNetSerializer();
        private IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        private IJwtAlgorithm algorithm;
        private IDateTimeProvider utcProvieder = new UtcDateTimeProvider();
        private IJwtValidator validator;
        private bool verify = false;
        private string secret = "";



        public Builder AddHeader(HeaderNames name, string value)
        {
            this.jwt.Header.Add(name.GetHeaderName(), value);
            return this;
        }

        public Builder AddClaim(string name, object value)
        {
            this.jwt.PayLoad.Add(name, value);
            return this;
        }
        public Builder AddClaim(string name, string value) => this.AddClaim(name, (object)value);
        public Builder AddClaim(PublicClaimsNames names, string value) => this.AddClaim(names.GetPublicClaimName(), value);

        public Builder AddSerializer(IJsonSerializer serializer)
        {
            this.serializer = serializer;
            return this;
        }

        public Builder AddTimeProvider(IDateTimeProvider provider)
        {
            this.utcProvieder = provider;
            return this;
        }

        public Builder AddValidator(IJwtValidator validator)
        {
            this.validator = validator;
            return this;
        }

        public Builder AddUrlEncoder(IBase64UrlEncoder urlEncoder)
        {
            this.urlEncoder = urlEncoder;
            return this;
        }

        public Builder AddAlgorithm(IJwtAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            return this;
        }

        public Builder AddSecret(string secret)
        {
            this.secret = secret;
            return this;
        }

        public Builder MustVerify()
        {
            this.verify = true;
            return this;
        }

        public Builder NotVerify()
        {
            this.verify = false;
            return this;
        }

        public string Build()
        {
            if (!canWeBuild())
            {
                throw new Exception("Can't build a Token. Check if you have call all of this: \n\r" +
                "- AddAlgorithm \n\r" +
                "- AddSerializer \n\r" +
                "- AddUrlEncoder \n\r" +
                "- AddSecret \n\r");
            }
            var encoder = new JwtEncoder(this.algorithm, this.serializer, this.urlEncoder);
            return encoder.Encode(this.jwt.PayLoad, secret);
        }

        public string Decode(string token)
        {
            this.tryToCreateAValidator();
            if (!canWeDecode())
            {
                throw new Exception("Can't build a Token. Check if you have call all of this: \n\r" +
                "- AddSerializer \n\r" +
                "- AddUrlEncoder \n\r" +
                "- AddTimeProvider \n\r" +
                "- AddValidator \n\r" +
                "If you called MustVerify you must also call AddSecret"
                );

            }
            var decoder = new JwtDecoder(this.serializer, this.validator, this.urlEncoder);
            return decoder.Decode(token, this.secret, this.verify);
        }

        public T Decode<T>(string token)
        {
            this.tryToCreateAValidator();
            if (!canWeDecode())
            {
                throw new Exception("Can't build a Token. Check if you have call all of this: \n\r" +
                "- AddSerializer \n\r" +
                "- AddUrlEncoder \n\r" +
                "- AddTimeProvider \n\r" +
                "- AddValidator \n\r" +
                "If you called MustVerify you must also call AddSecret"
                );

            }
            var decoder = new JwtDecoder(this.serializer, this.validator, this.urlEncoder);
            return decoder.DecodeToObject<T>(token, this.secret, this.verify);
        }

        private void tryToCreateAValidator()
        {
            if (this.serializer == null || this.utcProvieder == null)
            {
                throw new Exception("Can't create a Validator. Please call AddSerializer and AddTimeProvider");
            }
            if (this.validator == null)
            {
                this.validator = new JwtValidator(this.serializer, this.utcProvieder);
            }
        }

        private bool canWeBuild()
        {
            if (
                this.algorithm != null &&
                this.serializer != null &&
                this.urlEncoder != null &&
                this.jwt.PayLoad != null &&
                this.secret != null &&
                this.secret.Length > 0)
            {
                return true;
            }
            return false;
        }

        private bool canWeDecode()
        {
            if (this.serializer != null &&
            this.utcProvieder != null &&
            this.validator != null &&
            this.urlEncoder != null
            )
            {
                if (this.verify && this.secret == null && this.secret.Length > 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

    }
}
