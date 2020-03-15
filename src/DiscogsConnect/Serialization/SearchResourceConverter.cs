using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace DiscogsConnect
{
    internal class SearchResourceConverter : JsonConverter
    {
        protected SearchResult Create(Type objectType, JObject jObject) => 
            jObject["type"].ToString().ToLower() switch
            {
                "artist"  => new ArtistSearchResult(),
                "label"   => new LabelSearchResult(),
                "master"  => new MasterSearchResult(),
                "release" => new ReleaseSearchResult(),
                _ => throw new Exception($"The given result type {jObject["type"].ToString()} is not supported!")
            };
    
        public override bool CanConvert(Type objectType)
            => typeof(SearchResult).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();        
    }
}