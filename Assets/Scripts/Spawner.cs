using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

/*public class PlatformObjectPool : MonoBehaviour
{
    public static PlatformObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject prefab;
    public GameObject pickedSpawnArea;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject platformOrObstacle = SharedInstance.GetPooledObject();
        for(int i = 0; i < amountToPool; i++)
        {
            if (platformOrObstacle != null) 
            {
                platformOrObstacle.transform.position = pickedSpawnArea.transform.position;
                platformOrObstacle.transform.rotation = pickedSpawnArea.transform.rotation;
                platformOrObstacle.SetActive(true);
            }
            platformOrObstacle.SetActive(false);
            pooledObjects.Add(platformOrObstacle);
        }
    }
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}*/

public class Spawner : MonoBehaviour
{
    // TODO: The spawnArea itself has manually selected chances of spawning different things.
    
    [SerializeField] private List<GameObject> pickableObjectsToSpawn;
    //private List<GameObject> _pickableObstaclesToSpawn;
    [SerializeField] private List<Transform> pickableSpawnAreas;

    private Transform _spawnAreaGround;
    public GameObject flyingHoneyObstacle;
    
    [Header("Chance for obstacles to spawn when platforms are spawned. 101 means guaranteed.")]
    [SerializeField] [Range(0, 101)] private int extraObstacleChance = 15;
    
    [SerializeField] private float minSpawnRate = 2f;
    // [SerializeField] private float timeToReachMaxSpawnRate = 150f;
    [SerializeField] private float maxSpawnRate = 10f;
    // [SerializeField] private AnimationCurve animationCurve;
    
    private void Start()
    {
        pickableObjectsToSpawn = new List<GameObject>();
        //_pickableObstaclesToSpawn = new List<GameObject>();
        pickableSpawnAreas = new List<Transform>();
        
        foreach (var platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            pickableObjectsToSpawn.Add(platform);
        }
        foreach (var obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            pickableObjectsToSpawn.Add(obstacle);
            //_pickableObstaclesToSpawn.Add(obstacle);
        }
        foreach (var spawnArea in GameObject.FindGameObjectsWithTag("SpawnArea"))
        {
            var spawnAreaTransform = spawnArea.GetComponent<Transform>();
            pickableSpawnAreas.Add(spawnAreaTransform);
        }
        SpawnTimer();
        SpawnTimerGroundSpawnArea();
    }

    public void PhaseUpdate()
    {
        if (GameManager.phase1Active)
        {
            extraObstacleChance = 15;
            maxSpawnRate = 8;
            minSpawnRate = 2f;
        }
        if (GameManager.phase2Active)
        {
            extraObstacleChance = 30;
            maxSpawnRate = 6.5f;
            minSpawnRate = 1.5f;
        }
        if (GameManager.phase3Active)
        {
            extraObstacleChance = 50;
            maxSpawnRate = 5;
            minSpawnRate = 1.25f;
        }
        if (GameManager.phase4Active)
        {
            extraObstacleChance = 80;
            maxSpawnRate = 3;
            minSpawnRate = 0.75f;
        }
    }
    
    public void SpawnTimer()
    {
        /*var minSpawnTime = 1 / maxSpawnRate;
        var maxSpawnTime = 1 / minSpawnRate;*/

        while (GameManager.IsPlaying)
        {
            // var nextSpawnTime = Mathf.Lerp(maxSpawnTime, minSpawnTime, animationCurve.Evaluate(Time.time / timeToReachMaxSpawnRate));
            // var timeUntilNextSpawn = Random.Range(nextSpawnTime, nextSpawnTime);
            var timeUntilNextSpawn = Random.Range(minSpawnRate, maxSpawnRate);
            StartCoroutine(SpawnObjectInSpawnArea(timeUntilNextSpawn));
        }
    }

