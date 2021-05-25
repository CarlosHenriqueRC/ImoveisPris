using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ImoveisPris.Web.Client
{
    public class Util
    {
        public static T DeserializeJsonFromStream<T>(System.IO.Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        public static string DeserializeStringFromStream(System.IO.Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return "";

            using (var sr = new StreamReader(stream))
            {
                return  sr.ReadToEnd();
            }
        }

    }
}
