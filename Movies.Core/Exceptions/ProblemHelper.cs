using Microsoft.AspNetCore.Mvc;

public static class ProblemHelper
{
    public static ProblemDetailsException Create(int statusCode, string detail)
    {
        return new ProblemDetailsException(statusCode, detail);
    }
}