using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    #region ---Initialization & Variables---

        private LayerMask _groundLayer;
        private LayerMask _platformLayer;
        private CapsuleCollider2D _playerCollider;
        private Rigidbody2D _rigidbody2D;
    
        private Animator _animator;
        private PlayerInput _input;
        
        private AudioSource _audioSource;
        [SerializeField] private AudioClip[] runClips;
        [SerializeField] private AudioClip[] jumpClips;
        
        private Vector2 _moveDirection;
        
        [Header("Player Movement (Vertical Only)")]
        [SerializeField] private float jumpSpeed = 5f;
        [SerializeField] private float gravity = 9.18f;
    
        [Header("CoyoteTime")] 
        [SerializeField] private float coyoteTime = 0.2f;
        private float _coyoteTimeCounter;
    
        [Header("JumpBuffer")] 
        [SerializeField] private float jumpBufferTime = 0.2f;
        private float _jumpBufferCounter;
    
        // Update is called once per frame
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
        }

    #endregion
    
    #region ---Input & Movement---

        private void Update()
        {
            // ---------------------- Gameplay Inputs -------------------------------
            
            // TODO: Move most of this code away from Update, and over to just receive inputs from PlayerInput below.

            if (PlayerInput.DropBelow)
            {
                // TODO: Call method for DropBelow.
            }
            
            if (PlayerInput.SlowDescend)
            {
                // TODO: Call method for SlowDescend.
            }
            
            if (PlayerInput.Jump)
            {
                // TODO: Call method for Jump.
            }
            
            // ---------------------- Inputs Pause Screen & RunEndedScreen -------------------------------

            if (PlayerInput.ResetRun)
            {
                _input.ChangeToPlayer();
                SceneController.LoadNewRun();
            }
            
            if (PlayerInput.OpenPauseScreen)
            {
                _input.ChangeToPauseScreen();
                Time.timeScale = 0;
                SceneController.LoadPauseScreen();
            }

            if (PlayerInput.ClosePauseScreen)
            {
                _input.ChangeToPlayer();
                Time.timeScale = 1;
                SceneController.UnloadPauseScreen();
            }
            
            
            
            _moveDirection = _rigidbody2D.velocity;
            
            if (IsPlayerGrounded())
            { _coyoteTimeCounter = coyoteTime; }
            else
            { _coyoteTimeCounter -= 1 * Time.deltaTime; }
    
            if (PlayerInput.Jump)
            { _jumpBufferCounter = jumpBufferTime; }
            else 
            { _jumpBufferCounter -= 1 * Time.deltaTime; }
            
            // If we are eligible to Jump
            if (_jumpBufferCounter > 0 && _coyoteTimeCounter > 0)
            {
                _audioSource.PlayOneShot(jumpClips[Random.Range(0, jumpClips.Length)]);
                _moveDirection.y = jumpSpeed;
                _jumpBufferCounter = 0f;
            }
            
            if (PlayerInput.Jump && _rigidbody2D.velocity.y > 0f)
            {
                _moveDirection.y *= 0.5f;
                _coyoteTimeCounter = 0f;
            }
            _rigidbody2D.velocity = _moveDirection;
        }
        private void FixedUpdate()
        {
            // TODO: Move physics code over into here...
            throw new NotImplementedException();
        }
    
        #endregion

    #region ---Animation---

    private void UpdateAnimation()
    {
        Debug.Log("Updating Animation");
        
        // TODO: Set this up receive distance travelled / checkpoint reached from the DistanceTravelled.
        // Maybe change the override we're using after a certain phase? Or just switch over a new anim.

        _animator.Play("PlayerRunPhase1"); 
        
        _animator.Play("PlayerRunPhase2");
        
        _animator.Play("PlayerRunPhase3");
    }

    #endregion

    #region ---Collision & Triggers---

       private void OnTriggerEnter2D(Collider2D other)
       {
           if (other.gameObject.CompareTag("Obstacle"))
           {
               Debug.Log("Player Crashed into Obstacle");
               // TODO: Run Unity Event (PlayerStopped), which leads to a mini-score & progress scene with option to reset
           }
           else if (other.gameObject.CompareTag("Enemy"))
           {
               Debug.Log("Player Crashed into Enemy");
               // TODO: Run Unity Event (PlayerStopped), which leads to a mini-score & progress scene with option to reset
           }
           else if (other.gameObject.CompareTag("Item"))
           {
               Debug.Log("Player has picked up an Item");
               // TODO: Run Unity Event (Item), which applies the item buff to the game.
           }
       }
            
            private bool IsPlayerGrounded()
       {
           return Physics2D.Raycast(transform.position, Vector2.down, 0.2f, _groundLayer);
       }

   #endregion

    #region ---Audio---

        private void PlayRunAudio()
        {
            _audioSource.PlayOneShot(runClips[Random.Range(0, runClips.Length)]);
        }
    
    #endregion
    
}
    
