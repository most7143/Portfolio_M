public static class EXColor
{
    public static string SetGradeColor(GradeTypes type, string text)
    {
        return string.Format("<style={0}>{1}</style>", type.ToString(), text);
    }
}