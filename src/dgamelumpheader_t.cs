using System.Runtime.InteropServices;

namespace BetterExternalRaytrace
{
    [StructLayout(LayoutKind.Sequential)]
    public struct dgamelumpheader_t
    {
        public int m_LumpCount;
    }
}
