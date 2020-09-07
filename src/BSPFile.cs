using System;
using System.IO;
using System.Linq;

namespace BetterExternalRaytrace
{
	//Original: Thanks Zat for this https://github.com/BigMo/ZMH5_Helios/tree/master/%5BZMH5%5D%20Helios/CSGO/BSP
	//Modified: by TheRioMiner (v0.2)
	public class BSPFile
	{
		public const string _bspStr = "[BSP]";

		public dheader_t m_BSPHeader;
		public mvertex_t[] m_Vertexes;
		public cplane_t[] m_Planes;
		public dedge_t[] m_Edges;
		public int[] m_Surfedges;
		public dleaf_t[] m_Leaves;
		public snode_t[] m_Nodes;
		public dface_t[] m_Surfaces;
		public texinfo_t[] m_Texinfos;
		public dbrush_t[] m_Brushes;
		public dbrushside_t[] m_Brushsides;
		public ushort[] m_Leaffaces;
		public ushort[] m_Leafbrushes;
		public Polygon[] m_Polygons;
		public dface_t[] m_OriginalFaces;
		public long m_PlanesAddress;
		//private string m_EntitiesASCII;
		public string[] m_StaticPropsModelNames;
		public StaticPropLump_t[] m_StaticProps;
		public dgamelump_t[] m_GameLumps;



		public BSPFile(Stream stream)
        {
			Parse(stream);
		}

		public BSPFile(byte[] bytes)
		{
			using (var ms = new MemoryStream(bytes))
				Parse(ms);
		}



		#region METHODS

		private void Parse(Stream str)
		{
			//Read header
			m_BSPHeader = str.Read<dheader_t>();

			//Check bsp signature
			if (m_BSPHeader.m_Ident != 0x50534256)
				throw new Exception($"{_bspStr} Invalid signature!");

			//TODO: Implement version-check!
			if (m_BSPHeader.m_Version != BSPFlags.BSPVERSION)
			{
				//Invalid version, oops
				Console.WriteLine($"{_bspStr} Unknown version: {m_BSPHeader.m_Version}. Trying to parse it anyway.");
			}

			//ParseAndCheckLumpData(str, eLumpIndex.LUMP_VERTEXES, out m_Vertexes, BSPFlags.MAX_SURFINFO_VERTS, "Vertexes");
			m_Vertexes = ParseLumpData<mvertex_t>(str, eLumpIndex.LUMP_VERTEXES);
			ParsePlanes(str);
			m_Edges = ParseLumpData<dedge_t>(str, eLumpIndex.LUMP_EDGES);
			m_Surfedges = ParseLumpData<int>(str, eLumpIndex.LUMP_SURFEDGES);
			m_Leaves = ParseLumpData<dleaf_t>(str, eLumpIndex.LUMP_LEAFS);
			ParseNodes(str);
			m_Surfaces = ParseLumpData<dface_t>(str, eLumpIndex.LUMP_FACES);
			m_OriginalFaces = ParseLumpData<dface_t>(str, eLumpIndex.LUMP_ORIGINALFACES);
			m_Texinfos = ParseLumpData<texinfo_t>(str, eLumpIndex.LUMP_TEXINFO);
			m_Brushes = ParseLumpData<dbrush_t>(str, eLumpIndex.LUMP_BRUSHES);
			m_Brushsides = ParseLumpData<dbrushside_t>(str, eLumpIndex.LUMP_BRUSHSIDES);
			//m_Leaffaces = ParseLumpData<ushort>(str, eLumpIndex.LUMP_LEAFFACES);
			//m_Leafbrushes = ParseLumpData<ushort>(str, eLumpIndex.LUMP_LEAFBRUSHES);
			ParseAndCheckLumpData(str, eLumpIndex.LUMP_LEAFFACES, out m_Leaffaces, BSPFlags.MAX_MAP_LEAFFACES, "leaffaces");
			ParseAndCheckLumpData(str, eLumpIndex.LUMP_LEAFBRUSHES, out m_Leafbrushes, BSPFlags.MAX_MAP_LEAFBRUSHES, "leafbrushes");

			//ParsePolygons();
			ParseEntities(str);
			ParseStaticProps(str);
		}

