using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class ProblemDetailsException : Exception
{
    public ProblemDetails ProblemDetails { get; }

    public ProblemDetailsException(int statusCode, string detail)
    {
        ProblemDetails = new ProblemDetails
        {
            Title = "Invalid Request",
            Detail = detail,
            Status = statusCode,
        };
    }
}
