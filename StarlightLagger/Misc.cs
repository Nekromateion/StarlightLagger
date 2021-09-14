using MelonLoader;
using System.Collections;
using System.Runtime.CompilerServices;

namespace StarlightLagger
{
    internal static class Misc
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Start(this IEnumerator e)
        {
            MelonCoroutines.Start(e);
        }
    }
}