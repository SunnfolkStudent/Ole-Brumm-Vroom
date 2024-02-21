using UnityEngine;

public class Parallax : MonoBehaviour {

    private Transform ground; // For reference to the transform 
    private Camera cam; // Reference to Main Camera
    [SerializeField] [Range(0f, 50f)] private float speed = 1f;

    private float groundWidth; //The width of the transform, used for calculating current max x position of transform and next placement x position
    private float nextXPos = 0.0f; //Store next x position in variable for easier reading

    [SerializeField] private bool isBackground;
    [SerializeField] private bool isPlatform;
    [SerializeField] private bool isObstacle;

    // Use this for initialization
    private void Start() 
    {
        ground = transform;
        cam = Camera.main;

        //Store Ground width (Width of the ground tile)
        groundWidth = ground.GetComponent<Renderer>().bounds.size.x; 
    }

        
    private void Update() 
    {
        transform.Translate(Vector3.left * (Time.deltaTime * speed));
            
        //Create new Vector3 to be used in WorldToViewportPoint so it doesn't use the middle of the ground as reference
        var position = ground.position;
        Vector3 boxRightPos = new Vector3 (position.x + groundWidth/2, position.y, position.z);

        //Store view Position of ground
        Vector3 viewPos = cam.WorldToViewportPoint (boxRightPos);

        //If the ground tile is left of camera viewport
        if (viewPos.x < 0) 
        {
            //gameObject is offscreen, destroy it and re-instantiate it at new xPosition
            float currentRightX = ground.position.x + groundWidth - 1f;
            nextXPos = currentRightX + groundWidth;
                
            Instantiate(gameObject, new Vector3 (nextXPos, position.y, position.z), ground.rotation);

            Destroy(gameObject);
        }

    }
}