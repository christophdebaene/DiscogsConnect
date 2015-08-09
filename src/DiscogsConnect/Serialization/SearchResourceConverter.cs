using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace DiscogsConnect.Serialization
{
    // http://stackoverflow.com/questions/8030538/how-to-implement-custom-jsonconverter-in-json-net-to-deserialize-a-list-of-base

    class SearchResourceConverter : JsonConverter
    {
        protected SearchResult Create(Type objectType, Newtonsoft.Json.Linq.JObject jObject)
        {
            var type = (string)jObject.Property("type");
            switch (type.ToLower())
            {
                case "artist":
                    return new ArtistSearchResult();

                case "label":
                    return new LabelSearchResult();

                case "master":
                    return new MasterSearchResult();

                case "release":
                    return new ReleaseSearchResult();
            }

            throw new Exception(string.Format("The given result type {0} is not supported!", type));
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(SearchResult).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}