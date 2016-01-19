using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worldgen
{
    enum BiomeType
    {
        // Placeholder types used in further specializing biomes by conditions
        WATER,
        LAND
    }

    class Biome
    {
        public BiomeType type { get; set; }
        public Biome (BiomeType type)
        {
            this.type = type;
        }

        public static Dictionary<BiomeType, Biome> GetPrototypes()
        {
            var dict = new Dictionary<BiomeType, Biome>();
            dict.Add(BiomeType.LAND, new Biome(BiomeType.LAND));
            dict.Add(BiomeType.WATER, new Biome(BiomeType.WATER));
            return dict;
        }

        public Color GetColor()
        {
            return this.GetColor(0f);
        }

        public Color GetColor(float height)
        {
            switch (this.type)
            {
                case BiomeType.LAND:
                    return Color.FromArgb(255, 0, Math.Max(0, Math.Min(255, (int)(height * 150 + 70))), 0);
                case BiomeType.WATER:
                    return Color.FromArgb(255, 0, 0, Math.Min(255, Math.Max(0, (int)(255 + height * 255))));
                default:
                    return Color.FromArgb(255, 0, 0, 0);
            }
        }
    }
}
