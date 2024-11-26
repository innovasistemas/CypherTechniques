using CypherTechniques.Utilities;

namespace CypherTechniques.EncodeDecode
{
    class NumberBase
    {
        public string StringInput {get; set;}
        public string StringBaseOutput {get; set;}
        private readonly string[] hexChars = new string[6];
        private readonly int[] digHex = new int[6];
        public string[] chainBases = new string[500];


        public NumberBase()
        {
            StringInput = "";
            StringBaseOutput = "";
            hexChars = ["A", "B", "C", "D", "E", "F"];
            digHex = [10, 11, 12, 13, 14, 15, 16];
        }


        public string EncodeBaseString(int baseN)
        {
            for (int i = 0; i < StringInput.Length; i++) {
                StringBaseOutput += Base10ToBaseN(StringInput[i], baseN) + ",";
            }
            return StringBaseOutput;
        }


        public string DecodeBaseString(int baseN)
        {
            chainBases = StringInput.Split(",");
            for (int i = 0; i < chainBases.Length; i++) {
                StringBaseOutput += (char)BaseNToBase10(chainBases[i], baseN);
            }
            return StringBaseOutput;
        }


        public string Base10ToBaseN(int numAscci, int baseN)
        {
            int res;
            int num = numAscci;
            string resStr;
            string stringBase = "";
            while (num > 0) {
                res = num % baseN;
                num /= baseN;
                if (res >= 10) {
                    resStr = hexChars[res % 10];
                } else {
                    resStr = res.ToString();
                }
                stringBase = resStr + stringBase; 
            }
            return stringBase;
        }


        public int BaseNToBase10(string numChar, int baseN)
        {
            int N = 0;
            int pos;
            for (int i = 0; i < numChar.Length; i++) {
                pos = Arrays.SearchArray(hexChars, numChar.Substring(i, 1));
                if (pos > -1) {
                    N += digHex[pos] * (int)Math.Pow(baseN, numChar.Length - i - 1);
                } else {
                    N += Int32.Parse(numChar.Substring(i, 1)) * (int)Math.Pow(baseN, numChar.Length - i - 1);
                }
            }
            return N;
        }
    }
}