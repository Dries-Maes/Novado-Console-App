namespace Novado_Main
{
    public class StreamProcessing
    {
        public static bool IgnoreTriggered { get; set; }
        public static bool OnzinOpened { get; set; }
        public static int LevelOfGroup { get; set; }
        public static int ScoreResult { get; set; }
        public static int OnzinResult { get; set; }

        public static int ProcessStream(string inputStream, string returnType)
        {
            IgnoreTriggered = false;
            OnzinOpened = false;
            LevelOfGroup = 0;
            ScoreResult = 0;
            OnzinResult = 0;

            for (int i = 0; i < inputStream.Length; i++)
            {
                if (!IgnoreTriggered)
                {
                    if (!OnzinOpened || inputStream[i] == '!')
                    {
                        switch (inputStream[i])
                        {
                            case '!':
                                IgnoreTriggered = true;
                                break;

                            case '<':
                                OnzinOpened = true;
                                break;

                            case '{':
                                OnzinResult = OnzinOpened ? OnzinResult += 1 : OnzinResult;
                                LevelOfGroup = OnzinOpened ? LevelOfGroup : LevelOfGroup += 1;
                                break;

                            case '}':
                                OnzinResult = OnzinOpened ? OnzinResult += 1 : OnzinResult;
                                if (OnzinOpened) { break; }
                                ScoreResult += LevelOfGroup;
                                LevelOfGroup = (LevelOfGroup > 0) ? LevelOfGroup -= 1 : LevelOfGroup;
                                break;

                            default:
                                OnzinResult = OnzinOpened ? OnzinResult += 1 : OnzinResult;
                                break;
                        }
                    }
                    else
                    {
                        OnzinOpened = inputStream[i] == '>' ? false : OnzinOpened;
                        OnzinResult = inputStream[i] == '>' ? OnzinResult : OnzinResult += 1;
                    }
                }
                else
                {
                    IgnoreTriggered = false;
                }
            }
            return returnType == "score" ? ScoreResult : returnType == "onzin" ? OnzinResult : 0;
        }
    }
}