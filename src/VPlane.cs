using System.Runtime.InteropServices;

namespace BetterExternalRaytrace
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VPlane
    {
        public Vector3 m_Origin;
        public float m_Distance;

        public VPlane(Vector3 origin, float dist)
        {
            m_Origin = origin;
            m_Distance = dist;
        }

        public float DistTo(Vector3 location)
        {
            return Vector3.Dot(m_Origin, location) - m_Distance;
        }
    }
}
