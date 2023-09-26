using System;

namespace DesignPatternsDecorator
{
    public class ReportingServiceWithLogging : IReportingService
    {
        private IReportingService decorated;
        public ReportingServiceWithLogging(IReportingService decorated)
        {
            this.decorated = decorated;
        }

        public void Report()
        {
            Console.WriteLine("Commencing log...");
            decorated.Report();
            Console.WriteLine("Ending log...");
        }
    }
}
