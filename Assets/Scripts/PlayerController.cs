using System;
using System.Collections.Generic;
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
        [SerializeField] private float raycastDetectionRange = 0.1f;
        [SerializeField] private float raycastStartingOffsetX = 1f;
        
        private bool isPlayerGrounded;
        private bool isPlayerOnPlatform;
        private List<Collider2D> platform = new ();

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

        [Header("Collision Boxes")]
        [SerializeField] private Collider2D headCollider;
        [SerializeField] private Collider2D bodyCollider;
    
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

            if (PlayerInput.Jump)
            {
                if (!isPlayerGrounded) return;
                PlayerJumps();
            }

            if (PlayerInput.DropBelow)
            {
                print("Dropping through " + platform.Count + " platforms!");
                foreach (var p in platform)
                {
                    if (p == null) continue;
                    Physics2D.IgnoreCollision(headCollider, p);
                    Physics2D.IgnoreCollision(bodyCollider, p);
                }

                /*var amount = platform.Count;*/
                while(platform.Contains(null))
                {
                    platform.Remove(null);

                    /*
                    amount--;

                    if (amount < 0) break;*/
                }
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

            if (PlayerInput.QuitGame)
            {
                _gameManager.QuitGame();
            }
            
            /*if (isPlayerGrounded)
            { _coyoteTimeCounter = coyoteTime; }
            else
            { _coyoteTimeCounter -= 1 * Time.deltaTime; }
    
            if (PlayerInput.Jump)
            { _jumpBufferCounter = jumpBufferTime; }
            else 
            { _jumpBufferCounter -= 1 * Time.deltaTime; }*/
            
            // If we are eligible to Jump
            /*if (_jumpBufferCounter > 0 && _coyoteTimeCounter > 0)
            {
                _audioSource.PlayOneShot(jumpClips[Random.Range(0, jumpClips.Length)]);
                _moveDirection.y = jumpSpeed;
            }*/
            
            isPlayerGrounded = GroundedPlayer();
            if (!isPlayerGrounded)
            {
                Debug.Log("Player is not grounded");
            }
        }

        private void FixedUpdate()
        {
            if (_rigidbody2D.velocity.y == 0 && (isPlayerGrounded || isPlayerOnPlatform)) 
            {
                RunningAnimations();
            }
        }

        private bool GroundedPlayer()
        {
            // Just visuals, no function
            var position = transform.position;
            Debug.DrawRay(position + new Vector3(raycastStartingOffsetX, 0, 0), Vector2.down * raycastDetectionRange, Color.magenta);
                
            // Draw raycasts downwards from brushRaycasterPosition, they are drawn with an offset on the x-axis on both sides
            bool hitGround = Physics2D.Raycast(position + new Vector3(raycastStartingOffsetX, 0, 0), 
                        Vector2.down, distance: raycastDetectionRange, groundLayer);

            if (hitGround)
            {
                Debug.Log("Player Is Grounded");
                return true;
            }
            return false;
        }
    
        #endregion

    private void PlayerJumps()
    {
        Debug.Log("Player attempts jumping");
        if (!isPlayerGrounded) return;
        Debug.Log("Player is jumping");
        JumpingAnimation();
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
    }
        
    #region ---Animation---

    public void RunningAnimations()
    {
        if (_rigidbody2D.velocity.y != 0 && isPlayerGrounded) return;
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
        if (_rigidbody2D.velocity.y is > 0 or < 0) 
        {
            _animator.Play("PlayerJump");
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
       
       private void OnTriggerExit2D(Collider2D other)
       {
           if (!other.gameObject.CompareTag("Platform")) return;

           var collision = other.GetComponent<PlatformCollisionBoxes>().collision;

           if (platform.Contains(collision))
           {
               Physics2D.IgnoreCollision(headCollider, collision, false);
               Physics2D.IgnoreCollision(bodyCollider, collision, false);
               platform.Remove(collision);
           }
       }

       private void OnCollisionEnter2D(Collision2D other)
       {
           if (!other.gameObject.CompareTag("Platform")) return;

           if (!platform.Contains(other.collider)) platform.Add(other.collider);
       }

       #endregion

    #region ---Audio---

        private void PlayRunAudio()
        {
            _audioSource.PlayOneShot(runClips[Random.Range(0, runClips.Length)]);
        }
    
    #endregion
    
}
    