    public void SpawnTimerGroundSpawnArea()
    {
        /*var minSpawnTime = 1 / maxSpawnRate;
        var maxSpawnTime = 1 / minSpawnRate;*/

        while (GameManager.IsPlaying)
        {
            // var nextSpawnTime = Mathf.Lerp(maxSpawnTime, minSpawnTime, animationCurve.Evaluate(Time.time / timeToReachMaxSpawnRate));
            // var timeUntilNextSpawn = Random.Range(nextSpawnTime, nextSpawnTime);
            var timeUntilNextSpawn = Random.Range(minSpawnRate / 4, maxSpawnRate / 4);
            StartCoroutine(SpawnInGroundSpawnArea(timeUntilNextSpawn));
        }
    }

    private IEnumerator SpawnObjectInSpawnArea(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Spawning Object...");
        var pickedGameObject = pickableObjectsToSpawn[Random.Range(0, pickableObjectsToSpawn.Count)];
        var pickedSpawnArea = pickableSpawnAreas[Random.Range(0, pickableSpawnAreas.Count)];
        
        var spawnOffsetX = Random.Range(-0.5f, 0.5f);
        var spawnOffsetY = Random.Range(-0.5f, 0.5f);
        var position = pickedSpawnArea.position;
        //var offsetPosition = new Vector3(position.x + spawnOffsetX, position.y + spawnOffsetY, position.z);
        
        if (pickedGameObject == gameObject.CompareTag("Platform"))
        {
            var numberPicked = Random.Range(0, 100);
            
            // If the extraObstacleChance is 80, you have 80% chance of hitting a number below that out of 100.
            if (numberPicked < extraObstacleChance)
            {
                Debug.Log("AirSpawn with offset Obstacle");
                //var pickedObstacle = _pickableObstaclesToSpawn[Random.Range(0, _pickableObjectsToSpawn.Count)];
                Instantiate(pickedGameObject, pickedSpawnArea);
                Instantiate(flyingHoneyObstacle, position, quaternion.identity);
                yield return null;
            }
            else
            {
                Debug.Log("AirSpawn, just platform without offset");
                // Platforms should spawn without offsets, using the below.
                Instantiate(pickedGameObject, pickedSpawnArea);
                yield return null;
            }
        }
        // This code is only accessible to Obstacles without platforms.
        Debug.Log("AirSpawn, just obstacle");
        Instantiate(flyingHoneyObstacle, position,quaternion.identity);
        yield return null;
    }

    private IEnumerator SpawnInGroundSpawnArea(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Spawning on Ground...");
        var pickedGameObject = pickableObjectsToSpawn[Random.Range(0, pickableObjectsToSpawn.Count)];
        
        var spawnOffsetX = Random.Range(-1, 1);
        var spawnOffsetY = Random.Range(0, 1);
        var position = _spawnAreaGround.transform.position;
        var offsetPosition = new Vector3(position.x + spawnOffsetX, position.y + spawnOffsetY, position.z);
        
        var numberPicked = Random.Range(0, 100);
            
        // If the extraObstacleChance is 80, you have 80%/4 (20%) chance of hitting a number below that out of 100.
        if (numberPicked < extraObstacleChance/4)
        {
            Debug.Log("GroundSpawn with offset Obstacle");
            //var pickedObstacle = _pickableObstaclesToSpawn[Random.Range(0, _pickableObjectsToSpawn.Count)];
            Instantiate(pickedGameObject, _spawnAreaGround.transform);
            Instantiate(flyingHoneyObstacle, offsetPosition, quaternion.identity);
            yield return null;
        }
        else
        {
            Debug.Log("GroundSpawn without Offset");
            // Platforms should spawn without offsets, using the below.
            Instantiate(pickedGameObject, _spawnAreaGround.transform);
            yield return null;
        }
    }
    
    #region --- Koi-Punch WeightedTable ---
    
