using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // TODO: Create a List with the Platforms you can spawn.
    // TODO: Also bring up the obstacle you can spawn.
    // TODO: Create an Array with the spawnAreas you can spawn from.
    
    // TODO: Create a system that chooses a spawnArea (foreach loops), and spawns there.
        // TODO: The spawnArea itself has manually selected chances of spawning different things.
        
        private static List<SpawnArea> _availableSpawnAreas;
        private SpawnArea[] _previousNeighbouringAreas;
        private List<float> _weightDistributedToNeighbours;
        
        private class SpawnArea
        {
            public GameObject GameObject;
            // public float Weight = 100;
            // public int TimesSpawned;
        }
        
        #region ---Initialization---
        public void Awake()
        {
            _availableSpawnAreas = new List<SpawnArea>();
            AddSpawnAreasToSpawnAreaList();
        }
        
        private static void AddSpawnAreasToSpawnAreaList()
        {
            var spawnArea = GameObject.FindGameObjectsWithTag("SpawnArea");
            
            foreach (var obj in spawnArea)
            {
                var f = new SpawnArea { GameObject = obj };
                _availableSpawnAreas.Add(f);
            }
        }
        #endregion
        
        #region ---GetSpawnPosition---
        public Vector3 GetNextSpawnPosition()
        {
            var spawnArea = PickSpawnArea(_availableSpawnAreas);
            return spawnArea.GameObject.transform.position;
        }

        private static Vector3 RandomOffset(float offsetMax)
        {
            return new Vector3(Random.Range(-offsetMax, offsetMax), 0, Random.Range(-offsetMax, offsetMax));
        }
        #endregion
        
        private SpawnArea PickSpawnArea(List<SpawnArea> availableSpawnAreas)
        {
            /*var totalWeight = availableSpawnAreas.Sum(area => area.Weight);
            if (totalWeight == 0)
            {
                ResetSpawnAreaWeights(availableSpawnAreas);
            }*/
            var rnd = Random.Range(0, 4);
            
            float sum = 0;
            foreach (var area in availableSpawnAreas)
            {
                // sum += area.Weight;
                if (sum >= rnd)
                {
                    return area;  
                }
            }
            return null;
        }

        /*private void NewProbabilities(IReadOnlyCollection<SpawnArea> availableSpawnAreas, SpawnArea area)
        {
            NewProbabilitiesForPickedArea(area);
        }
        
        private void NewProbabilitiesForPickedArea(SpawnArea spawnArea)
        {
            spawnArea.TimesSpawned++;
            spawnArea.Weight *= weightLossOfPickedArea;

            if (spawnArea.TimesSpawned >= maxPickRate)
            {
                spawnArea.Weight = 0;
            }
        }*/
    }

