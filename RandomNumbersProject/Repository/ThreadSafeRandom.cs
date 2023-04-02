using Inreface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomGenerator
{
    internal static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random? _local;
        private static readonly Random Global = new(); // Global instance used to generate seeds

        private static Random Instance
        {
            get
            {
                if (_local is null)
                {
                    int seed;
                    lock (Global) // Ensure no concurrent access to Global
                    {
                        seed = Global.Next();
                    }

                    _local = new Random(seed); // Create [ThreadStatic] instance with specific seed
                }

                return _local;
            }
        }
        public static int Next(int numbersCount) => Instance.Next(1, numbersCount);
    }
}
