using System.Runtime.InteropServices;

namespace BetterExternalRaytrace
{
    [StructLayout(LayoutKind.Sequential)]
    public struct snode_t
    {
        public int m_Planenum;
        public long m_pPlane;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] m_Children;

        public long m_LeafChildren;
        public long m_NodeChildren;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public short[] m_Mins;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public short[] m_Maxs;

        public ushort m_Firstface;
        public ushort m_Numfaces;
        public short m_Area;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private byte[] m_Pad;

        public snode_t(int a)
        {
            m_Planenum = 0;
            m_pPlane = 0;
            m_Children = new int[2];
            m_LeafChildren = 0;
            m_NodeChildren = 0;
            m_Mins = new short[3];
            m_Maxs = new short[3];
            m_Firstface = 0;
            m_Numfaces = 0;
            m_Area = 0;
            m_Pad = new byte[2];
        }
    }
}
