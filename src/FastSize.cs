using System;
using System.Runtime.InteropServices;

namespace BetterExternalRaytrace
{
    public static class FastSize<T>
    {
        /// <summary>
        /// The real type.
        /// </summary>
        public static readonly Type Type;

        /// <summary>
        /// The typecode of the type.
        /// </summary>
        public static readonly TypeCode TypeCode;

        /// <summary>
        /// The size of the type. (Unsigned Integer)
        /// </summary>
        public static readonly uint Size;

        /// <summary>
        /// The size of the type. (Signed Integer)
        /// </summary>
        public static readonly int SizeSigned;


        static FastSize()
        {
            Type = typeof(T);
            TypeCode = Type.GetTypeCode(Type);
            SizeSigned = (TypeCode == TypeCode.Boolean) ? 1 : Marshal.SizeOf(Type);
            Size = (uint)(SizeSigned);
        }
    }
}