		private void ParseStaticProps(Stream str)
		{
			var gameLump = m_BSPHeader.m_Lumps[(int)eLumpIndex.LUMP_GAME_LUMP];
			var gameLumpHeader = str.Read<dgamelumpheader_t>(gameLump.m_Fileofs);
			m_GameLumps = new dgamelump_t[gameLumpHeader.m_LumpCount];
			for (var i = 0; i < m_GameLumps.Length; i++)
				m_GameLumps[i] = str.Read<dgamelump_t>();

			const int mainLumpId = 1936749168;
			if (m_GameLumps.Any(x=>x.m_Id == mainLumpId))
			{
				var staticPropsGameLump = str.Read<StaticPropDictLump_t>(m_GameLumps.First(x => x.m_Id == mainLumpId).m_FileOfs);
				//Read names
				m_StaticPropsModelNames = str.ReadArray<StaticPropDictLumpName>(staticPropsGameLump.m_DictEntries).Select(x => x.m_Name).ToArray();
				//Read leaves
				var staticPropLeafLumps = str.Read<StaticPropLeafLump_t>();
				var leaves = str.ReadArray<ushort>(staticPropLeafLumps.m_LeafEntries);
				var numProps = str.Read<int>();
				m_StaticProps = str.ReadArray<StaticPropLump_t>(numProps);
			}
		}

		private void ParseEntities(Stream str)
		{
			var entitiesLump = m_BSPHeader.m_Lumps[(int)eLumpIndex.LUMP_ENTITIES];
			//var data = new byte[entitiesLump.m_Filelen];
			//str.Position = entitiesLump.m_Fileofs;
			//str.Read(data, 0, data.Length);
			str.Position = entitiesLump.m_Fileofs + entitiesLump.m_Filelen; //Just skip

			//m_EntitiesASCII = Encoding.ASCII.GetString(data);
			//File.WriteAllText("map_entities.txt", m_EntitiesASCII);
		}

		//private void ParsePolygons()
		//{
		//    var polygons = new List<Polygon>();
		//    foreach (var surface in m_Surfaces)
		//    {
		//        var first_edge = surface.m_Firstedge;
		//        var num_edges = surface.m_Numedges;

		//        if (num_edges < 3 || num_edges > BSPFlags.MAX_SURFINFO_VERTS)
		//            continue;

		//        if (surface.m_Texinfo <= 0)
		//            continue;

		//        var poly = new Polygon();
		//        for (var i = 0; i < num_edges; i++)
		//        {
		//            var edge_index = m_Surfedges[first_edge + 1];
		//            if (edge_index >= 0)
		//                poly.m_Verts[i] = m_Vertexes[m_Edges[edge_index].m_V[0]].m_Position;
		//            else
		//                poly.m_Verts[i] = m_Vertexes[m_Edges[-edge_index].m_V[1]].m_Position;
		//        }

		//        poly.m_nVerts = num_edges;
		//        poly.m_Plane = new VPlane(m_Planes[surface.m_Planenum].m_Normal,
		//            m_Planes[surface.m_Planenum].m_Distance);
		//        polygons.Add(poly);
		//    }

		//    m_Polygons = polygons.ToArray();
		//}

		private void ParsePlanes(Stream str)
		{
			m_PlanesAddress = m_BSPHeader.m_Lumps[(int)eLumpIndex.LUMP_PLANES].m_Fileofs;
			var planes = ParseLumpData<dplane_t>(str, eLumpIndex.LUMP_PLANES);
			int plane_bits;
			m_Planes = new cplane_t[planes.Length];

			for (var i = 0; i < planes.Length; i++)
			{
				var op = new cplane_t();
				var ip = planes[i];

				plane_bits = 0;
				for (var j = 0; j < 3; j++)
				{
					op.m_Normal[j] = ip.m_Normal[j];
					if (op.m_Normal[j] < 0f) plane_bits |= 1 << j;
				}

				op.m_Distance = ip.m_Distance;
				op.m_Type = ip.m_Type;
				op.m_SignBits = (byte)plane_bits;

				m_Planes[i] = op;
			}
		}

		private void ParseNodes(Stream str)
		{
			var nodes = ParseLumpData<dnode_t>(str, eLumpIndex.LUMP_NODES);
			m_Nodes = new snode_t[nodes.Length];

			for (var i = 0; i < m_Nodes.Length; i++)
			{
				var op = new snode_t(0);
				var ip = nodes[i];

				Array.Copy(ip.m_Mins, op.m_Mins, ip.m_Mins.Length);
				Array.Copy(ip.m_Maxs, op.m_Mins, ip.m_Maxs.Length);
				op.m_Planenum = ip.m_Planenum;
				op.m_pPlane = ip.m_Planenum;
				op.m_Firstface = ip.m_Firstface;
				op.m_Numfaces = ip.m_Numfaces;

				for (var j = 0; j < 2; j++)
				{
					var child_index = ip.m_Children[j];
					op.m_Children[j] = child_index;

					if (child_index >= 0)
					{
						op.m_LeafChildren = 0;
						op.m_NodeChildren = child_index;
					}
					else
					{
						op.m_LeafChildren = -(child_index + 1);
						op.m_NodeChildren = 0;
					}
				}

				m_Nodes[i] = op;
			}
		}

