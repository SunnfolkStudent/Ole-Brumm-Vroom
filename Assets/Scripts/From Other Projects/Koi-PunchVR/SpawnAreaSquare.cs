using UnityEngine;

namespace From_Other_Projects.Koi_PunchVR
{
    // Uncomment the below if you need to place SpawnAreas, but otherwise keep commented, cuz it gets performance-heavy.
    public class SpawnAreaSquare : MonoBehaviour
    {
        [Range(0, 20)] public float spawnAreaRadius;
        private readonly int _segments = 180;
        
        private void Start()
        {
            CreatePointsSpawnArea();
        }
        
        private void Update()
        {
            CreatePointsSpawnArea();
        }
        
        private void CreatePointsSpawnArea()
        {
            var angle = 20f;
            
            for (var i = 0; i < (_segments + 1); i++)
            {
                var x = Mathf.Sin (Mathf.Deg2Rad * angle) * spawnAreaRadius;
                var y = Mathf.Cos (Mathf.Deg2Rad * angle) * spawnAreaRadius;
                
                angle += (360f / _segments);
            }
        }
    }
}