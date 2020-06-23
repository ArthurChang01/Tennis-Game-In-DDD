using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using TennisGame.Models;

namespace TennisGame.Applications.Serializers.JsonConverters
{
    public class TeamConverter : JsonConverter<Team>
    {
        public override Team Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string id = string.Empty;
            string name = string.Empty;
            List<Player> players = new List<Player>();
            int score = 0;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return new Team(id, name, score, players.ToArray());

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propName = reader.GetString();
                    reader.Read();
                    switch (propName)
                    {
                        case nameof(Team.Id):
                            id = reader.GetString();
                            break;

                        case nameof(Team.Name):
                            name = reader.GetString();
                            break;

                        case nameof(Team.Players):
                            if (reader.TokenType == JsonTokenType.StartArray)
                            {
                                while (reader.Read())
                                {
                                    if (reader.TokenType == JsonTokenType.EndArray)
                                        break;

                                    players.Add(JsonSerializer.Deserialize<Player>(ref reader, options));
                                }
                            }
                            break;

                        case nameof(Team.Score):
                            score = reader.GetInt32();
                            break;
                    }
                }
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, Team value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(value.Id), value.Id);
            writer.WriteString(nameof(value.Name), value.Name);

            writer.WritePropertyName(nameof(value.Players));
            writer.WriteStartArray();
            foreach (var player in value.Players)
            {
                JsonSerializer.Serialize(writer, player, options);
            }
            writer.WriteEndArray();

            writer.WriteNumber(nameof(value.Score), value.Score);

            writer.WriteEndObject();
        }
    }
}