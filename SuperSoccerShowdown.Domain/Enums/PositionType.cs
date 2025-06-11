using System.Text.Json.Serialization;

namespace SuperSoccerShowdown.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum PositionType
    {
        Goalie,
        Defence,
        Offence
    }

}
