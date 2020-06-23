using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TennisGame.Models;

namespace TennisGame.Applications.Serializers.JsonConverters
{
    public class PlayersConverter : JsonConverter<Player>
    {
        public override Player Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var id = string.Empty;
            var name = string.Empty;
            var teamId = string.Empty;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return new Player(id, name, teamId);

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propName = reader.GetString();
                    reader.Read();
                    switch (propName)
                    {
                        case "Id":
                            id = reader.GetString();
                            break;

                        case "Name":
                            name = reader.GetString();
                            break;

                        case "TeamId":
                            teamId = reader.GetString();
                            break;
                    }
                }
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, Player value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(value.Id), value.Id);
            writer.WriteString(nameof(value.Name), value.Name);
            writer.WriteString(nameof(value.TeamId), value.TeamId);

            writer.WriteEndObject();
        }
    }
}