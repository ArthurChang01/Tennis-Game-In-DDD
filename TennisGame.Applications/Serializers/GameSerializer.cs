using System;
using System.Text;
using System.Text.Json;
using EventStore.ClientAPI;
using TennisGame.Applications.Serializers.JsonConverters;
using TennisGame.Core;
using TennisGame.Events;
using TennisGame.Persistent.EventStore;

namespace TennisGame.Applications.Serializers
{
    public class GameSerializer : IEventSerializer
    {
        private readonly JsonSerializerOptions _opt;

        public GameSerializer()
        {
            _opt = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                MaxDepth = 5
            };
            _opt.Converters.Add(new GameIdConverter());
            _opt.Converters.Add(new PlayersConverter());
            _opt.Converters.Add(new TeamConverter());
            _opt.Converters.Add(new GameInitialEventConverter());
            _opt.Converters.Add(new LosePointEventConverter());
            _opt.Converters.Add(new WinPointEventConverter());
        }

        public DomainEvent Convert(ResolvedEvent @event)
        {
            var meta = JsonSerializer.Deserialize<EventMeta>(@event.Event.Metadata);

            return JsonSerializer.Deserialize(@event.Event.Data, Type.GetType(meta.EventType), _opt) as DomainEvent;
        }

        public EventData Convert(DomainEvent @event)
        {
            var json = GetDataJsonString(@event);
            var data = Encoding.UTF8.GetBytes(json);

            var evnType = @event.GetType();
            var meta = new EventMeta()
            {
                EventType = evnType.AssemblyQualifiedName
            };
            var metaJson = JsonSerializer.Serialize(meta);
            var metaData = Encoding.UTF8.GetBytes(metaJson);

            return new EventData(Guid.Parse(@event.Id), evnType.Name, true, data, metaData);
        }

        private string GetDataJsonString(DomainEvent @event)
            => @event switch
            {
                GameInitialEvent evt => JsonSerializer.Serialize(evt, _opt),
                WinPointEvent evt => JsonSerializer.Serialize(evt, _opt),
                LosePointEvent evt => JsonSerializer.Serialize(evt, _opt),
                _ => string.Empty
            };
    }
}