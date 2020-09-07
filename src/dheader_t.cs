using System.Runtime.InteropServices;

namespace BetterExternalRaytrace
{
    [StructLayout(LayoutKind.Sequential)]
    public struct dheader_t
    {
        public int m_Ident;
        public int m_Version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = BSPFlags.HEADER_LUMPS)]
        public lump_t[] m_Lumps;
        public int m_MapRevision;
    }
}
