using Microsoft.AspNetCore.Mvc;

public static class ProblemHelper
{
    public static ProblemDetailsException Create(string title, int statusCode, string detail)
    {
        return new ProblemDetailsException(title, statusCode, detail);
    }
}