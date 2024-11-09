using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // setup logging here
            Console.WriteLine($"Error on ");
            if (context.Exception is DbUpdateException)
            {
                context.Result = new StatusCodeResult(500);
            } else
            {
                context.Result = new StatusCodeResult(500);
            }
        }
    }
}
