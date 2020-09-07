using System.Runtime.InteropServices;

namespace BetterExternalRaytrace
{
    [StructLayout(LayoutKind.Sequential)]
    public struct dbrushside_t
    {
        public ushort m_Planenum;
        public short m_Texinfo;
        public short m_Dispinfo;
        public byte m_Bevel;
        public byte m_Thin;
    }
}
