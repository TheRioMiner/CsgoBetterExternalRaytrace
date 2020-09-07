using System.Runtime.InteropServices;

namespace BetterExternalRaytrace
{
    [StructLayout(LayoutKind.Sequential)]
    public struct dface_t
    {
        public ushort m_Planenum;
        public byte m_Side;
        public byte m_OnNode;
        public int m_Firstedge;
        public short m_Numedges;
        public short m_Texinfo;
        public short m_Dispinfo;
        public short m_SurfaceFogVolumeID;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] m_Styles;

        public int m_Lightofs;
        public float m_Area;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] m_LightmapTextureMinsInLuxels;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] m_LightmapTextureSizeInLuxels;

        public int m_OrigFace;
        public ushort m_NumPrims;
        public ushort m_FIrstPrimID;
        public ushort m_SmoothingGroups;
    }
}
