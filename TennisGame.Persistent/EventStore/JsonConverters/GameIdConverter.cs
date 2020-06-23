using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TennisGame.Models;

namespace TennisGame.Persistent.EventStore.JsonConverters
{
    public class GameIdConverter : JsonConverter<GameId>
    {
        public override GameId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var gameId = string.Empty;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return new GameId(gameId);

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    reader.Read();
                    gameId = reader.GetString();
                }
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, GameId value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(GameId), value.ToString());

            writer.WriteEndObject();
        }
    }
}