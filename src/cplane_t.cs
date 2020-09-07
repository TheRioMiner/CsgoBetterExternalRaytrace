using System.Runtime.InteropServices;

namespace BetterExternalRaytrace
{
    [StructLayout(LayoutKind.Sequential)]
    public struct cplane_t
    {
        public Vector3 m_Normal;
        public float m_Distance;
        public byte m_Type;
        public byte m_SignBits;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private byte[] m_Pad;
    }
}
