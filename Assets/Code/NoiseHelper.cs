using UnityEngine;

public static class NoiseHelper
{
    public static float Perlin(float x, float y, float frequency, int seed)
    {
        return Mathf.PerlinNoise(x * frequency + 823 * seed, y * frequency + 234 * seed);
    }

    public static float FBM(float x, float y, int octaves, float frequency, int seed)
    {
        var result = 0f;
        for (int i = octaves - 1; i >= 0; i--)
        {
            result += Mathf.Pow(.5f, i) * Perlin(x, y, frequency * Mathf.Pow(2, i), seed + i);
        }

        return result;
    }
}