		private void ParseAndCheckLumpData<T>(Stream str, eLumpIndex index, out T[] data, int max, string name) where T : struct
		{
			data = ParseLumpData<T>(str, index);
			if (data.Length > max)
				throw new Exception($"{_bspStr} {name} has too many entries!");
			if (data.Length == 0)
				throw new Exception($"{_bspStr} {name} has no entries!");
		}

		private T[] ParseLumpData<T>(Stream str, eLumpIndex index) where T : struct
		{
			var lump = m_BSPHeader.m_Lumps[(int)index];
			if (lump.m_FourCC != 0)
				throw new Exception($"{_bspStr} {index} LZMA!");
				
			var count = lump.m_Filelen / FastSize<T>.SizeSigned;
			if (count <= 0)
				return new T[0];

			return str.ReadArray<T>(lump.m_Fileofs, count);
		}

		#endregion


		#region RAYTRACE

		private const ContentsFlag SOLID_LEAF_FLAG = ContentsFlag.CONTENTS_SOLID | ContentsFlag.CONTENTS_DETAIL;

		private unsafe int GetLeafIndexForPoint(Vector3 point)
		{
			int node = 0;
			while (node >= 0)
			{
				var pNode = m_Nodes[node];
				var pPlane = m_Planes[pNode.m_Planenum];
				float d = (Vector3.Dot(point, pPlane.m_Normal) - pPlane.m_Distance);
				if (d > 0)
					node = pNode.m_Children[0];
				else
					node = pNode.m_Children[1];
			}

			return (-node - 1);
		}

		//Optimized raytrace algorithm by TheRioMiner
		public bool RayTrace(Vector3 start, Vector3 end, out Vector3 hit)
		{
			var point = start;
			var direction = end - start;
			direction *= 1f / direction.Length(); //Normalize direction
			var maxDistance = Vector3.Distance(start, end);

			while (Vector3.Distance(start, point) < maxDistance)
			{
				//Get leaf index for point
				int currLeafIndex = GetLeafIndexForPoint(point);

				//Get leaf by index and check that its not contains solid
				dleaf_t leaf = m_Leaves[currLeafIndex];
				if (leaf.m_Area != -1)
				{
					if ((leaf.m_Contents & SOLID_LEAF_FLAG) != 0)
					{
						//Hitted in solid!
						hit = point;
						return true;
					}
				}

				//Fast exit from leaf by our direction
				int leafIndex = 0;
				Vector3 addDistance = direction;
				do
				{
					point += addDistance;
					addDistance += addDistance; //Geometric progression!
					leafIndex = GetLeafIndexForPoint(point);
				}
				while (leafIndex == currLeafIndex);

				//Go back to find border of our leaf
				int backPorchLastAccessLeafIndex = currLeafIndex;
				Vector3 newPoint = point;
				Vector3 oldPoint = point - (addDistance / 2f);
				Vector3 pointsDiff = newPoint - oldPoint;
				Vector3 halfTestPoint;
				do
				{
					//Get half point from two points
					halfTestPoint = oldPoint + (pointsDiff / 2f);

					//Get leaf index by our half point
					backPorchLastAccessLeafIndex = GetLeafIndexForPoint(halfTestPoint);
					if (backPorchLastAccessLeafIndex == currLeafIndex)
						oldPoint = halfTestPoint; //We need go furtner (go into new leaf)
					else
						newPoint = halfTestPoint; //We need go back (go into our leaf)

					//Calc two points difference
					pointsDiff = (newPoint - oldPoint);
				}
				while (pointsDiff.LengthSquared() > 0.5f); //(Math.Abs(pointsDiff.X) > Math.Abs(direction.X / 4f)) //More fast but not accurate (+-1 unit)

				//Border of our leaf is finded!
				point = halfTestPoint;

				//We still in our leaf?
				if (backPorchLastAccessLeafIndex == currLeafIndex)
				{
					//We need out from old leaf and go into new leaf
					addDistance = direction / 2f;
					do
					{
						point += addDistance;
						leafIndex = GetLeafIndexForPoint(point);
					}
					while (leafIndex == currLeafIndex);
				}
			}

			//Not hit!
			hit = end;
			return false;
		}

		#endregion
	}
}