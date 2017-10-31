using System.Collections.Generic;
namespace JWT.JWTBuilder.Models
{
    public class JWTData
    {
        public JWTData() : this(Header: new Dictionary<string, string>(), PayLoad: new Dictionary<string, object>())
        {

        }

        public JWTData(Dictionary<string, string> Header, Dictionary<string, object> PayLoad)
        {
            this.Header = Header;
            this.PayLoad = PayLoad;
        }

        public Dictionary<string, string> Header { get; set; }
        public Dictionary<string, object> PayLoad { get; set; }

    }
}