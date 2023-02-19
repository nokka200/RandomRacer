namespace ValueTester
{
    static public class ValueChecker
    {
        /// <summary>
        /// Checks if value is greater than maximun target value
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="max">Compare value to this</param>
        /// <returns>Difference, if value is less returns 0</returns>
        static public int CheckIfOverFlow(int value, int max)
        {
            int re = 0;

            if (value > max)
            {
                re = value - max;
                return re;
            }
            else
            {
                return re;
            }
        }

        /// <summary>
        /// Checks if both values are even on max
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>true if even else false</returns>
        static public bool CheckIfEvenOnMax(int first, int second, int max)
        {
            if (first == max && second == max)
            {
                return true;
            }
            return false ;
        }

        static public bool CheckIfEven(int first, int second) 
        {
            return first == second ;
        }

    }
}