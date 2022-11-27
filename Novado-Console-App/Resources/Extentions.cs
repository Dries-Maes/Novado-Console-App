namespace Novado_Console_App.Resources
{
    public static class Extensions
    {
        public static bool ContainsCharactersFrom(this string str, string toCompare)
        {
            foreach (var c in toCompare.ToCharArray())
            {
                if (!str.Contains(c)) return false;
            }
            return true;
        }

        public static int CountChar(this string source, char toFind)
        {
            return source.Count(t => t == toFind);
        }
    }
}