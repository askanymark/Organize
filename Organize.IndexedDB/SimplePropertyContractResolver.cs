using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Organize.IndexedDB;

public class SimplePropertyContractResolver : DefaultContractResolver
{
    public SimplePropertyContractResolver()
    {
        NamingStrategy = new CamelCaseNamingStrategy();
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);
        var propertyType = property.PropertyType;
        var isSimpleProperty = propertyType == typeof(int) || propertyType == typeof(string) ||
                               propertyType == typeof(bool) || propertyType == typeof(decimal) ||
                               propertyType == typeof(float) || propertyType == typeof(double) || propertyType.IsEnum;

        if (isSimpleProperty) property.ShouldSerialize = instance => true;
        else property.ShouldSerialize = instance => false;

        return property;
    }
}