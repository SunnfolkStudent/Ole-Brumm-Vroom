using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace From_Other_Projects.Koi_PunchVR
{
    public class PlatformObjectPool : MonoBehaviour
    {
        [SerializeField] private ObjectScrub[] platformTypes;
        private static List<PlatformPool> _platformPools;
        private static Transform _platformContainer;
        
        #region ---Initialization---
        private void Awake()
        {
            _platformContainer = transform.GetChild(0);
            _platformPools = new List<PlatformPool>();
            foreach (var platformScrub in platformTypes)
            {
                _platformPools.Add(new PlatformPool(platformScrub));
            }
            // PlatformSpawnType.InitializePlatformSpawnTypes(_platformPools);
        }
        #endregion
        
        #region ---PlatformPoolInfo---
        public class PlatformPool
        {
            public readonly PlatformRecord PlatformRecord;
            public readonly List<Platform> Platforms;
            public float Weight;
            public int TimesSpawned;
            
            public PlatformPool(ObjectScrub objectScrub)
            {
                PlatformRecord = new PlatformRecord(objectScrub);
                Platforms = new List<Platform>();
                AddMultiplePlatformsToPool(objectScrub.initialAmountInPool, this);
                Weight = objectScrub.weightInRandomTable;
                TimesSpawned = 0;
            }
        }
        
        #region >>>---FishRecord---
        public record PlatformRecord
        {
            public readonly ObjectScrub ObjectScrub;
            public readonly PlatformRecordChild[] Children;
            
            public PlatformRecord(ObjectScrub objectScrub)
            {
                ObjectScrub = objectScrub;
            }
        }
        
        public struct PlatformRecordChild
        {
            public readonly Transform InitialTransform;
            public readonly Rigidbody Rigidbody;
            
            public PlatformRecordChild(Transform transform)
            {
                InitialTransform = transform;
                Rigidbody = transform.gameObject.GetComponent<Rigidbody>();
            }
        }
        #endregion
        
        #region >>>---Platform---
        public class Platform
        {
            public readonly PlatformPool PlatformPool;
            public readonly GameObject ParentGameObject;
            public readonly Child[] Children;
            
            public Platform(PlatformPool platformPool)
            {
                PlatformPool = platformPool;
                ParentGameObject = Instantiate(platformPool.PlatformRecord.ObjectScrub.objectPrefab, _platformContainer);
                ParentGameObject.SetActive(false);
                Children = ParentGameObject.GetComponentsInChildren<Transform>().Select(transform1 => new Child(transform1)).ToArray();
            }
        }
        
        public class Child
        {
            public readonly Transform Transform;
            public readonly Rigidbody Rigidbody;
            
            public Child(Transform transform)
            {
                Transform = transform;
                Rigidbody = transform.gameObject.GetComponent<Rigidbody>();
            }
        }
        #endregion
        #endregion
        
        #region ---PoolInteraction---
        public static Platform GetPooledObject(PlatformPool platformPool)
        {
            var availablePlatformsInPool = platformPool.Platforms.Where(platform => !platform.ParentGameObject.activeInHierarchy).ToArray();
            if (availablePlatformsInPool.Length < 2) AddPlatformsInPool(platformPool);
            return availablePlatformsInPool[0];
        }
        
        public static void DespawnPlatform(Platform platform)
        {
            ResetPropertiesOfPlatformInPool(platform);
            platform.ParentGameObject.SetActive(false);
        }

        private static void ResetPropertiesOfPlatformInPool(Platform platform)
        {
            for (var i = 0; i < platform.Children.Length; i++)
            {
                platform.Children[i].Transform.position = platform.PlatformPool.PlatformRecord.Children[i].InitialTransform.position;
                platform.Children[i].Transform.rotation = platform.PlatformPool.PlatformRecord.Children[i].InitialTransform.rotation;
                platform.Children[i].Transform.localScale = platform.PlatformPool.PlatformRecord.Children[i].InitialTransform.localScale;
            }
        }
        
        private static void AddMultiplePlatformsToPool(int amount, PlatformPool platformPool)
        {
            for(var i = 0; i < amount; i++)
            {
                AddPlatformsInPool(platformPool);
            }
        }

        private static void AddPlatformsInPool(PlatformPool platformPool)
        {
            platformPool.Platforms.Add(new Platform(platformPool));
        }
        #endregion
    }
}
