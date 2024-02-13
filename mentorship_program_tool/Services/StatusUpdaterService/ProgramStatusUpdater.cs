using mentorship_program_tool.Data;

namespace mentorship_program_tool.Services.StatusUpdaterService
{
    public class ProgramStatusUpdater : IHostedService, IDisposable
    {
        private readonly IServiceProvider _services;
        private Timer _timer;

        public ProgramStatusUpdater(IServiceProvider services)
        {
            _services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24)); // Check every 24 hours

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // Replace YourDbContext with your actual DbContext
                var programs = dbContext.Programs;

                foreach (var program in programs)
                {
                    if (program.EndDate.Date < DateTime.UtcNow.Date) // Check if EndDate is before today's date
                    {
                        program.ProgramStatus = 8; // Update ProgramStatus to 8
                    }
                }

                dbContext.SaveChanges(); // Save changes to the database
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}