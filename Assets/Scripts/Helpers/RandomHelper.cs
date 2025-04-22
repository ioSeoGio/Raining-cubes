using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using System;
using UnityEditor;

public static class RandomHelper
{
    private const int MinRandom = 0;
    private const int MaxRandom = 100;

    private readonly static System.Random s_random = new System.Random();

    public static bool IsRandomEventHappened(float chance)
    {
        return GetRandomNumber(MinRandom, MaxRandom + 1) <= chance;
    }

    public static int GetRandomNumber(int min, int max)
    {
        return s_random.Next(min, max);
    }

    public static Vector3 GetRandomPointOnTerrain(Terrain terrain, float yIndent)
    {
        Vector3 terrainSize = terrain.terrainData.size;

        return terrain.transform.position + new Vector3(
            UnityEngine.Random.Range(0, terrainSize.x),
            terrain.GetPosition().y + yIndent,
            UnityEngine.Random.Range(0, terrainSize.z)
        );
    }
}
