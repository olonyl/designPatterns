using System;

namespace DesignPatternsDecorator
{
    public class ReportingService : IReportingService
    {
        public void Report() {
            Console.WriteLine("Here is your report");
        }
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

    }
}
