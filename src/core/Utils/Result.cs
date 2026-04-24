using System.Diagnostics.CodeAnalysis;

namespace Game.Core.Utils;

/// <inheritdoc cref="Result{T}" />
public readonly struct Result
{
    /// <summary>Cria uma operação que foi um sucesso e retornou <paramref name="value" />.</summary>
    public static Result<T> Ok<T>(T value) => new(value, error: null);

    /// <summary>Cria uma operação foi um fracasso e retornou o erro <paramref name="error" />.</summary>
    public static Result<T> Err<T>(string error) => new(value: default!, error);

    /// <inheritdoc cref="Result{T}" />
    public static Result<Unit> Ok() => new(default, error: null);

    /// <inheritdoc cref="Err{T}(string)" />
    public static Result<Unit> Err(string error) => new(value: default, error);
}

/// <summary>
/// Representa o resultado de uma operação que pode ter sucesso e ter valor ou ter falhado e ter um erro.
/// </summary>
/// <remarks>Nunca pode estar em ambos os estado (sucesso e erro) simultaneamente.</remarks>
public readonly struct Result<T>
{
    private readonly T _value;
    private readonly string? _error;
    private readonly bool _isOk;

    internal Result(T value, string? error)
    {
        _isOk = error is null;

        if (_isOk)
        {
            _value = value;
            _error = null;
        }
        else
        {
            _value = default!;
            _error = error ?? throw new ArgumentNullException(nameof(error));
        }
    }

    /// <summary>
    /// Verifica se o resultado é um sucesso retornado:
    /// - true em caso de sucesso: value preenchido, error null.
    /// - false em caso de erro: value default, error preenchido.
    /// </summary>
    public bool IsOk(
        [NotNullWhen(returnValue: true)] out T? value,
        [NotNullWhen(returnValue: false)] out string? error
    )
    {
        if (_isOk)
        {
            (value, error) = (_value!, null);
            return true;
        }

        (value, error) = (default, _error!);
        return false;
    }

    /// <inheritdoc />
    public override string ToString() => _isOk ? $"Ok({_value})" : $"Err({_error})";
}

public static class ResultExtensions
{
    /// <summary>Converte uma exceção em Result de erro.</summary>
    public static Result<T> AsResult<T>(this Exception ex, bool msgOnly = false) =>
        Result.Err<T>(msgOnly ? ex.Message : ex.ToString());

    /// <summary>Converte uma string em Result de erro.</summary>
    public static Result<T> AsResult<T>(this string error) => Result.Err<T>(error);

    /// <summary>Indica se o resultado é um sucesso ignorando o valor ou erro.</summary>
    public static bool IsOk<T>(this Result<T> result) => result.IsOk(out _, out _);

    /// <summary>Retorna o valor caso seja sucesso.</summary>
    /// <remarks>MÉTODO INSEGURO (joga Exception em caso de não ser sucesso). Só usar se tiver certeza de que foi sucesso.</remarks>
    public static T Unwrap<T>(this Result<T> result) =>
        result.IsOk(out var value, out _)
            ? value
            : throw new InvalidOperationException("No value present");
}
