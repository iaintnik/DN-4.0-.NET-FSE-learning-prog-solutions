using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace EmployeeAPI.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            string logPath = "C:\\Logs\\exception_log.txt";

            File.AppendAllText(logPath, $"[{DateTime.Now}] {exception.Message}\n");

            context.Result = new ObjectResult("Internal Server Error")
            {
                StatusCode = 500
            };
        }
    }
}
