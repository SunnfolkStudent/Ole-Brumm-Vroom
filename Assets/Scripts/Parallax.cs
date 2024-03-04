using UnityEngine;
using UnityEngine.Serialization;

public class Parallax : MonoBehaviour
{
    private Spawner _spawner;
    private Transform _objectTransform; // For reference to the transform 
    private Camera _cam; // Reference to Main Camera
    
    private float _objectWidth; // The width of the transform, used for calculating current max x position of transform and next placement x position
    private float _nextXPos = 0f; // Store next x position in variable for easier reading

    private Vector3 _initialSpawnPosition;
    
    [Header("This is the phase1 speed and initial speed that's being multiplied with:")]
    [SerializeField] [Range(0f, 50f)] private float initialObjectSpeed = 1f;

    [Header("Multipliers for parallax speed in each phase:")]
    [SerializeField] private float multiplierPhase2 = 1.5f;
    [SerializeField] private float multiplierPhase3 = 3f;
    [SerializeField] private float multiplierPhase4 = 5f;

    [Header("Choose a bool depending on function. Leave unchecked if background.")]
    [SerializeField] private bool isAirPlatform;
    [SerializeField] private bool isGroundPlatform;
    [SerializeField] private bool isObstacle;

    // Use this for initialization
    private void Start() 
    {
        _spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        _objectTransform = transform;
        _initialSpawnPosition = _objectTransform.position;
        _cam = Camera.main;
        
        if (isAirPlatform)
        {
            _objectWidth = _objectTransform.GetComponentInChildren<Renderer>().bounds.size.x; 
        }
        else
        {
            // Store Ground Width (Width of the Ground tile)
            _objectWidth = _objectTransform.GetComponent<Renderer>().bounds.size.x;
        }
    }

    private float CurrentSpeed()
    {
        // TODO: Swap out the static bools with Events & Listeners...
        
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
            
        //Create new Vector3 to be used in WorldToViewportPoint so it doesn't use the middle of the object as reference
        var position = _objectTransform.position;
        Vector3 boxRightPos = new Vector3 (position.x + _objectWidth/2, position.y, position.z);

        //Store view Position of the object
        Vector3 viewPos = _cam.WorldToViewportPoint (boxRightPos);

        //If the object tile is left of camera viewport
        if (viewPos.x < 0) 
        {
            // if gameObject is offscreen, destroy it and re-instantiate it at new xPosition
            float currentRightX = _objectTransform.position.x + _objectWidth;
            
            if (isAirPlatform || isObstacle)
            {
                // TODO: Swap out with reference to objectPool and deactivate object instead.
                _objectTransform.position = new Vector3(_initialSpawnPosition.x, position.y, position.z);
                
                // Instantiate(gameObject, _initialSpawnPosition, _objectTransform.rotation);
            }
            else if (isGroundPlatform)
            {
                // TODO: Swap out with reference to objectPool and deactivate object instead.
                _objectTransform.position = new Vector3(_initialSpawnPosition.x, position.y, position.z);
                // Instantiate(gameObject, _initialSpawnPosition, _objectTransform.rotation); 
            }
            else
            {
                _nextXPos = currentRightX + _objectWidth;
                _objectTransform.position = new Vector3(_nextXPos, position.y, position.z);
                // Instantiate(gameObject, new Vector3(_nextXPos, position.y, position.z), _objectTransform.rotation); 
            }
        }
    }
}