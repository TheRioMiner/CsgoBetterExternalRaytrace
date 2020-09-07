using System.Runtime.InteropServices;

namespace BetterExternalRaytrace
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Polygon
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = BSPFlags.MAX_SURFINFO_VERTS)]
        public Vector3[] m_Verts;

        public int m_nVerts;
        public VPlane m_Plane;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = BSPFlags.MAX_SURFINFO_VERTS)]
        public VPlane[] m_EdgePlanes;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = BSPFlags.MAX_SURFINFO_VERTS)]
        public Vector3[] m_Vec2D;

        public int m_Skip;
    }
}
