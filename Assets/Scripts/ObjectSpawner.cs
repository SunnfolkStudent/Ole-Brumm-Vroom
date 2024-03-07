using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Element 0 is the lowest platform, element 1 is 2nd lowest...")]
    public GameObject[] availableSpawnAreas;
    public List<GameObject> availablePlatformLayouts;
    private List<GameObject> _possibleSpawns;

    [Header("For the platforms, to help increase horizontal gap at high speeds.")]
    [SerializeField] private float phase1Offset = 1f;
    [SerializeField] private float phase2Offset = 1.5f;
    [SerializeField] private float phase3Offset = 2.25f;
    [SerializeField] private float phase4Offset = 3f;
    
    // Start is called before the first frame update
    private void Start()
    {
        availablePlatformLayouts = new List<GameObject>();
        _possibleSpawns = new List<GameObject>();
        foreach (var platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            availablePlatformLayouts.Add(platform);
        }
    }
    
    private int HowManyPlatformsAreSpawningNext(List<GameObject> possibleSpawnAreas)
    {
        var howManyPlatforms = Random.Range(1, possibleSpawnAreas.Count);
        
        return howManyPlatforms;
        
        /*var platform1 = possibleSpawnAreas[Random.Range(0, possibleSpawnAreas.Count)];
        var numberOfPlatforms = possibleSpawnAreas[Random.Range(0, possibleSpawnAreas.Count)];
        return numberOfPlatforms;*/
    }

    // "Where" are we spawning?
    private void ChooseNextPossibleSpawnAreas(GameObject lastPlatformSpawnLocation)
    {
        var bottomSpawnArea = availableSpawnAreas[0];
        var lowSpawnArea = availableSpawnAreas[1];
        var highSpawnArea = availableSpawnAreas[2];
        var topSpawnArea = availableSpawnAreas[3];
        
        if (lastPlatformSpawnLocation == topSpawnArea)
        {
            _possibleSpawns.Add(topSpawnArea);
            _possibleSpawns.Add(highSpawnArea);
            _possibleSpawns.Add(lowSpawnArea);
            _possibleSpawns.Add(bottomSpawnArea);

            HowManyPlatformsAreSpawningNext(_possibleSpawns);
        }
        else if (lastPlatformSpawnLocation == highSpawnArea)
        {
            _possibleSpawns.Add(topSpawnArea);
            _possibleSpawns.Add(highSpawnArea);
            _possibleSpawns.Add(lowSpawnArea);
            _possibleSpawns.Add(bottomSpawnArea);

            HowManyPlatformsAreSpawningNext(_possibleSpawns);
        }
        else if (lastPlatformSpawnLocation == lowSpawnArea)
        {
            _possibleSpawns.Add(highSpawnArea);
            _possibleSpawns.Add(lowSpawnArea);
            _possibleSpawns.Add(bottomSpawnArea);

            HowManyPlatformsAreSpawningNext(_possibleSpawns);
        }
        else // groundPlatform
        {
            _possibleSpawns.Add(lowSpawnArea);
            _possibleSpawns.Add(bottomSpawnArea);

            HowManyPlatformsAreSpawningNext(_possibleSpawns);
        }
    }
    
    private Vector2 PlatformSpawnOffset()
    {
        if (GameManager.phase1Active)
        {
            return new Vector2(phase1Offset, 0);
        }
        else if (GameManager.phase2Active)
        {
            return new Vector2(phase2Offset, 0);
        }
        else if (GameManager.phase3Active)
        {
            return new Vector2(phase3Offset, 0);
        }
        else
        {
            return new Vector2(phase4Offset, 0);
        }
    }

    

    private List<GameObject> PickSpawnAreas(int possibleSpawnAreas)
    {
        return new List<GameObject>();
    }

    // "What" ...is spawning?
    private void ChoosePlatform(GameObject spawnArea, List<GameObject> spawnAreas)
    {
        var chosenPlatform = availablePlatformLayouts[Random.Range(0, availablePlatformLayouts.Count)];
        
        SpawnPlatform(spawnArea, chosenPlatform);
    }

    // "When" is it spawning?
    private void SpawnPlatform(GameObject whereToSpawn, GameObject platform)
    {
        
        
        
        ChooseNextPossibleSpawnAreas(whereToSpawn);
    }

    private void SpawnPlatforms(GameObject whereToSpawn, GameObject[] platforms)
    {
        
        
        ChooseNextPossibleSpawnAreas(whereToSpawn);
    }

}
