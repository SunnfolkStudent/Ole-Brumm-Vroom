using UnityEngine;
using UnityEngine.Serialization;

namespace From_Other_Projects
{
    public class GroundParallax : MonoBehaviour {

        Transform ground; //For reference to the transform 
        Camera cam; //Reference to Main Camera
        [SerializeField] [Range(0f, 50f)] private float speed = 1f;

        float groundWidth; //The width of the transform, used for calculating current max x position of transform and next placement x position
        private float nextXPos = 0.0f; //Store next x position in variable for easier reading

        // Use this for initialization
        void Start () {
            //Set up References
            ground = transform;
            cam = Camera.main;

            //Store Ground width (Width of the ground tile)
            groundWidth = ground.GetComponent<Renderer>().bounds.size.x; 
        }

        // Update is called once per frame
        void Update () {

            transform.Translate(Vector3.left * (Time.deltaTime * speed));
            
            //Create new Vector3 to be used in WorldToViewportPoint so it doesn't use the middle of the ground as reference
            Vector3 boxRightPos = new Vector3 (ground.position.x + groundWidth/2, ground.position.y, ground.position.z);

            //Store view Position of ground
            Vector3 viewPos = cam.WorldToViewportPoint (boxRightPos);

            //If the ground tile is left of camera viewport
            if (viewPos.x < 0) {
                //gameObject is offscreen, destroy it and re-instantiate it at new xPosition
                float currentRightX = ground.position.x + groundWidth;
                nextXPos = currentRightX + groundWidth;


                Instantiate (gameObject, new Vector3 (nextXPos, ground.position.y, ground.position.z), ground.rotation);

                Destroy (gameObject);
            }

        }
    }
}
