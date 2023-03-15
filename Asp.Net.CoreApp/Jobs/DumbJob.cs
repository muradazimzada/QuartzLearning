using Quartz;

namespace Asp.Net.CoreApp.Jobs
{
    public class DumbJob : IJob
    {
        public string? JobSays { private get; set; }
        public float MyFloatValue { private get; set; }
        public async Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;

            JobDataMap dataMap = context.JobDetail.JobDataMap;  // Note the difference from the previous example
            Console.WriteLine("Job Called");
            //IList<DateTimeOffset> state = (IList<DateTimeOffset>)dataMap["myStateData"];
            //state.Add(DateTimeOffset.UtcNow);
            //string jobSays = dataMap.GetString("jobSays");
            //float myFloatValue = dataMap.GetFloat("myFloatValue");
            await Console.Error.WriteLineAsync("Instance " + key + " of DumbJob says: " + JobSays + ", and val is: " + MyFloatValue);
           
        }
    }
}
