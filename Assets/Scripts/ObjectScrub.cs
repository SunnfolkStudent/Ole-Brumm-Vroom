using UnityEngine;

[CreateAssetMenu(fileName = "NewObject", menuName = "Scriptable Object", order = 1)]
public class ObjectScrub : ScriptableObject
{
    [Header("Platform Properties")] 
    public GameObject objectPrefab;
            
    // We'll send this to a method, that will Instantiate 5 gameObjects of this scrub. They will be used and rotated as needed.
    public int initialAmountInPool = 5;
    public float weightInRandomTable = 100;
}