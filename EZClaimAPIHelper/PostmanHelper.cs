using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EZClaimAPIHelper
{
    public class PostmanHelper
    {
        private static CollectionInfo GetCollectionInfo(string name)
        {
            return new CollectionInfo
            {
                PostmanId = Guid.NewGuid().ToString(),
                Name = name,
                Schema = "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
            };
        }

        private static List<Header> GetHeaders(HttpRequestMessage requestMessage)
        {
            List<Header> headers = new();

            foreach (var header in requestMessage.Headers)
            {
                headers.Add(new Header { Key = header.Key, Value = header.Value.First(), Type = "default" });
            }

            return headers;
        }

        public static void GeneratePostmanScript(string baseAddress, HttpRequestMessage requestMessage)
        {
            string name = requestMessage.RequestUri.OriginalString.Substring(1).Replace("/", "_");
            name = Regex.Replace(name, @"[^a-zA-Z0-9_]+", "_");

            PostmanCollection postmanCollection = new PostmanCollection
            {
                Info = GetCollectionInfo(name),
                Item = new List<Item>
                {
                new Item
                {
                    Name = $"{baseAddress}{requestMessage.RequestUri.OriginalString}",
                    Request = new Request
                    {
                        Method = requestMessage.Method.Method,
                        Header = GetHeaders(requestMessage),
                        Body = new Body
                        {
                            Mode = "raw",
                            Raw = requestMessage.Content.ReadAsStringAsync().Result,
                            Options = new Options
                            {
                                Raw = new RawOptions
                                {
                                    Language = "json"
                                }
                            }
                        },
                        Url = new Url
                        {
                            Raw = $"{baseAddress}{requestMessage.RequestUri.OriginalString}",
                            Protocol = baseAddress.Split("://")[0],
                            Host = baseAddress.Split("://")[1].Split(".").ToList(),
                            Path = requestMessage.RequestUri.OriginalString.Substring(1).Split("/").ToList()
                        }
                    },
                    Response = new List<object>()
                }
            }
            };

            // Convert to JSON
            string postmanJson = JsonConvert.SerializeObject(postmanCollection, Formatting.Indented);



            string folderName = "PostmanScripts";
            string fileName = $"{name}_{DateTime.Now.Ticks.ToString()}.json";
            string currentDirectory = Environment.CurrentDirectory;
            string folderPath = Path.Combine(currentDirectory, folderName);
            string filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllText(filePath, postmanJson);
        }

        class PostmanCollection
        {
            [JsonProperty("info")]
            public CollectionInfo Info { get; set; }
            [JsonProperty("item")]
            public List<Item> Item { get; set; }
        }

        class CollectionInfo
        {
            [JsonProperty("_postman_id")]
            public string PostmanId { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("schema")]
            public string Schema { get; set; }
        }

        class Item
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("request")]
            public Request Request { get; set; }
            [JsonProperty("response")]
            public List<object> Response { get; set; }
        }

        class Request
        {
            [JsonProperty("method")]
            public string Method { get; set; }
            [JsonProperty("header")]
            public List<Header> Header { get; set; }
            [JsonProperty("body")]
            public Body Body { get; set; }
            [JsonProperty("url")]
            public Url Url { get; set; }
        }

        class Header
        {
            [JsonProperty("key")]
            public string Key { get; set; }
            [JsonProperty("value")]
            public string Value { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
        }

        class Body
        {
            [JsonProperty("mode")]
            public string Mode { get; set; }
            [JsonProperty("raw")]
            public string Raw { get; set; }
            [JsonProperty("options")]
            public Options Options { get; set; }
        }

        class Options
        {
            [JsonProperty("raw")]
            public RawOptions Raw { get; set; }
        }

        class RawOptions
        {
            [JsonProperty("language")]
            public string Language { get; set; }
        }

        class Url
        {
            [JsonProperty("raw")]
            public string Raw { get; set; }
            [JsonProperty("protocol")]
            public string Protocol { get; set; }
            [JsonProperty("host")]
            public List<string> Host { get; set; }
            [JsonProperty("path")]
            public List<string> Path { get; set; }
        }
    }
}
