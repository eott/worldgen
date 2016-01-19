using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worldgen
{
    class Step
    {
        public static World DetermineLandWater(World world)
        {
            var noise = new OpenSimplexNoise();
            float scale = 50;

            for (int x = 0; x < world.width; x++)
            {
                for (int y = 0; y < world.height; y++)
                {
                    if (noise.Evaluate(x / scale, y / scale) >= 0.15f)
                    {
                        world.SetBiomeAt(x, y, BiomeType.LAND);
                    }
                    else
                    {
                        world.SetBiomeAt(x, y, BiomeType.WATER);
                    }
                }
            }

            return world;
        }

        public static World CalculateWaterDistance(World world)
        {
            for (int x = 0; x < world.width; x++)
            {
                for (int y = 0; y < world.height; y++)
                {
                    int seek = 10;
                    int dist = seek;
                    BiomeType type = world.GetBiomeAt(x, y).type;

                    int fromX = Math.Max(0, x - seek);
                    int toX = Math.Min(world.width - 1, x + seek);
                    int fromY = Math.Max(0, y - seek);
                    int toY = Math.Min(world.height - 1, y + seek);

                    for (int dx = fromX; dx <= toX; dx++)
                    {
                        for (int dy = fromY; dy <= toY; dy++)
                        {
                            if (world.GetBiomeAt(dx, dy).type != type)
                            {
                                dist = Math.Min(dist, Math.Abs(x - dx) + Math.Abs(y - dy));
                            }
                        }
                    }

                    world.SetCustomMap(x, y, Map.WATER_DISTANCE, (float)dist);
                }
            }

            return world;
        }

        public static World CreateHeightMap(World world)
        {
            float seaLevel = 0;

            for (int x = 0; x < world.width; x++)
            {
                for (int y = 0; y < world.height; y++)
                {
                    if (world.GetBiomeAt(x, y).type == BiomeType.LAND)
                    {
                        world.SetHeightMapAt(x, y, seaLevel + 0.1f * world.GetWaterDistanceAt(x, y));
                    }
                    else
                    {
                        world.SetHeightMapAt(x, y, seaLevel - 0.1f * world.GetWaterDistanceAt(x, y));
                    }
                }
            }

            return world;
        }
    }
}
