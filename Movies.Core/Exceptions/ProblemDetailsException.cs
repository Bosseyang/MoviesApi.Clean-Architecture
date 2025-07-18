using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class ProblemDetailsException : Exception
{
    public ProblemDetails ProblemDetails { get; }

    public ProblemDetailsException(string title, int statusCode, string detail)
    {
        ProblemDetails = new ProblemDetails
        {
            Title = title,
            Detail = detail,
            Status = statusCode,
        };
    }
}
