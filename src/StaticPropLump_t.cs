using System.Runtime.InteropServices;

namespace BetterExternalRaytrace
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StaticPropLump_t
    {
        //v4
        public Vector3 Origin;       // origin
        public Vector3 Angles;       // orientation (pitch roll yaw)
        public ushort PropType;    // index into model name dictionary
        public ushort FirstLeaf;   // index into leaf array
        public ushort LeafCount;
        public byte Solid;       // solidity type
        public byte Flags;
        public int Skin;        // model skin numbers
        public float FadeMinDist;
        public float FadeMaxDist;
        public Vector3 LightingOrigin;  // for lighting
                                        
        //Since v5
        public float ForcedFadeScale; // fade distance scale

        //v6 and v7 only
        //public ushort MinDXLevel;      // minimum DirectX version to be visible
        //public ushort MaxDXLevel;      // maximum DirectX version to be visible

        //Since v8
        public byte MinCPULevel;
        public byte MaxCPULevel;
        public byte MinGPULevel;
        public byte MaxGPULevel;
        //Since v7
        public int DiffuseModulation; // per instance color and alpha modulation
        //Since v10
        public float unknown;
        //Since v9
        public bool DisableX360;     // if true, don't show on XBox 360
    }
}
