using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class Parallax : MonoBehaviour
{
    private Transform _objectTransform; // For reference to the transform 
    private Camera _cam; // Reference to Main Camera
    
    private float _objectWidth; // The width of the transform, used for calculating current max x position of transform and next placement x position
    private float _nextXPos; // Store next x position in variable for easier reading

    private Vector3 _initialSpawnPosition;
    
    [Header("This is the initial speed and acceleration that's being multiplied with:")]
    [SerializeField] [Range(0f, 50f)] private float initialVelocity = 2f;
    [SerializeField] [Range(0f, 50f)] private float initialAcceleration = 0.05f;
    private float _currentVelocity;
    private float _currentAcceleration;
    
    [Header("Multipliers that determine speed in each phase:")]
    [SerializeField] private float multiplierPhase2 = 1.5f;
    [SerializeField] private float multiplierPhase3 = 3f;
    [SerializeField] private float multiplierPhase4 = 5f;

    [Header("Choose a bool depending on function. Leave unchecked if background.")]
    [SerializeField] private bool isAirPlatform;
    [SerializeField] private bool isGroundPlatform;
    [SerializeField] private bool isObstacle;

    [SerializeField] private bool debugViewPos;
    // private bool _objectStopped;
    
    // Use this for initialization
    private void Start() 
    {
        // _spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        _objectTransform = transform;
        _initialSpawnPosition = _objectTransform.position;
        _cam = Camera.main;

        if (_objectTransform.TryGetComponent(out Renderer _))
        {
            _objectWidth = _objectTransform.GetComponent<Renderer>().bounds.size.x;
        }
        else
        {
            _objectWidth = _objectTransform.GetComponent<BoxCollider2D>().bounds.size.x;
        }
        ChangeCurrentSpeed();
    }

    // TODO: Make a reference to this below method everytime we adjust the speed of an object.
    public void ChangeCurrentSpeed()
    {
        switch (GameManager.currentPhase)
        {
            case 1:
                _currentVelocity = initialVelocity;
                _currentAcceleration = initialAcceleration;
                break;
            case 2:
                _currentVelocity = initialVelocity * multiplierPhase2;
                _currentAcceleration = initialAcceleration * multiplierPhase2;
                break;
            case 3:
                _currentVelocity = initialVelocity * multiplierPhase3;
                _currentAcceleration = initialAcceleration * multiplierPhase3;
                break;
            case 4:
                _currentVelocity = initialVelocity * multiplierPhase4;
                _currentAcceleration = initialAcceleration * multiplierPhase4;
                break;
        }
    }
        
    private void FixedUpdate()
    {
        // TODO: Set up the right system for speed with objects : )
        
        _currentVelocity += _currentAcceleration * Time.fixedDeltaTime;
        transform.Translate(Vector3.left * (_currentVelocity * Time.fixedDeltaTime)); 
            
        // Create new Vector3 to be used in WorldToViewportPoint so it doesn't use the middle of the object as reference
        var position = _objectTransform.position;
        
        // TODO: Set up collider and EstimatedCrashTime to use objectLeftPos & objectRightPos. : )
        Vector3 objectLeftPos = new Vector3 (position.x - _objectWidth/2, position.y, position.z);
        Vector3 objectRightPos = new Vector3 (position.x + _objectWidth/2, position.y, position.z);

        // Store view Position of the object
        Vector3 viewPos = _cam.WorldToViewportPoint (objectRightPos);

        if (debugViewPos)
        {
            Debug.Log("ViewPos of " + gameObject + ":" + viewPos);
        }
        
        //If the object tile is left of camera viewport
        if (viewPos.x < 0) 
        {
            // if gameObject is offscreen, destroy it and re-instantiate it at new xPosition
            float currentRightX = _objectTransform.position.x + _objectWidth;
            
            if (isAirPlatform || isObstacle)
            {
                _objectTransform.position = new Vector3(_initialSpawnPosition.x, position.y, position.z);
                // TODO: Insert method that stops platform, until it receives new instructions.
                StopPlatform();
                // Function(_objectTransform.position);
            }
            else if (isGroundPlatform)
            {
                _objectTransform.position = new Vector3(_initialSpawnPosition.x, position.y);
                // TODO: Insert method that stops platform, until it receives new instructions.
                StopPlatform();
                // Function(_objectTransform.position);
            }
            else
            {
                _nextXPos = currentRightX + _objectWidth;
                _objectTransform.position = new Vector3(_nextXPos, position.y, position.z);
                // Instantiate(gameObject, new Vector3(_nextXPos, position.y, position.z), _objectTransform.rotation); 
            }
        }
    }

    private void StopPlatform()
    {
        _currentVelocity = 0;
        _currentAcceleration = 0;
        // _objectStopped = true;
    }
    
    
    
}