using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace PhotoGallery.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ImageType
    {
        [EnumMember(Value = "Cake Smash")]
        CakeSmash,
        [EnumMember(Value = "Grow With Me")]
        GrowWithMe,
        [EnumMember(Value = "Maternity")]
        Maternity,
        [EnumMember(Value = "Children")]
        Children,
        [EnumMember(Value = "Family")]
        Family,
        [EnumMember(Value = "Seniors")]
        Seniors,
        [EnumMember(Value = "Weddings")]
        Weddings,
        [EnumMember(Value = "Banner")]
        Banner
    }
}