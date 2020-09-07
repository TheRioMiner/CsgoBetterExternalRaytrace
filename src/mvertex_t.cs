using System.Runtime.InteropServices;

namespace BetterExternalRaytrace
{
    [StructLayout(LayoutKind.Sequential)]
    public struct mvertex_t
    {
        public Vector3 m_Position;
    }
}
