namespace Medyk.Test.PrivateLessons
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            LogToFile(message);
            LogToFile2(message);
            LogToDB(message);
        }

        private void LogToDB(string message)
        {
            //scary DB logging
        }

        private void LogToFile(string message)
        {
            //scary File logging
        }

        public virtual void LogToFile2(string message)
        {
            //scary File logging
        }
    }
}