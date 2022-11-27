using System.Data;
using System.Reflection.PortableExecutable;

namespace Novado_Console_App.Resources
{
    public class FractalSeedFactory
    {
        private static Dictionary<char[,], char[,]> _sourceKeys = new();

        public static Dictionary<char[,], char[,]> SourceKeys
        {
            get
            {
                if (_sourceKeys.Count == 0)
                {
                    FractalSeedMethod();
                }
                return _sourceKeys;
            }
        }

        public static void FractalSeedMethod()
        {
            string FileName = "../../../Resources/fractal.txt";
            var reader = new StreamReader(FileName);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] gesplitsteTekst = line.Split(" => ");
                string[] keyRows = gesplitsteTekst[0].Split('/');
                string[] valueRows = gesplitsteTekst[1].Split('/');
                ProduceKeys(ConvertToArray(keyRows), ConvertToArray(valueRows));
            }

            RemoveDoublesFromSourceKey();
        }

        public static char[,] ConvertToArray(string[] input)
        {
            switch (input.Count())
            {
                case 2:
                    char[,] fractalKey2d = { { input[0][0] , input[0][1] },
                                             { input[1][0] , input[1][1] } };
                    return fractalKey2d;

                case 3:
                    char[,] fractalValue3d = { { input[0][0], input[0][1], input[0][2] },
                                               { input[1][0], input[1][1], input[1][2] },
                                               { input[2][0], input[2][1], input[2][2] } };
                    return fractalValue3d;

                case 4:
                    char[,] fractalValue4d = { { input[0][0], input[0][1], input[0][2], input[0][3] },
                                               { input[1][0], input[1][1], input[1][2], input[1][3] },
                                               { input[2][0], input[2][1], input[2][2], input[2][3] },
                                               { input[3][0], input[3][1], input[3][2], input[3][3] },};
                    return fractalValue4d;

                default:
                    return null;
            }
        }

        public static void ProduceKeys(char[,] sourceKey, char[,] sourceValue)
        {
            List<char[,]> fractalKeyVariations = CreateKeyVariations(sourceKey);

            foreach (var item in fractalKeyVariations)
            {
                _sourceKeys.Add(item, sourceValue);
            }
        }

        public static void RemoveDoublesFromSourceKey()
        {
            Dictionary<char[,], char[,]> results = new();

            foreach (var item in SourceKeys)
            {
                if (!results.Any(x => SequenceEquals(x.Key, item.Key)))
                    results.Add(item.Key, item.Value);
            }

            _sourceKeys = results;
        }

        public static bool SequenceEquals(char[,] a, char[,] b)
        {
            return a.Rank == b.Rank
                   && Enumerable.Range(0, a.Rank).All(d => a.GetLength(d) == b.GetLength(d))
                   && a.Cast<char>().SequenceEqual(b.Cast<char>());
        }

        public static List<char[,]> CreateKeyVariations(char[,] fractalKey)
        {
            List<char[,]> Variations = new()
            {
                fractalKey ,
            };
            fractalKey = Transpose(fractalKey);
            Variations.Add(fractalKey);
            fractalKey = Transpose(fractalKey);
            fractalKey = MirrorHorizontal(Transpose(fractalKey));
            Variations.Add(fractalKey);
            fractalKey = MirrorHorizontal(Transpose(fractalKey));
            Variations.Add(fractalKey);
            fractalKey = MirrorHorizontal(Transpose(fractalKey));
            Variations.Add(fractalKey);
            fractalKey = MirrorHorizontal(fractalKey);
            Variations.Add(fractalKey);
            fractalKey = MirrorHorizontal(Transpose(fractalKey));
            Variations.Add(fractalKey);
            fractalKey = MirrorHorizontal(Transpose(fractalKey));
            Variations.Add(fractalKey);
            fractalKey = MirrorHorizontal(Transpose(fractalKey));
            Variations.Add(fractalKey);
            fractalKey = MirrorVertical(fractalKey);
            Variations.Add(fractalKey);
            fractalKey = MirrorHorizontal(Transpose(fractalKey));
            Variations.Add(fractalKey);
            fractalKey = MirrorHorizontal(Transpose(fractalKey));
            Variations.Add(fractalKey);
            fractalKey = MirrorHorizontal(Transpose(fractalKey));
            Variations.Add(fractalKey);

            return Variations;
        }

        private static char[,] Transpose(char[,] array)
        {
            int i, j;
            int dimension = array.GetLength(0);
            var result = new char[dimension, dimension];
            for (i = 0; i < array.GetLength(0); i++)
            {
                for (j = 0; j < array.GetLength(1); j++)
                {
                    result[j, i] = array[i, j];
                }
            }
            return result;
        }

        private static char[,] MirrorHorizontal(char[,] array)
        {
            int i, j;
            int dimension = array.GetLength(0);
            var result = new char[dimension, dimension];
            for (i = 0; i < array.GetLength(0); i++)
            {
                for (j = 0; j < dimension; j++)
                {
                    result[j, i] = array[dimension - 1 - j, i];
                }
            }
            return result;
        }

        private static char[,] MirrorVertical(char[,] array)
        {
            int i, j;
            int dimension = array.GetLength(0);
            var result = new char[dimension, dimension];
            for (i = 0; i < array.GetLength(0); i++)
            {
                for (j = 0; j < dimension; j++)
                {
                    result[j, i] = array[j, dimension - 1 - i];
                }
            }
            return result;
        }
    }
}