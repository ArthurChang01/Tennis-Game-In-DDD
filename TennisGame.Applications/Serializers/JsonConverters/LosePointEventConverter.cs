using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using TennisGame.Events;
using TennisGame.Models;

namespace TennisGame.Applications.Serializers.JsonConverters
{
    public class LosePointEventConverter : JsonConverter<LosePointEvent>
    {
        public override LosePointEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string id = string.Empty;
            int version = 0;
            DateTimeOffset occuredDate = default;
            string teamId = string.Empty;
            string playerId = string.Empty;
            string newScore = string.Empty;
            GameStatus status = default;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return new LosePointEvent(id, version, occuredDate, teamId, playerId, newScore, status);

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propName = reader.GetString();
                    reader.Read();
                    switch (propName)
                    {
                        case nameof(LosePointEvent.Id):
                            id = reader.GetString();
                            break;

                        case nameof(LosePointEvent.Version):
                            version = reader.GetInt32();
                            break;

                        case nameof(LosePointEvent.OccuredDate):
                            occuredDate = DateTimeOffset.Parse(reader.GetString(), CultureInfo.InvariantCulture);
                            break;

                        case nameof(LosePointEvent.TeamId):
                            teamId = reader.GetString();
                            break;

                        case nameof(LosePointEvent.PlayerId):
                            playerId = reader.GetString();
                            break;

                        case nameof(LosePointEvent.NewScore):
                            newScore = reader.GetString();
                            break;

                        case nameof(LosePointEvent.NewStatus):
                            status = Enum.Parse<GameStatus>(reader.GetString());
                            break;
                    }
                }
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, LosePointEvent value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(value.Id), value.Id);
            writer.WriteNumber(nameof(value.Version), value.Version);
            writer.WriteString(nameof(value.OccuredDate), value.OccuredDate.ToString("yyyy/MM/dd HH:mm:ss"));

            writer.WriteString(nameof(value.TeamId), value.TeamId);
            writer.WriteString(nameof(value.PlayerId), value.PlayerId);
            writer.WriteString(nameof(value.NewScore), value.NewScore);
            writer.WriteString(nameof(value.NewStatus), value.NewStatus.ToString());

            writer.WriteEndObject();
        }
    }
}