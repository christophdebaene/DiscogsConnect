using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DiscogsConnect
{
    internal class DiscogsContractResolver : DefaultContractResolver
    {
        private static readonly Dictionary<Type, string> mapping = new Dictionary<Type, string>
        {
            { typeof(ArtistRelease), "Releases"},
            { typeof(LabelRelease), "Releases"},
            { typeof(MasterVersion), "Versions"},
            { typeof(SearchResult), "Results"},
            { typeof(CollectionItem), "Releases"},
            { typeof(Wants), "Wants"},
        };

        public DiscogsContractResolver()
        {
            NamingStrategy = new SnakeCaseNamingStrategy();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);

            if (typeof(PaginationResponse).GetTypeInfo().IsAssignableFrom(member.DeclaringType.GetTypeInfo()) &&
                member.Name == "Items")
            {
                var resourceType = member.DeclaringType.GenericTypeArguments[0];
                jsonProperty.PropertyName = mapping[resourceType];
            }

            return jsonProperty;
        }
    }
}