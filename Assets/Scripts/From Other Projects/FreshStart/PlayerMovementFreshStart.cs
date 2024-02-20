using From_Other_Projects.Programming101;
using UnityEngine;
using UnityEngine.InputSystem;

namespace From_Other_Projects.FreshStart
{
    public class PlayerMovementFreshStart : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 5f;
        public float jumpSpeed = 12f;
        private Vector2 _desiredVelocity;

        [Header("CoyoteTime")] 
        public float coyoteTime = 0.2f;
        public float coyoteTimeCounter;

        [Header("JumpBuffer")] 
        public float jumpBufferTime = 0.2f;
        public float jumpBufferCounter;
    
        [Header("Acceleration")]
        public float accelerationTime = 0.02f;
        public float groundFriction = 0.03f;
        public float airFriction = 0.005f;
    
        [Header("isGrounded")]
        public LayerMask whatIsGround;

        [Header("Audio")] 
        public AudioClip[] jumpClips;
        public AudioClip[] walkClips;

        [Header("Components")]
        private Rigidbody2D _rigidbody2D;
        private InputManagerFreshStart _input;
        private Animator _animation;
        private AudioSource _audioSource;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _input = GetComponent<InputManagerFreshStart>();
            _animation = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            _desiredVelocity = _rigidbody2D.velocity;


            if (Keyboard.current.uKey.wasPressedThisFrame)
            {
                _audioSource.Play();
            }
            // _animation.UpdateAnimation(_desiredVelocity, IsPlayerGrounded(), _input.moveDirection);
        
            if (IsPlayerGrounded())
            { coyoteTimeCounter = coyoteTime; }
            else
            { coyoteTimeCounter -= 1 * Time.deltaTime; }

            if (_input.jumpPressed)
            { jumpBufferCounter = jumpBufferTime; }
            else 
            { jumpBufferCounter -= 1 * Time.deltaTime; }
        
            // If we are eligible to Jump
            if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
            {
                _audioSource.PlayOneShot(jumpClips[Random.Range(0, jumpClips.Length)]);
                _desiredVelocity.y = jumpSpeed; // Jump
                jumpBufferCounter = 0f;
            }
        
            if (_input.jumpReleased && _rigidbody2D.velocity.y > 0f)
            {
                _desiredVelocity.y *= 0.5f;
                coyoteTimeCounter = 0f;
            }
            _rigidbody2D.velocity = _desiredVelocity;
        }
    
        private void FixedUpdate()
        {
            if (_input.moveDirection.x != 0)
            {
                _desiredVelocity.x = Mathf.Lerp(_desiredVelocity.x, 
                    moveSpeed * _input.moveDirection.x, accelerationTime);
            }
            else
            {
                _desiredVelocity.x = Mathf.Lerp(_desiredVelocity.x, 0f, 
                    IsPlayerGrounded() ? groundFriction : airFriction);
            }

            _rigidbody2D.velocity = _desiredVelocity;
        }

        private bool IsPlayerGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, 1.6f, whatIsGround);
        }

        public void PlayWalkAudio()
        {
            _audioSource.PlayOneShot(walkClips[Random.Range(0, walkClips.Length)]);
        }
    }
}