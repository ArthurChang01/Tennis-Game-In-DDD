using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using TennisGame.Events;
using TennisGame.Models;

namespace TennisGame.Applications.Serializers.JsonConverters
{
    public class GameInitialEventConverter : JsonConverter<GameInitialEvent>
    {
        public override GameInitialEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string id = string.Empty;
            int version = 0;
            DateTimeOffset occuredDate = default;
            GameId gameId = null;
            List<Team> teams = new List<Team>();
            GameStatus status = default;
            string score = string.Empty;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return new GameInitialEvent(id, version, occuredDate, gameId, teams, status, score);

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propName = reader.GetString();
                    reader.Read();
                    switch (propName)
                    {
                        case nameof(GameInitialEvent.Id):
                            id = reader.GetString();
                            break;

                        case nameof(GameInitialEvent.Version):
                            version = reader.GetInt32();
                            break;

                        case nameof(GameInitialEvent.OccuredDate):
                            occuredDate = DateTimeOffset.Parse(reader.GetString(), CultureInfo.InvariantCulture);
                            break;

                        case nameof(GameInitialEvent.GameId):
                            gameId = JsonSerializer.Deserialize<GameId>(ref reader, options);
                            break;

                        case nameof(GameInitialEvent.Teams):
                            if (reader.TokenType == JsonTokenType.StartArray)
                            {
                                while (reader.Read())
                                {
                                    if (reader.TokenType == JsonTokenType.EndArray)
                                        break;

                                    teams.Add(JsonSerializer.Deserialize<Team>(ref reader, options));
                                }
                            }
                            break;

                        case nameof(GameInitialEvent.Status):
                            status = Enum.Parse<GameStatus>(reader.GetString());
                            break;

                        case nameof(GameInitialEvent.Score):
                            score = reader.GetString();
                            break;
                    }
                }
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, GameInitialEvent value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(value.Id), value.Id);
            writer.WriteNumber(nameof(value.Version), value.Version);
            writer.WriteString(nameof(value.OccuredDate), value.OccuredDate.ToString("yyyy/MM/dd HH:mm:ss"));

            writer.WritePropertyName(nameof(value.GameId));
            JsonSerializer.Serialize(writer, value.GameId, options);

            writer.WritePropertyName(nameof(value.Teams));
            writer.WriteStartArray();
            foreach (var team in value.Teams)
            {
                JsonSerializer.Serialize(writer, team, options);
            }
            writer.WriteEndArray();

            writer.WriteString(nameof(value.Status), value.Status.ToString());
            writer.WriteString(nameof(value.Score), value.Score);

            writer.WriteEndObject();
        }
    }
}