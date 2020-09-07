namespace BetterExternalRaytrace
{
    struct trace_t
    {
        /// Determine if plane is NOT valid
        public bool m_AllSolid;
        /// Determine if the start point was in a solid area
        public bool m_StartSolid;
        /// Time completed, 1.0 = didn't hit anything :)
        public float m_Fraction;
        public float m_FractionLeftSolid;
        /// Final trace position
        public Vector3 m_EndPos;
        public cplane_t m_pPlane;
        public int m_Contents;
        public dbrush_t m_pBrush;
        public int m_nBrushSide;

        public static trace_t Create()
        {
            trace_t t = new trace_t();
            t.m_AllSolid = true;
            t.m_StartSolid = true;
            t.m_Fraction = 1f;
            t.m_FractionLeftSolid = 1f;
            t.m_EndPos = Vector3.Zero;
            t.m_Contents = 0;
            t.m_nBrushSide = 0;

            return t;
        }
    }
}
