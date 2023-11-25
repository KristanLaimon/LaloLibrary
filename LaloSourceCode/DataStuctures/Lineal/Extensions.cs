using LaloLibrary.DataStuctures;

namespace LaloLibrary.DataStructures.Lineal
{
    public static class Extensions
    {
        public static LinkedCircularList<T> ToCircularList<T>(this T[] array)
        {
            LinkedCircularList<T> list = new();
            list.Add(array);
            return list;
        }

        public static LinkedDoubleCircularList<T> ToDoubleCircularList<T>(this T[] array)
        {
            LinkedDoubleCircularList<T> list = new();
            list.Add(array);
            return list;
        }
    }
}