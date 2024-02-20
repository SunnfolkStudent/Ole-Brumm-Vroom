using System.Collections;
using UnityEngine;

namespace From_Other_Projects.Koi_PunchVR
{
    public class PlatformSpawnTimeManager : MonoBehaviour
    {
        private static bool _isSpawningPlatforms;
        
        [Header("PlatformSpawnFrequencyTimer")]
        [SerializeField] private float maxSpawnRate = 1.5f;
        [SerializeField] private float minSpawnRate = 0.5f;
        [SerializeField] private float timeToMaxSpawnRate = 20f;
        [SerializeField] private AnimationCurve animationCurve;
        
        #region ---Initialization---
        private void Awake()
        {
            /*EventManager.FishSpawning += StartSpawning;
            EventManager.FishSpawningAtMaxRate += StartSpawningAtMaxRate;
            EventManager.StopFishSpawning += StopSpawning;*/
        }
        #endregion
        
        #region ---PlatformSpawnFrequencyControls---
        [ContextMenu("StartSpawning")]
        private void StartSpawning()
        {
            StartCoroutine(SpawnPlatform());
        }
        
        [ContextMenu("StartSpawningAtMaxRate")]
        private void StartSpawningAtMaxRate()
        {
            StartCoroutine(SpawnPlatformMaxRate());
        }

        [ContextMenu("StopSpawning")]
        private void StopSpawning()
        {
            _isSpawningPlatforms = false;
        }
        #endregion
        
        #region ---PlatformFrequencyTimer---
        private IEnumerator SpawnPlatform()
        {
            _isSpawningPlatforms = true;
            var minSpawnTime = 1 / maxSpawnRate;
            var maxSpawnTime = 1 / minSpawnRate;
            while (_isSpawningPlatforms)
            {
                var nextSpawnTime = Mathf.Lerp(minSpawnTime, maxSpawnTime, (animationCurve.Evaluate(Time.time / timeToMaxSpawnRate)));
                yield return new WaitForSeconds(nextSpawnTime);
                // EventManager.SpawnFish.Invoke();
            }
        }
        
        private IEnumerator SpawnPlatformMaxRate()
        {
            var spawnTime = 1 / maxSpawnRate;
            while (_isSpawningPlatforms)
            {
                yield return new WaitForSeconds(spawnTime);
                // EventManager.SpawnFish.Invoke();
            }
        }
        #endregion
    }
}