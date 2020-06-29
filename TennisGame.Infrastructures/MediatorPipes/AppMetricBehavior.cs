using System;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Meter;
using App.Metrics.Timer;
using MediatR;

namespace TennisGame.Infrastructures.MediatorPipes
{
    public class AppMetricBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        #region Fields

        private readonly IMetricsRoot _metrics;
        private readonly MeterOptions _requestMetric;
        private readonly TimerOptions _exeTimer;
        private readonly MeterOptions _exception;

        #endregion Fields

        #region Constructors

        public AppMetricBehavior(IMetricsRoot metrics)
        {
            _metrics = metrics;
            _requestMetric = new MeterOptions()
            {
                Name = "Mediator Requests",
                MeasurementUnit = App.Metrics.Unit.Requests,
                RateUnit = TimeUnit.Seconds,
                Context = "application"
            };
            _exeTimer = new TimerOptions()
            {
                Name = "Mediator Requests Execution Time",
                MeasurementUnit = App.Metrics.Unit.Requests,
                DurationUnit = TimeUnit.Milliseconds,
                RateUnit = TimeUnit.Seconds,
                Context = "application"
            };
            _exception = new MeterOptions()
            {
                Name = "Mediator Requests Exceptions",
                MeasurementUnit = App.Metrics.Unit.Requests,
                RateUnit = TimeUnit.Seconds,
                Context = "application"
            };
        }

        #endregion Constructors

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var tags = new MetricTags("request", request.GetType().Name);

            _metrics.Measure.Meter.Mark(_requestMetric, tags);
            using (_metrics.Measure.Timer.Time(_exeTimer, tags))
            {
                try
                {
                    return await next();
                }
                catch (Exception ex)
                {
                    _metrics.Measure.Meter.Mark(_exception,
                        new MetricTags(new[] { "request", "exception" },
                        new[] { request.GetType().Name, ex.GetType().FullName }));
                    throw;
                }
            }
        }
    }
}