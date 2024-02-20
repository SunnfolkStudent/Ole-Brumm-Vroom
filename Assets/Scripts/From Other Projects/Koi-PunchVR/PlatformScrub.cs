using UnityEngine;

namespace From_Other_Projects.Koi_PunchVR
{
    [CreateAssetMenu(fileName = "NewPlatform", menuName = "Platform", order = 1)]
    public class PlatformScrub : ScriptableObject
    {
        [Header("Platform Properties")] 
        public GameObject prefab;
        public int initialAmountInPool = 5;
        public float weightInRandomTable = 100;
        
        [Header("Despawning")]
        public float despawnDelay = 2.5f;
        public float despawnTime = 10;
        public float despawnAltitude = -5;
    }
}
