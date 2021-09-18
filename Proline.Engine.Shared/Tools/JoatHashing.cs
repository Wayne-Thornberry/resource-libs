namespace Proline.Engine.Tools
{
    public static class JoatHashing
    {
        internal static int GenerateHash(string key)
        {
            var hash = 0;
            int x = key.Length;
            var chars = key.ToCharArray();

            for (int i = x - 1; i >= 0; i--)
            {
                hash += chars[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash;
        }
    }
}