    /*public class SpawnAreaWeightedTable : MonoBehaviour
    {
        private static List<SpawnArea> _availableSpawnAreas;
        private SpawnArea[] _previousNeighbouringAreas;
        private List<float> _weightDistributedToNeighbours;

        #region ---InspectorSettings---
        [Header("Spawn Area Settings")]
        [Tooltip("How often a spawnArea can be picked, before it's disabled")]
        [SerializeField] private int maxPickRate = 5;
        [Header("Neighbour Detection Range (Connected to Neighbour Circle):")]
        [Tooltip("Search distance for a neighbouring spawnArea, 1f being 1 tile")]
        public float neighborDistanceSearchRadius = 7f;
        
        [Header("Percentage of weight lost and distributed to close neighbours")]
        [Range(0f, 1f)] [SerializeField] private float weightLossOfPickedArea = 0.5f;
        // [Range(0f, 1f)] [SerializeField]  private float weightDistributedToNeighbors = 0.75f;
        
        [Header("How long the NeighbourChain can count neighbours in a row:")]
        [SerializeField] private int chainMaxNumber = 2;
        private int _chainCurrentNumber;
        
        [Header("Percentage after picking a neighbour for the n-th time in a row.")]
        [Range(0f, 1f)] [SerializeField] private float firstPercentage;
        [Range(0f, 1f)] [SerializeField] private float secondPercentage;
        [Range(0f, 1f)] [SerializeField] private float thirdPercentage;
        [Range(0f, 1f)] [SerializeField] private float fourthPercentage;
        [Range(0f, 1f)] [SerializeField] private float fifthPercentage;
        [Range(0f, 1f)] [SerializeField] private float sixthPercentage;
        #endregion
        
        private class SpawnArea
        {
            public GameObject GameObject;
            public float Weight = 100;
            public int TimesSpawned;
        }
        
        #region ---Initialization---
        public void Awake()
        {
            _availableSpawnAreas = new List<SpawnArea>();
            AddSpawnAreasToSpawnAreaList();
            InitializeNeighbourPercentages();
        }

        private void InitializeNeighbourPercentages()
        {
            _weightDistributedToNeighbours = new List<float>
            {
                firstPercentage,
                secondPercentage,
                thirdPercentage,
                fourthPercentage,
                fifthPercentage,
                sixthPercentage
            };
        }
        
        private static void AddSpawnAreasToSpawnAreaList()
        {
            var spawnArea = GameObject.FindGameObjectsWithTag("SpawnArea");
            
            foreach (var obj in spawnArea)
            {
                var f = new SpawnArea { GameObject = obj };
                // f.SpawnAreaCircle = f.GameObject.GetComponent<SpawnAreaCircle>();
                _availableSpawnAreas.Add(f);
            }
        }
        #endregion
        
        #region ---GetSpawnPosition---
        public Vector3 GetNextFishSpawnPosition()
        {
            var spawnArea = PickSpawnArea(_availableSpawnAreas);
            return spawnArea.GameObject.transform.position; //+ RandomOffset(spawnArea.SpawnAreaCircle.spawnAreaRadius);
        }

        private static Vector3 RandomOffset(float offsetMax)
        {
            return new Vector3(Random.Range(-offsetMax, offsetMax), 0, Random.Range(-offsetMax, offsetMax));
        }
        #endregion
        
        #region ---RandomWeightedChoice---
        private SpawnArea PickSpawnArea(List<SpawnArea> availableSpawnAreas)
        {
            var totalWeight = availableSpawnAreas.Sum(area => area.Weight);
            if (totalWeight == 0)
            {
                ResetSpawnAreaWeights(availableSpawnAreas);
            }
            var rnd = Random.Range(0, totalWeight);
            
            float sum = 0;
            foreach (var area in availableSpawnAreas)
            {
                sum += area.Weight;
                if (sum >= rnd)
                {
                    NewProbabilities(availableSpawnAreas, area);
                    return area;  
                }
            }
            return null;
        }

        private static void ResetSpawnAreaWeights(List<SpawnArea> availableSpawnAreas)
        {
            foreach (var spawnArea in availableSpawnAreas)
            {
                spawnArea.Weight = 100;
                spawnArea.TimesSpawned = 0;
            }
        }

        private void NewProbabilities(IReadOnlyCollection<SpawnArea> availableSpawnAreas, SpawnArea area)
        {
            if (_previousNeighbouringAreas != null)
            {
                if (_previousNeighbouringAreas.Contains(area)) _chainCurrentNumber++;
                else _chainCurrentNumber = 0;
            }
            if (_chainCurrentNumber > chainMaxNumber) _chainCurrentNumber = chainMaxNumber;
            NeighborsNewProbability(availableSpawnAreas, area, _chainCurrentNumber);
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
        }
        
        private void NeighborsNewProbability(IReadOnlyCollection<SpawnArea> availableSpawnAreas, SpawnArea area, int neighbourChainNumber)
        {
            _previousNeighbouringAreas = null;
            
            var currentNeighbors = availableSpawnAreas.Where(spawnArea =>
                Vector3.Distance(spawnArea.GameObject.transform.position, area.GameObject.transform.position)
                  <= neighborDistanceSearchRadius && spawnArea.TimesSpawned < maxPickRate).ToArray();
            
            // Debug.Log("Neighbours connected to recent spawn:" + (currentNeighbors.Length-1));
            
            if (currentNeighbors.Length !> 0) return;
            
            var weightToDistribute = area.Weight * (1 - weightLossOfPickedArea);
            var weightToNeighbours = weightToDistribute * _weightDistributedToNeighbours[neighbourChainNumber-1] / currentNeighbors.Length;
            var weightForTheRest = weightToDistribute * (1 - _weightDistributedToNeighbours[neighbourChainNumber-1]) 
                                   / (availableSpawnAreas.Count - currentNeighbors.Length);
            
            _previousNeighbouringAreas = currentNeighbors;
            foreach (var spawnArea in _availableSpawnAreas)
            {
                spawnArea.Weight += currentNeighbors.Contains(spawnArea) ? weightToNeighbours : weightForTheRest;
            }
        }
        #endregion
    }*/
    #endregion

