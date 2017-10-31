using System.ComponentModel;
using JWT.JWTBuilder.Enums;

namespace JWT.JWTBuilder.Helper
{
    public static class EnumHelper
    {
        public static string GetHeaderName(this HeaderNames value) => getValueOfDescription(value);
        public static string GetPublicClaimName(this PublicClaimsNames value) => getValueOfDescription(value);

        private static string getValueOfDescription(object value)
        {
            var info = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}