using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewPlatform", menuName = "Platform", order = 1)]
public class PlatformScrub : ScriptableObject
{
    [Header("Platform Properties")] 
    public GameObject platformPrefab;
    public int initialAmountInPool = 5;
    public float weightInRandomTable = 100;
}