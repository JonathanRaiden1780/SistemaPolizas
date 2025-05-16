public class ApiException : Exception
{
    public int StatusCode { get; }

    public ApiException(string message, int statusCode = 500) : base(message)
    {
        StatusCode = statusCode;
    }
}

public class UnauthorizedException : ApiException
{
    public UnauthorizedException(string message = "No autorizado") : base(message, 401) { }
}

public class ForbiddenException : ApiException
{
    public ForbiddenException(string message = "No tiene permisos para realizar esta acción") : base(message, 403) { }
}

public class NotFoundException : ApiException
{
    public NotFoundException(string message = "Recurso no encontrado") : base(message, 404) { }
}