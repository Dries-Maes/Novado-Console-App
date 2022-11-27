namespace Novado_Console_App
{
    public class Checksum
    {
        public static int ChecksumManipulator(string input)
        {
            int aggregateResult = 0;
            char previous = input[input.Length - 1];

            foreach (char item in input)
            {
                if (item == previous)
                {
                    aggregateResult += item - '0';
                };

                previous = item;
            }

            return aggregateResult;
        }

        public static int ChecksumBonus(string input)
        {
            int aggregateResult = 0;
            int halfLength = input.Length / 2;
            int length = input.Length;

            for (int i = 0; i < input.Length; i++)
            {
                char currentNumber = input[i];

                if (currentNumber == (input[(i + halfLength) >= length ? (i + halfLength - length) : (i + halfLength)]))
                {
                    aggregateResult += input[i] - '0';
                }
            }

            return aggregateResult;
        }
    }
}