using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DespawnWall : MonoBehaviour
{
    private BoxCollider2D _wallCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        _wallCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Detected!");
        Destroy(other.gameObject);
        // TODO: Notify SpawnManager.
    }
}
