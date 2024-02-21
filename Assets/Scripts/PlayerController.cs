using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    #region ---Initialization & Variables---

        // --- Components ---
        private CapsuleCollider2D _playerCollider;
        private Rigidbody2D _rigidbody2D;
        private AudioSource _audioSource;
        [SerializeField] private AudioClip[] runClips;
        [SerializeField] private AudioClip[] jumpClips;
        private Animator _animator;
        
        // --- Scripts ---
        private GameManager _gameManager;
        private PlayerInput _input;
        
        // --- Variables ---
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float raycastDetectionRange = 0.5f;
        private bool isPlayerGrounded;

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
            _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
            _input = GetComponent<PlayerInput>();
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
            
            _input.ChangeToPlayer();
        }

    #endregion
    
    #region ---Input & Movement---

        private void Update()
        {
            // ---------------------- Gameplay Inputs -------------------------------
            
            // TODO: Move most of this code away from Update, and over to just receive inputs from PlayerInput below.

            /*if (PlayerInput.DropBelow)
            {
                // TODO: Call method for DropBelow.
            }*/
            
            if (PlayerInput.SlowDescend)
            {
                if (isPlayerGrounded) return;
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed / 3);

                // TODO: Call method for SlowDescend.
            }
            
            if (PlayerInput.Jump)
            {
                if (!isPlayerGrounded) return;
             
                Debug.Log("Player is jumping");
                JumpingAnimation();
                
                // Applies upwards force on the physics of the object, according to jumpSpeed.
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
            }
            
            // ---------------------- Inputs Pause Screen & RunEndedScreen -------------------------------

            if (PlayerInput.ResetRun)
            {
                _input.ChangeToPlayer();
                _gameManager.LoadNewRun();
            }
            
            if (PlayerInput.OpenPauseScreen)
            {
                _input.ChangeToPauseScreen();
                Time.timeScale = 0;
                _gameManager.LoadPauseScreen();
            }

            if (PlayerInput.ClosePauseScreen)
            {
                _input.ChangeToPlayer();
                Time.timeScale = 1;
                _gameManager.UnloadPauseScreen();
            }
            
            if (isPlayerGrounded)
            { _coyoteTimeCounter = coyoteTime; }
            else
            { _coyoteTimeCounter -= 1 * Time.deltaTime; }
    
            if (PlayerInput.Jump)
            { _jumpBufferCounter = jumpBufferTime; }
            else 
            { _jumpBufferCounter -= 1 * Time.deltaTime; }
            
            // If we are eligible to Jump
            /*if (_jumpBufferCounter > 0 && _coyoteTimeCounter > 0)
            {
                _audioSource.PlayOneShot(jumpClips[Random.Range(0, jumpClips.Length)]);
                _moveDirection.y = jumpSpeed;
            }*/
            
            isPlayerGrounded = GroundedPlayer();
        }
        
        private bool GroundedPlayer()
        {
            float offsetLeft = 0.2f;
            float offsetRight = 1.7f;
        
            // Just visuals, no function
            var position = transform.position;
            Debug.DrawRay(position + new Vector3(offsetRight, -1.6f, 0), Vector2.down, Color.red);
            Debug.DrawRay(position - new Vector3(offsetLeft, 1.6f, 0), Vector2.down, Color.green);
                
            // Draw raycasts downwards from brushRaycasterPosition, they are drawn with an offset on the x-axis on both sides
            bool hitLeft = Physics2D.Raycast(position + new Vector3(offsetLeft, 0, 0), 
                        Vector2.down, raycastDetectionRange, groundLayer);
            bool hitRight = Physics2D.Raycast(position - new Vector3(offsetRight, 0, 0), 
                        Vector2.down, raycastDetectionRange, groundLayer);

            if (hitLeft || hitRight)
            {
                Debug.Log("PlayerIsGrounded");
                return true;
            }
            else
                return false;
        }
    
        #endregion

    #region ---Animation---

    public void UpdateRunningAnimation()
    {
        if (GameManager.phase1Active)
        {
            _animator.Play("PlayerRunPhase1");
        }
        else if (GameManager.phase2Active)
        {
             _animator.Play("PlayerRunPhase2"); 
        }
        else if (GameManager.phase3Active)
        {
            _animator.Play("PlayerRunPhase3"); 
        }
        else if (GameManager.phase4Active)
        {
            _animator.Play("PlayerRunPhase4"); 
        }
    }

    private void JumpingAnimation()
    {
        if (_rigidbody2D.velocity.y > 0) 
        {
            _animator.Play("PlayerJump");
        }
        else
        {
            _animator.Play("PlayerDescend");
        }
    }

    #endregion

    #region ---Collision & Triggers---

       private void OnTriggerEnter2D(Collider2D other)
       {
           if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("DeathZone"))
           {
               Debug.Log("Player Crashed into Obstacle / DeathZone");
               _gameManager.LoadRunEndedScene();
           }
           else if (other.gameObject.CompareTag("Item"))
           {
               Debug.Log("Player has picked up an Item");
               // TODO: Run Unity Event (Item), which applies the item buff to the game.
           }
       }

   #endregion

    #region ---Audio---

        private void PlayRunAudio()
        {
            _audioSource.PlayOneShot(runClips[Random.Range(0, runClips.Length)]);
        }
    
    #endregion
    
}
    
