using System.Collections.Generic;
using System.Linq;
using From_Other_Projects.Koi_PunchVR;
using UnityEngine;

public static class PlatformSpawnType
{
    private static PlatformObjectPool.PlatformPool[] _platformPoolTypes;
    private const float WeightLostFromPicked = 0.5f;
    private const int MaxPickAmount = 5;

    #region ---Initialization---
    public static void InitializePlatformSpawnTypes(List<PlatformObjectPool.PlatformPool> platformPools)
    {
        _platformPoolTypes = platformPools.ToArray();
    }
    #endregion

    #region ---GetPlatform---
    public static PlatformObjectPool.Platform GetNextPlatform()
    {
        return PlatformObjectPool.GetPooledObject(PickPlatformType(_platformPoolTypes));
    }
    #endregion

    #region ---RandomWeightedChoice---
    private static PlatformObjectPool.PlatformPool PickPlatformType(IEnumerable<PlatformObjectPool.PlatformPool> platformTypes)
    {
        var platformPools = platformTypes.ToArray();
        var totalWeight = platformPools.Sum(platformPool => platformPool.Weight);

        if (totalWeight == 0)
        {
            ResetPlatformTypeWeights(platformPools);   
        }
            
        var rnd = Random.Range(0, totalWeight);
            
        float sum = 0;
        foreach (var platformPool in platformPools)
        {
            sum += platformPool.Weight;
            if (sum < rnd) continue;
            NewProbabilities(platformPools, platformPool);
            return platformPool;
        }
            
        return null;
    }
        
    private static void ResetPlatformTypeWeights(PlatformObjectPool.PlatformPool[] platformTypes)
    {
        foreach (var differentPlatforms in platformTypes)
        {
            differentPlatforms.Weight = 100;
            differentPlatforms.TimesSpawned = 0;
        }
    }
        
    private static void NewProbabilities(IReadOnlyCollection<PlatformObjectPool.PlatformPool> platformPools, PlatformObjectPool.PlatformPool platformPool)
    {
        platformPool.TimesSpawned++;
        platformPool.Weight *= WeightLostFromPicked;

        if (platformPool.TimesSpawned >= MaxPickAmount)
        {
            platformPool.Weight = 0;
            return;
        }
            
        var weightToDistribute = platformPool.Weight * (1 - WeightLostFromPicked);
        var weightForOthers = weightToDistribute / platformPools.Count;
            
        foreach (var pool in platformPools)
        {
            pool.Weight += weightForOthers;
        }
    }
    #endregion
}