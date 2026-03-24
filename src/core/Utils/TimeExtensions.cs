public static class TimeExtensions
{
    /// <summary>Converte um inteiro para milissegundos.</summary>
    public static TimeSpan Millis(this int number)
    {
        return TimeSpan.FromMilliseconds(number);
    }

    /// <summary>Cria uma espera assíncrona baseado no tempo informado.</summary>
    /// <example>
    /// <code>
    /// await 50.Millis().Delay();
    /// </code>
    /// </example>
    public static Task Delay(this TimeSpan time)
    {
        return Task.Delay(time);
    }
}
