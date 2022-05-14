using CitizenFX.Core;

namespace Proline.Resource.Console
{
    public static class EConsole
    {
        public static void WriteLine(int data)
        {
            WriteLine(data.ToString());
        }

        public static void WriteLine(bool data)
        {
            WriteLine(data.ToString());
        }

        public static void WriteLine(long data)
        {
            WriteLine(data.ToString());
        }

        public static void WriteLine(string data)
        {
            Debug.WriteLine(data);
        }

        public static void WriteLine(object data)
        {
            WriteLine(data.ToString());
        }

        public static void Write(int data)
        {
            Write(data.ToString());
        }

        public static void Write(bool data)
        {
            Write(data.ToString());
        }

        public static void Write(long data)
        {
            Write(data.ToString());
        }

        public static void Write(string data)
        {
            Debug.Write(data);
        }

        public static void Write(object data)
        {
            WriteLine(data.ToString());
        }
    }
}
