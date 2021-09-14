using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace StarlightLagger
{
    internal static class Serialization
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] ToByteArray(object obj)
        {
            if (obj == null) return null;
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T IL2CPPFromByteArray<T>(byte[] data)
        {
            if (data == null) return default;
            var bf = new Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            var ms = new Il2CppSystem.IO.MemoryStream(data);
            object obj = bf.Deserialize(ms);
            return (T) obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T FromManagedToIL2CPP<T>(object obj)
        {
            return IL2CPPFromByteArray<T>(ToByteArray(obj));
        }
    }
}