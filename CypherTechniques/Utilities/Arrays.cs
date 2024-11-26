namespace CypherTechniques.Utilities
{
    class Arrays
    {

        public Arrays()
        {

        }


        public static int SearchArray(string[] array, string datum)
        {
            int pos = -1;
            int i = 0;
            while (i < array.Length && pos == -1) {
                if (array[i] == datum) {
                    pos = i;
                } else {
                    i++;
                }
            }
            return pos;
        }
    }
}