using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worldgen
{
    enum Topology
    {
        EUCLIDEAN,
        TORUS
    }

    enum Map
    {
        HEIGHT,
        BIOME_TYPE,
        WATER_DISTANCE
    }

    class World
    {
        public int width { get; set; }

        public int height { get; set; }

        public Topology topology { get; set; }

        protected Dictionary<BiomeType, Biome> biomes;

        protected BiomeType[,] biomeMap;

        protected float[,] heightMap;

        protected Dictionary<Map, float[,]> customMaps;

        public World(int w, int h)
        {
            this.biomes = Biome.GetPrototypes();
            this.biomeMap = new BiomeType[w, h];
            this.heightMap = new float[w, h];
            this.customMaps = new Dictionary<Map, float[,]>();
            this.customMaps[Map.WATER_DISTANCE] = new float[w, h];
        }

        public void Generate(Action<string> logCallback)
        {
            logCallback("Determine land/water...");
            Step.DetermineLandWater(this);
            logCallback(" done\n");

            logCallback("Calculate water distance...");
            Step.CalculateWaterDistance(this);
            logCallback(" done\n");

            logCallback("Create heightmap...");
            Step.CreateHeightMap(this);
            logCallback(" done\n");
        }

        public Biome GetBiomeAt(int x, int y)
        {
            return this.biomes[this.biomeMap[x, y]];
        }

        public World SetBiomeAt(int x, int y, BiomeType type)
        {
            this.biomeMap[x, y] = type;
            return this;
        }

        public World SetHeightMapAt(int x, int y, float h)
        {
            this.heightMap[x, y] = h;
            return this;
        }

        public World SetCustomMap(int x, int y, Map mapName, float h)
        {
            this.customMaps[mapName][x, y] = h;
            return this;
        }

        public float GetWaterDistanceAt(int x, int y)
        {
            return this.customMaps[Map.WATER_DISTANCE][x, y];
        }

        public Color GetColorAt(int x, int y)
        {
            return this.biomes[this.biomeMap[x, y]].GetColor(this.heightMap[x, y]);

        }
    }
}
