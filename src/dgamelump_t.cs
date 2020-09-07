using System.Runtime.InteropServices;

namespace BetterExternalRaytrace
{
    [StructLayout(LayoutKind.Sequential)]
    public struct dgamelump_t
    {
        public int m_Id;
        public ushort m_Flags;
        public ushort m_Version;
        public int m_FileOfs;
        public int m_FileLen;
    }
}
