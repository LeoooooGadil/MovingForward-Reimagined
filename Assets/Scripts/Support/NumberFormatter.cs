
public static class NumberFormatter
{
    public static string FormatNumberWithThousandsSeparator(long number)
    {
        return number.ToString("N0").Replace(",", " ");
    }

    public static string FormatNumberWithThousandsSeparator(float number)
    {
        return number.ToString("N0").Replace(",", " ");
    }
}
