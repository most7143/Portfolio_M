using System;

public static class EXEnum
{
    public static TEnum Parse<TEnum>(string value, bool ignoreCase = true)
       where TEnum : struct, Enum
    {
        if (Enum.TryParse(value, ignoreCase, out TEnum result))
        {
            return result;
        }

        return default;
    }
}