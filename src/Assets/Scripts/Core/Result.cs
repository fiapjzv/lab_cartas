using System.Diagnostics.CodeAnalysis;

/// <summary>Resultado de uma operação que pode falhar.</summary>
public class Result
{
    private string? error;

    /// <summary>Indica se o resultado representa sucesso.</summary>
    public bool IsOk([NotNullWhen(false)] out string? error)
    {
        error = this.error;
        return error is null;
    }

    /// <summary>Operação foi um sucesso.</summary>
    public static Result Ok() => new();

    /// <summary>Operação falhou com o erro: <paramref name="error"/>.</summary>
    public static Result Err(string error) => new() { error = error };
}
