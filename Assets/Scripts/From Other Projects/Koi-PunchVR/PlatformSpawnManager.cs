using System.Collections;
using System.Collections.Generic;
using From_Other_Projects.Koi_PunchVR;
using UnityEngine;

public class PlatformSpawnManager : MonoBehaviour
{
    private PlatformSpawnAreas _platformSpawnAreas;
    
    #region ---Initialization---
    private void Awake()
    {
        _platformSpawnAreas = GetComponent<PlatformSpawnAreas>();
        EventManager.SpawnFish += SpawnFish;
    }
    #endregion
    
    private void SpawnFish()
    {
        var spawnPos = _platformSpawnAreas.GetNextPlatformSpawnPosition();
        var platform = PlatformSpawnType.GetNextPlatform();
        platform.ParentGameObject.SetActive(true);
    }
}
