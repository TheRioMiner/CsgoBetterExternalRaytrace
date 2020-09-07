namespace BetterExternalRaytrace
{
    public class BSPFlags
    {
        public const uint MAX_BRUSH_LIGHTMAP_DIM_WITHOUT_BORDER = 32;
        public const uint MAX_BRUSH_LIGHTMAP_DIM_INCLUDING_BORDER = 35;
        public const uint MAX_DISP_LIGHTMAP_DIM_WITHOUT_BORDER = 128;
        public const uint MAX_DISP_LIGHTMAP_DIM_INCLUDING_BORDER = 131;
        public const uint MAX_LIGHTMAP_DIM_WITHOUT_BORDER = MAX_DISP_LIGHTMAP_DIM_WITHOUT_BORDER;
        public const uint MAX_LIGHTMAP_DIM_INCLUDING_BORDER = MAX_DISP_LIGHTMAP_DIM_INCLUDING_BORDER;

        public const float DIST_EPSILON = 0.03125f;
        public const int MAX_SURFINFO_VERTS = 32;
        public const uint BSPVERSION = 21;
        public const int HEADER_LUMPS = 64;
        public const int MAX_POLYGONS = 50120;
        public const int MAX_MOD_KNOWN = 512;
        public const int MAX_MAP_MODELS = 1024;
        public const int MAX_MAP_BRUSHES = 8192;
        public const int MAX_MAP_ENTITIES = 4096;
        public const int MAX_MAP_ENTSTRING = 256 * 1024;
        public const int MAX_MAP_NODES = 65536;
        public const int MAX_MAP_TEXINFO = 12288;
        public const int MAX_MAP_TEXDATA = 2048;
        public const int MAX_MAP_LEAFFACES = 65536;
        public const int MAX_MAP_LEAFBRUSHES = 65536;
        public const int MIN_MAP_DISP_POWER = 2;
        public const int MAX_MAP_DISP_POWER = 4;
        public const int MAX_MAP_SURFEDGES = 512000;
        public const int MAX_DISP_CORNER_NEIGHBORS = 4;

        /// NOTE: These are stored in a short in the engine now.  Don't use more than 16 bits
        public const uint SURF_LIGHT = 0x0001; /// value will hold the light strength
        public const uint SURF_SLICK = 0x0002; /// effects game physics
        public const uint SURF_SKY = 0x0004; /// don't draw, but add to skybox
        public const uint SURF_WARP = 0x0008; /// turbulent water warp
        public const uint SURF_TRANS = 0x0010;
        public const uint SURF_WET = 0x0020; /// the surface is wet
        public const uint SURF_FLOWING = 0x0040; /// scroll towards angle
        public const uint SURF_NODRAW = 0x0080; /// don't bother referencing the texture
        public const uint SURF_Huint = 0x0100; /// make a primary bsp splitter
        public const uint SURF_SKIP = 0x0200; /// completely ignore, allowing non-closed brushes
        public const uint SURF_NOLIGHT = 0x0400; /// Don't calculate light
        public const uint SURF_BUMPLIGHT = 0x0800; /// calculate three lightmaps for the surface for bumpmapping
        public const uint SURF_HITBOX = 0x8000; /// surface is part of a hitbox

        public const uint CONTENTS_EMPTY = 0;           /// No contents
        public const uint CONTENTS_SOLID = 0x1;         /// an eye is never valid in a solid
        public const uint CONTENTS_WINDOW = 0x2;         /// translucent, but not watery (glass)
        public const uint CONTENTS_AUX = 0x4;
        public const uint CONTENTS_GRATE = 0x8;         /// alpha-tested "grate" textures.  Bullets/sight pass through, but solids don't
        public const uint CONTENTS_SLIME = 0x10;
        public const uint CONTENTS_WATER = 0x20;
        public const uint CONTENTS_MIST = 0x40;
        public const uint CONTENTS_OPAQUE = 0x80;        /// things that cannot be seen through (may be non-solid though)
        public const uint LAST_VISIBLE_CONTENTS = 0x80;
        public const uint ALL_VISIBLE_CONTENTS = LAST_VISIBLE_CONTENTS | LAST_VISIBLE_CONTENTS - 1;
        public const uint CONTENTS_TESTFOGVOLUME = 0x100;
        public const uint CONTENTS_UNUSED3 = 0x200;
        public const uint CONTENTS_UNUSED4 = 0x400;
        public const uint CONTENTS_UNUSED5 = 0x800;
        public const uint CONTENTS_UNUSED6 = 0x1000;
        public const uint CONTENTS_UNUSED7 = 0x2000;
        public const uint CONTENTS_MOVEABLE = 0x4000;      /// hits entities which are MOVETYPE_PUSH (doors, plats, etc.)
                                                           /// remaining contents are non-visible, and don't eat brushes
        public const uint CONTENTS_AREAPORTAL = 0x8000;
        public const uint CONTENTS_PLAYERCLIP = 0x10000;
        public const uint CONTENTS_MONSTERCLIP = 0x20000;
        /// currents can be added to any other contents, and may be mixed
        public const uint CONTENTS_CURRENT_0 = 0x40000;
        public const uint CONTENTS_CURRENT_90 = 0x80000;
        public const uint CONTENTS_CURRENT_180 = 0x100000;
        public const uint CONTENTS_CURRENT_270 = 0x200000;
        public const uint CONTENTS_CURRENT_UP = 0x400000;
        public const uint CONTENTS_CURRENT_DOWN = 0x800000;
        public const uint CONTENTS_ORIGIN = 0x1000000;   /// removed before bsping an entity
        public const uint CONTENTS_MONSTER = 0x2000000;   /// should never be on a brush, only in game
        public const uint CONTENTS_DEBRIS = 0x4000000;
        public const uint CONTENTS_DETAIL = 0x8000000;   /// brushes to be added after vis leafs
        public const uint CONTENTS_TRANSLUCENT = 0x10000000;  /// uint set if any surface has trans
        public const uint CONTENTS_LADDER = 0x20000000;
        public const uint CONTENTS_HITBOX = 0x40000000;  /// use accurate hitboxes on trace

        /// everyhting
        public const uint MASK_ALL = 0xFFFFFFFF;
        /// everything that is normally solid
        public const uint MASK_SOLID = CONTENTS_SOLID | CONTENTS_MOVEABLE | CONTENTS_WINDOW | CONTENTS_MONSTER | CONTENTS_GRATE;
        /// everything that blocks player movement
        public const uint MASK_PLAYERSOLID = CONTENTS_SOLID | CONTENTS_MOVEABLE | CONTENTS_PLAYERCLIP | CONTENTS_WINDOW | CONTENTS_MONSTER | CONTENTS_GRATE;
        /// blocks npc movement
        public const uint MASK_NPCSOLID = CONTENTS_SOLID | CONTENTS_MOVEABLE | CONTENTS_MONSTERCLIP | CONTENTS_WINDOW | CONTENTS_MONSTER | CONTENTS_GRATE;
        /// water physics in these contents
        public const uint MASK_WATER = CONTENTS_WATER | CONTENTS_MOVEABLE | CONTENTS_SLIME;
        /// everything that blocks line of sight
        public const uint MASK_OPAQUE = CONTENTS_SOLID | CONTENTS_MOVEABLE | CONTENTS_SLIME | CONTENTS_OPAQUE;
        /// bullets see these as solid
        public const uint MASK_SHOT = CONTENTS_SOLID | CONTENTS_MOVEABLE | CONTENTS_MONSTER | CONTENTS_WINDOW | CONTENTS_DEBRIS | CONTENTS_HITBOX;
        /// non-raycasted weapons see this as solid (includes grates)
        public const uint MASK_SHOT_HULL = CONTENTS_SOLID | CONTENTS_MOVEABLE | CONTENTS_MONSTER | CONTENTS_WINDOW | CONTENTS_DEBRIS | CONTENTS_GRATE;
        /// everything normally solid, except monsters (world+brush only)
        public const uint MASK_SOLID_BRUSHONLY = CONTENTS_SOLID | CONTENTS_MOVEABLE | CONTENTS_WINDOW | CONTENTS_GRATE;
        /// everything normally solid for player movement, except monsters (world+brush only)
        public const uint MASK_PLAYERSOLID_BRUSHONLY = CONTENTS_SOLID | CONTENTS_MOVEABLE | CONTENTS_WINDOW | CONTENTS_PLAYERCLIP | CONTENTS_GRATE;
        /// everything normally solid for npc movement, except monsters (world+brush only)
        public const uint MASK_NPCSOLID_BRUSHONLY = CONTENTS_SOLID | CONTENTS_MOVEABLE | CONTENTS_WINDOW | CONTENTS_MONSTERCLIP | CONTENTS_GRATE;
        /// just the world, used for route rebuilding
        public uint MASK_NPCWORLDpublic = CONTENTS_SOLID | CONTENTS_WINDOW | CONTENTS_MONSTERCLIP | CONTENTS_GRATE;
        public const uint MASK_CURRENT = CONTENTS_CURRENT_0 | CONTENTS_CURRENT_90 | CONTENTS_CURRENT_180 | CONTENTS_CURRENT_270 | CONTENTS_CURRENT_UP | CONTENTS_CURRENT_DOWN;
        public const uint MASK_DEADSOLID = CONTENTS_SOLID | CONTENTS_PLAYERCLIP | CONTENTS_WINDOW | CONTENTS_GRATE;
    }
}
