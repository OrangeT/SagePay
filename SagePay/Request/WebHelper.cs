using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace OrangeTentacle.SagePay.Request
{
    internal class WebHelper
    {
        internal static T EnumFromString<T>(string value)
        {
            value = value.Replace(" ", "");
            return (T)Enum.Parse(typeof(T), value, true);
        }

        internal static string SendRequest(string url, NameValueCollection collection)
        {
            WebClient client = new WebClient();
            var response = client.UploadValues(url, collection);
            var textResponse = System.Text.Encoding.UTF8.GetString(response);
            return textResponse;
        }

        internal static Dictionary<string, string> SplitResponse(string response)
        {
            var lines = response.Split('\n');
            var collection = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                    continue;

                var values = line.Split('=');
                collection.Add(values[0].Trim(), values[1].Trim());
            }

            return collection;
        }
    }

}
