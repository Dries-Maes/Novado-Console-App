using Novado_Console_App.Resources;

namespace Novado_Main
{
    public class SegmentedDisplay
    {
        public static int SegmentedDisplayMethod()
        {
            string FileName = "../../../Resources/input.txt";

            var reader = new StreamReader(FileName);
            Dictionary<string[], string[]> Lines = new();
            Dictionary<char, char> SegmentMapper = new();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Lines.Add(line.Trim()
                    .Split(" | ")[0]
                    .Split(' ')
                    .Select(x => string.Concat(x.Trim().ToCharArray().OrderBy(x => x)))
                    .OrderBy(x => x.Length)
                    .ToArray(), line
                    .Split('|')[1].Trim()
                    .Split(' ')
                    .Select(x => string.Concat(x.ToCharArray().OrderBy(x => x)))
                    .ToArray());
            }

            var result = 0;
            foreach (var keyValuePair in Lines)
            {
                Dictionary<string, int> DigitMapper = new();
                var testGetal = keyValuePair.Value;
                DigitMapper = MapNumbers(keyValuePair);
                var testGetalInt = 0;
                foreach (string number in testGetal)
                {
                    var digit = DigitMapper[number];
                    testGetalInt *= 10;
                    testGetalInt += digit;
                }

                result += testGetalInt;
            }
            return result;
        }

        public static Dictionary<string, int> MapNumbers(KeyValuePair<string[], string[]> dict)
        {
            var DigitShapes = string.Join(" ", dict.Key);

            var een = dict.Key[0];
            var vier = dict.Key[2];
            var zeven = dict.Key[1];
            var acht = dict.Key[9];
            var drie = string.Concat(dict.Key.Where(x => x.Length == 5 && x.ContainsCharactersFrom(zeven)));
            var negen = string.Concat(dict.Key.Where(x => x.Length == 6 && x.ContainsCharactersFrom(drie)));
            var vijf = string.Concat(dict.Key.Where(x => x.Length == 5 && negen.ContainsCharactersFrom(x) && x != drie));
            var twee = string.Concat(dict.Key.Where(x => x.Length == 5 && x != drie && x != vijf));
            var zes = string.Concat(dict.Key.Single(x => x.Length == 6 && x.ContainsCharactersFrom(vijf) && x != negen));
            var nul = string.Concat(dict.Key.Single(x => x.Length == 6 && x != zes && x != negen));
            return new Dictionary<string, int>(){
        { string.Concat(een.ToCharArray().OrderBy(x => x)), 1 },
        { string.Concat(twee.ToCharArray().OrderBy(x => x)), 2 },
        { string.Concat(drie.ToCharArray().OrderBy(x => x)), 3 },
        { string.Concat(vier.ToCharArray().OrderBy(x => x)), 4 },
        { string.Concat(vijf.ToCharArray().OrderBy(x => x)), 5 },
        { string.Concat(zes.ToCharArray().OrderBy(x => x)), 6 },
        { string.Concat(zeven.ToCharArray().OrderBy(x => x)), 7 },
        { string.Concat(acht.ToCharArray().OrderBy(x => x)), 8 },
        { string.Concat(negen.ToCharArray().OrderBy(x => x)), 9 },
        { string.Concat(nul.ToCharArray().OrderBy(x => x)), 0 } };
        }
    }
}