    #region --- PlatformSpawnAreas ---

    public class PlatformSpawnAreas : MonoBehaviour
    {
        private static List<SpawnArea> _availableSpawnAreas;
        private SpawnArea[] _previousNeighbouringAreas;
        private List<float> _weightDistributedToNeighbours;

        #region ---InspectorSettings---
        [Header("Spawn Area Settings")]
        [Tooltip("How often a spawnArea can be picked, before it's disabled")]
        [SerializeField] private int maxPickRate = 4;
        [Header("Neighbour Detection Range (Connected to Neighbour Circle):")]
        [Tooltip("Search distance for a neighbouring spawnArea, 1f being 1 tile")]
        public float neighborDistanceSearchRadius = 2f;
        
        [Header("Percentage of weight lost and distributed to close neighbours")]
        [Range(0f, 1f)] [SerializeField] private float weightLossOfPickedArea = 0.5f;
        
        [Header("How long the NeighbourChain can count neighbours in a row:")]
        [SerializeField] private int chainMaxNumber = 5;
        private int _chainCurrentNumber;
        
        [Header("Percentage after picking a neighbour for the n-th time in a row.")]
        [Range(0f, 1f)] [SerializeField] private float firstPercentage;
        [Range(0f, 1f)] [SerializeField] private float secondPercentage;
        [Range(0f, 1f)] [SerializeField] private float thirdPercentage;
        [Range(0f, 1f)] [SerializeField] private float fourthPercentage;
        [Range(0f, 1f)] [SerializeField] private float fifthPercentage;
        [Range(0f, 1f)] [SerializeField] private float sixthPercentage;
        
        #endregion
        
        private class SpawnArea
        {
            public GameObject GameObject;
            public float Weight = 100;
            public int TimesSpawned;
        }
        
        #region ---Initialization---
        public void Awake()
        {
            _availableSpawnAreas = new List<SpawnArea>();
            AddSpawnAreasToSpawnAreaList();
            InitializeNeighbourPercentages();
        }

