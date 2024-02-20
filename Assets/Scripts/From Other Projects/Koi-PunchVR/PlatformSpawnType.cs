using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace From_Other_Projects.Koi_PunchVR
{
    public static class PlatformSpawnType
    {
        // TODO: make not static
        private static PlatformObjectPool.PlatformPool[] _fishPoolTypes;
        private const float WeightLostFromPicked = 0.5f;
        private const int MaxPickAmount = 5;

        #region ---Initialization---
        public static void InitializePlatformSpawnTypes(List<PlatformObjectPool.PlatformPool> fishPools)
        {
            _fishPoolTypes = fishPools.ToArray();
        }
        #endregion

        #region ---GetFish---
        public static PlatformObjectPool.Platform GetNextFish()
        {
            return PlatformObjectPool.GetPooledObject(PickFishType(_fishPoolTypes));
        }
        #endregion

        #region ---RandomWeightedChoice---
        private static PlatformObjectPool.PlatformPool PickFishType(IEnumerable<PlatformObjectPool.PlatformPool> fishTypes)
        {
            var fishPools = fishTypes.ToArray();
            var totalWeight = fishPools.Sum(fishPool => fishPool.Weight);

            if (totalWeight == 0)
            {
                ResetFishTypeWeights(fishPools);   
            }
            
            var rnd = Random.Range(0, totalWeight);
            
            float sum = 0;
            foreach (var fishPool in fishPools)
            {
                sum += fishPool.Weight;
                if (sum < rnd) continue;
                NewProbabilities(fishPools, fishPool);
                return fishPool;
            }
            
            return null;
        }
        
        private static void ResetFishTypeWeights(PlatformObjectPool.PlatformPool[] fishTypes)
        {
            foreach (var differentFish in fishTypes)
            {
                differentFish.Weight = 100;
                differentFish.TimesSpawned = 0;
            }
        }
        
        private static void NewProbabilities(IReadOnlyCollection<PlatformObjectPool.PlatformPool> fishPools, PlatformObjectPool.PlatformPool platformPool)
        {
            platformPool.TimesSpawned++;
            platformPool.Weight *= WeightLostFromPicked;

            if (platformPool.TimesSpawned >= MaxPickAmount)
            {
                platformPool.Weight = 0;
                return;
            }
            
            var weightToDistribute = platformPool.Weight * (1 - WeightLostFromPicked);
            var weightForOthers = weightToDistribute / fishPools.Count;
            
            foreach (var pool in fishPools)
            {
                pool.Weight += weightForOthers;
            }
        }
        #endregion
    }
}
