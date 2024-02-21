using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Spawner _spawner;
    private Transform ground; // For reference to the transform 
    private Camera cam; // Reference to Main Camera
    
    private float groundWidth; // The width of the transform, used for calculating current max x position of transform and next placement x position
    private float nextXPos; // Store next x position in variable for easier reading
    
    [Header("This is the phase1 speed and initial speed that's being multiplied with:")]
    [SerializeField] [Range(0f, 50f)] private float initialObjectSpeed = 1f;

    [Header("Multipliers for parallax speed in each phase:")]
    [SerializeField] private float multiplierPhase2 = 1.5f;
    [SerializeField] private float multiplierPhase3 = 3f;
    [SerializeField] private float multiplierPhase4 = 6f;

    [Header("Choose a bool depending on function. Leave unchecked if background.")]
    [SerializeField] private bool isPlatform;
    [SerializeField] private bool isObstacle;

    // Use this for initialization
    private void Start() 
    {
        _spawner = GetComponentInParent<Spawner>();
        ground = transform;
        cam = Camera.main;
        
        if (isPlatform)
        {
            groundWidth = ground.GetComponentInChildren<Renderer>().bounds.size.x; 
        }
        else
        {
            // Store Ground Width (Width of the Ground tile)
            groundWidth = ground.GetComponent<Renderer>().bounds.size.x; 
        }
    }

    private float CurrentSpeed()
    {
        if (GameManager.phase1Active)
        {
            return initialObjectSpeed;
        }
        if (GameManager.phase2Active)
        {
            return initialObjectSpeed * multiplierPhase2;
        }
        if (GameManager.phase3Active)
        {
            return initialObjectSpeed * multiplierPhase3;
        }
        if (GameManager.phase4Active)
        {
            return initialObjectSpeed * multiplierPhase4;
        }
        if (!GameManager.IsPlaying)
        {
            return initialObjectSpeed * 0;
        }
        return initialObjectSpeed * 1;
    }
        
    private void Update() 
    {
        transform.Translate(Vector3.left * (Time.deltaTime * CurrentSpeed()));
            
        //Create new Vector3 to be used in WorldToViewportPoint so it doesn't use the middle of the ground as reference
        var position = ground.position;
        Vector3 boxRightPos = new Vector3 (position.x + groundWidth/2, position.y, position.z);

        //Store view Position of ground
        Vector3 viewPos = cam.WorldToViewportPoint (boxRightPos);

        //If the ground tile is left of camera viewport
        if (viewPos.x < 0) 
        {
            // if gameObject is offscreen, destroy it and re-instantiate it at new xPosition
            float currentRightX = ground.position.x + groundWidth;
            if (isPlatform || isObstacle)
            {
                // TODO: Connect to the Spawner Script, and spawn through that one.
            }
            else
            {
                nextXPos = currentRightX + groundWidth;
                Instantiate(gameObject, new Vector3 (nextXPos, position.y, position.z), ground.rotation);
            }
            Destroy(gameObject);
        }

    }
}