        private void InitializeNeighbourPercentages()
        {
            _weightDistributedToNeighbours = new List<float>
            {
                firstPercentage,
                secondPercentage,
                thirdPercentage,
                fourthPercentage,
                fifthPercentage,
                sixthPercentage
            };
        }
        
        private static void AddSpawnAreasToSpawnAreaList()
        {
            var spawnArea = (GameObject.FindGameObjectsWithTag("SpawnArea"));
            
            foreach (var obj in spawnArea)
            {
                var f = new SpawnArea { GameObject = obj };
                _availableSpawnAreas.Add(f);
            }
        }
        #endregion
        
        #region ---GetSpawnPosition---
        public Vector2 GetNextPlatformSpawnPosition()
        {
            var spawnArea = PickSpawnArea(_availableSpawnAreas);
            return spawnArea.GameObject.transform.position;
        }

        private static Vector3 RandomOffset(float offsetMax)
        {
            return new Vector3(Random.Range(-offsetMax, offsetMax), Random.Range(-offsetMax, offsetMax), 0);
        }
        #endregion
        
        #region ---RandomWeightedChoice---
        private SpawnArea PickSpawnArea(List<SpawnArea> availableSpawnAreas)
        {
            var totalWeight = availableSpawnAreas.Sum(area => area.Weight);
            if (totalWeight == 0)
            {
                ResetSpawnAreaWeights(availableSpawnAreas);
            }
            var rnd = Random.Range(0, totalWeight);
            
            float sum = 0;
            foreach (var area in availableSpawnAreas)
            {
                sum += area.Weight;
                if (sum >= rnd)
                {
                    NewProbabilities(availableSpawnAreas, area);
                    return area;  
                }
            }
            return null;
        }

        private static void ResetSpawnAreaWeights(List<SpawnArea> availableSpawnAreas)
        {
            foreach (var spawnArea in availableSpawnAreas)
            {
                spawnArea.Weight = 100;
                spawnArea.TimesSpawned = 0;
            }
        }

        private void NewProbabilities(IReadOnlyCollection<SpawnArea> availableSpawnAreas, SpawnArea area)
        {
            if (_previousNeighbouringAreas != null)
            {
                if (_previousNeighbouringAreas.Contains(area)) _chainCurrentNumber++;
                else _chainCurrentNumber = 0;
            }
            if (_chainCurrentNumber > chainMaxNumber) _chainCurrentNumber = chainMaxNumber;
            NeighborsNewProbability(availableSpawnAreas, area, _chainCurrentNumber);
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
        }
        
        private void NeighborsNewProbability(IReadOnlyCollection<SpawnArea> availableSpawnAreas, SpawnArea area, int neighbourChainNumber)
        {
            _previousNeighbouringAreas = null;
            
            var currentNeighbors = availableSpawnAreas.Where(spawnArea =>
                Vector3.Distance(spawnArea.GameObject.transform.position, area.GameObject.transform.position)
                  <= neighborDistanceSearchRadius && spawnArea.TimesSpawned < maxPickRate).ToArray();
            
            if (currentNeighbors.Length !> 0) return;
            
            var weightToDistribute = area.Weight * (1 - weightLossOfPickedArea);
            var weightToNeighbours = weightToDistribute * _weightDistributedToNeighbours[neighbourChainNumber-1] / currentNeighbors.Length;
            var weightForTheRest = weightToDistribute * (1 - _weightDistributedToNeighbours[neighbourChainNumber-1]) 
                                   / (availableSpawnAreas.Count - currentNeighbors.Length);
            
            _previousNeighbouringAreas = currentNeighbors;
            foreach (var spawnArea in _availableSpawnAreas)
            {
                spawnArea.Weight += currentNeighbors.Contains(spawnArea) ? weightToNeighbours : weightForTheRest;
            }
        }
        #endregion
        #endregion
    }
}


