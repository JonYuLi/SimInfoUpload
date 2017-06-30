using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SimInfoUpload
{
    public class HttpUtility
    {
        public SimInfo getNumFormServer(string num)
        {
            string ret = HttpGet("http://120.132.12.76:8080/i501-hg/sim/search/findByNumber", "number=" + num);
            return JsonToObject(ret);
        }

        public SimInfo getSimInfoByCCID(string ccid)
        {
            string ret = HttpGet("http://120.132.12.76:8080/i501-hg/sim/search/findByCcid", "ccid=" + ccid);
            return JsonToObject(ret);
        }

        public static string HttpGet(string Url, string postDataStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";

                request.Timeout = 1000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Request Error: " + e.ToString());
                return null;
            }
        }

        private SimInfo JsonToObject(string jsonStr)
        {
            if (jsonStr == null)
            {
                return null;
            }
            RootObject rootObject = (RootObject)JsonConvert.DeserializeObject(jsonStr, typeof(RootObject));
            if (rootObject._embedded.sim.Count <= 0)
            {
                return null;
            }
            SimInfo simInfo = new SimInfo
            {
                num = rootObject._embedded.sim[0].number,
                ccid = rootObject._embedded.sim[0].ccid,
                description = rootObject._embedded.sim[0].description
            };
            return simInfo;
        }

        public bool HttpPost(SimInfo simInfo)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://120.132.12.76:8080/i501-hg/sim");
                request.Method = "POST";
                request.ContentType = "application/json";
                string postStr = saveLoad(simInfo);
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.UTF8);
                writer.Write(postStr);
                writer.Flush();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string retString = reader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine("post return :  " + retString);
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return false;
            }
        }

        public string saveLoad(SimInfo simInfo)
        {
            StringWriter sw = new StringWriter();
            JsonWriter writer = new JsonTextWriter(sw);
            writer.WriteStartObject();
            writer.WritePropertyName("number");
            writer.WriteValue(simInfo.num);
            writer.WritePropertyName("ccid");
            writer.WriteValue(simInfo.ccid);
            writer.WritePropertyName("description");
            writer.WriteValue(simInfo.description);
            writer.WriteEndObject();
            writer.Flush();
            string jsonText = sw.GetStringBuilder().ToString();
            return jsonText;
        }
    }
}
