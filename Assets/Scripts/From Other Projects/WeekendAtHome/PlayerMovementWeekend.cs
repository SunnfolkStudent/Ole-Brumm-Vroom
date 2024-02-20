using UnityEngine;

namespace From_Other_Projects
{
    public class PlayerMovementWeekend : MonoBehaviour
    {
        public Animator anim;
        [SerializeField] private Rigidbody2D rb;
               
        [Header("Configurable Parameters")]
        [SerializeField] private float moveSpeed = 2.25f;
        
        private Vector2 _directionV2;
        
        // We're giving the string any initial value ("_Down"), to avoid yellow errors with Animator Index-1; which is the Animator not finding a string to play;
        // This makes sure the animator always can fall back to play "_Down", at any point!
        private string _direction = "_Down";

        private void Update()
        {
            // if (!DataTransfer.PlayerCanMove) return;
            
            // Animation direction
            if (_directionV2.y > Mathf.Sqrt(0.5f))
            {
                _direction = "_Up";
            } 
            else if (_directionV2.y < -Mathf.Sqrt(0.5f))
            {
                _direction = "_Down";
            } 
            else if (_directionV2.x > Mathf.Sqrt(0.5f))
            {
                _direction = "_Right";
            }
            else if (_directionV2.x < -Mathf.Sqrt(0.5f))
            {
                _direction = "_Left";
            }

            Animate(_directionV2 == Vector2.zero ? "Player_Idle" : "Player_Walk");
            
            _directionV2.x = Input.GetAxisRaw("Horizontal");
            _directionV2.y = Input.GetAxisRaw("Vertical");
        }
        
        private void Animate(string unitAnimation)
        {
            anim.Play(unitAnimation + _direction);
        }

        private void FixedUpdate()
        {
            // if (!DataTransfer.PlayerCanMove) return;
            rb.MovePosition(rb.position + _directionV2.normalized * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}
