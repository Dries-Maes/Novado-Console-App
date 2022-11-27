using Novado_Console_App.Resources;
using System.Data;

namespace Novado_Console_App
{
    public class FractalLogic
    {
        public static char[,] init = new char[,] { { '.', '#', '.' }, { '.', '.', '#' }, { '#', '#', '#' } };

        public static int FractalLogicMethod(int amountOfItterations)
        {
            var iteration = 0;
            while (iteration < amountOfItterations)
            {
                List<char[,]> Transformables = ExtractTransformables(init);
                List<char[,]> postTransformables = TransformClustersUsingKey(Transformables);
                init = RecreateGrid(postTransformables);
                iteration++;
            };

            int pixels = 0;
            for (int i = 0; i < init.GetLongLength(0); i++)
            {
                for (int j = 0; j < init.GetLongLength(0); j++)
                {
                    pixels = init[i, j] == '#' ? pixels+1 : pixels;
                }
            }
            return pixels;
        }

        private static char[,] RecreateGrid(List<char[,]> postTransformables)
        {
            double BlockWidth = postTransformables[0].GetLongLength(0);
            double currentRasterDimension = Math.Sqrt(postTransformables.Count * (Math.Pow(BlockWidth, 2)));
            char[,] result = new char[(int)currentRasterDimension, (int)currentRasterDimension];

            for (int i = 0; i < currentRasterDimension; i++)
            {
                for (int j = 0; j < (currentRasterDimension / BlockWidth); j++)
                {
                    for (int k = 0; k < BlockWidth; k++)
                    {
                        result[i, j*(int)BlockWidth+k] = postTransformables[j][(i % (int)BlockWidth), k];
                    }
                }
            }
            return result;
        }

        private static List<char[,]> TransformClustersUsingKey(List<char[,]> preTransformables)
        {
            List<char[,]> results = new();
            results = preTransformables.Select(x => FractalSeedFactory.SourceKeys.FirstOrDefault(y => FractalSeedFactory.SequenceEquals(y.Key, x)).Value).ToList();
            return results;
        }

        public static List<char[,]> ExtractTransformables(char[,] init)
        {
            List<char[,]> Transformables = new();

            if (init.GetLength(1) % 2 == 0)

            {
                for (int i = 0; i < init.GetLength(1) / 2; i++)
                {
                    for (int j = 0; j < init.GetLength(1) / 2; j++)
                    {
                        Transformables.Add(new char[,] {  { init[(i*2) +0, (j*2)+0] , init[(i*2) +0, (j*2)+1] },
                                                          { init[(i*2) +1, (j*2)+0] , init[(i*2) +1, (j*2)+1] },
                                                       });
                    }
                }
            }
            else
            {
                for (int i = 0; i < init.GetLength(1) / 3; i++)
                {
                    for (int j = 0; j < init.GetLength(1) / 3; j++)
                    {
                        Transformables.Add(new char[,] {  { init[(i*3) +0, (j*3)+0] , init[(i*3) +0, (j*3)+1], init[(i*3) +0, (j*3)+2] },
                                                          { init[(i*3) +1, (j*3)+0] , init[(i*3) +1, (j*3)+1], init[(i*3) +1, (j*3)+2] },
                                                          { init[(i*3) +2, (j*3)+0] , init[(i*3) +2, (j*3)+1], init[(i*3) +2, (j*3)+2] } });
                    }
                }
            }

            return Transformables;
        }
    }
}