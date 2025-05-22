

namespace Delegates.Extension
{
    public static class ExtensionFunction
    {
        public static int WordCount(this string st)
        {
            return st.Split(" ").Length;
        }
    }
}