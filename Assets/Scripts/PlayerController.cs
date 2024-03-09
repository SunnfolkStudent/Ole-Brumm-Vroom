using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    #region ---Initialization & Variables---

    // --- Components ---
    private CapsuleCollider2D _playerCollider;
    private Rigidbody2D _rigidbody2D;
    public AudioSource audioSourceRunning;
    public AudioSource audioSourceOther;
    [SerializeField] private AudioClip[] runClips;
    [SerializeField] private AudioClip[] phaseShiftRunClips;
    [SerializeField] private AudioClip playerCrashed;
    [SerializeField] private AudioClip sadFailedRunToot;
    private Animator _animator;
    private Transform _transformChild;

    // --- Scripts ---
    private GameManager _gameManager;
    private PlayerInput _input;

    // --- Variables ---
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastDetectionRange = 0.1f;
    [SerializeField] private float raycastStartingOffsetX = 1f;

    [SerializeField] [Range(0,1)] private float runningVolume;
    [SerializeField] [Range(0,1)] private float otherVolume;
    private float _defaultRunningVolume;
    private float _defaultOtherVolume;

    private bool _isPlayerGrounded;
    public static bool PlayerHasCrashed;
    private bool _isPlayerOnPlatform;
    private bool _currentlyInJumpOrFalling;
    private List<Collider2D> _platform = new();

    private Vector2 _moveDirection;

    [Header("Player Movement (Vertical Only)")] 
    [SerializeField] private float jumpSpeed = 5f;
    private float _doubleJumpTimer;

    private bool _recentlyJumped;
    // [SerializeField] private float gravity = 9.18f;

    [Header("CoyoteTime")] 
    // [SerializeField] private float coyoteTime = 0.2f;
    private float _coyoteTimeCounter;

    [Header("JumpBuffer")] 
    // [SerializeField] private float jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;

    [Header("Collision Boxes")] 
    [SerializeField] private Collider2D headCollider;
    [SerializeField] private Collider2D bodyCollider;

    // Update is called once per frame
    private void Start()
    {
        _transformChild = GetComponentInChildren<Transform>();
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        audioSourceRunning.Play();
        _defaultRunningVolume = audioSourceRunning.volume;
        _defaultOtherVolume = audioSourceOther.volume;

        _input.ChangeToPlayer();
    }

    #endregion

    #region ---Input & Movement---

    private void Update()
    {
        _doubleJumpTimer += Time.deltaTime;
        if (_doubleJumpTimer > 3f)
        {
            _recentlyJumped = false;
        }
        audioSourceRunning.volume = runningVolume;
        
        if (PlayerInput.Jump)
        {
            if (PlayerHasCrashed) return;
            if (_isPlayerGrounded && !_currentlyInJumpOrFalling)
            {
                StartCoroutine(PlayerJumps());
            }
            else
            {
                StartCoroutine(PlayerDoubleJumps());
            }
        }

        if (PlayerInput.DropBelow)
        {
            StartCoroutine(PlayerDropsDown());
        }

        // ---------------------- Inputs Pause Screen & RunEndedScreen -------------------------------

        if (PlayerInput.ResetRun)
        {
            PlayerHasCrashed = false;
            _input.ChangeToPlayer();
            StartCoroutine(_gameManager.LoadNewRun());
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
            StartCoroutine(_gameManager.UnloadPauseScreen());
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
    }

    private void FixedUpdate()
    {
        if (PlayerHasCrashed) return;
        _isPlayerGrounded = GroundedPlayer();
        if (_currentlyInJumpOrFalling) return;
        if (_isPlayerGrounded || _isPlayerOnPlatform)
        {
            RunningAnimations();
        }
        
        if (!_isPlayerGrounded)
        {
            Debug.Log("Player is not grounded");
        }
        
        /*if (_rigidbody2D.velocity.y == 0 && (_isPlayerGrounded || _isPlayerOnPlatform))
        {
            RunningAnimations();
        }*/
    }

    private bool GroundedPlayer()
    {
        // Just visuals, no function
        var position = _transformChild.position;
        Debug.DrawRay(position + new Vector3(raycastStartingOffsetX, 0, 0), Vector2.down * raycastDetectionRange,
            Color.magenta);

        // Draw raycasts downwards from brushRaycasterPosition, they are drawn with an offset on the x-axis on both sides
        bool hitGround = Physics2D.Raycast(position + new Vector3(raycastStartingOffsetX, 0, 0),
            Vector2.down, raycastDetectionRange, groundLayer);

        if (hitGround)
        {
            Debug.Log("Player Is Grounded");
            return true;
        }
        return false;
    }

    #endregion

    private IEnumerator PlayerJumps()
    {
        _currentlyInJumpOrFalling = true;
        runningVolume = 0;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
        _animator.Play("PlayerJump");
        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => _isPlayerGrounded || PlayerInput.Jump);
        if (PlayerInput.Jump)
        {
            StartCoroutine(PlayerDoubleJumps());
            yield return new WaitUntil(() => _isPlayerGrounded);
            StopCoroutine(PlayerDoubleJumps());
            _currentlyInJumpOrFalling = false;
        }
        if (_isPlayerGrounded)
        {
            StopCoroutine(PlayerDoubleJumps());
            _currentlyInJumpOrFalling = false;
        }
    }

    private IEnumerator PlayerDoubleJumps()
    {
        if (_recentlyJumped || !_currentlyInJumpOrFalling) yield break;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
        _animator.Play("PlayerJump");
        _doubleJumpTimer = 0;
    }

    private IEnumerator PlayerDropsDown()
    {
        _currentlyInJumpOrFalling = true;
        runningVolume = 0;
        foreach (var p in _platform)
        {
            if (p == null) continue;
            Physics2D.IgnoreCollision(headCollider, p);
            Physics2D.IgnoreCollision(bodyCollider, p);
        }

        while (_platform.Contains(null))
        {
            _platform.Remove(null);
        }

        _animator.Play("PlayerDescend");
        

        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => _isPlayerGrounded);
        _currentlyInJumpOrFalling = false;
    }

    #region ---Animation---

    private void RunningAnimations()
    {
        if (_rigidbody2D.velocity.y != 0 && !_isPlayerGrounded || PlayerHasCrashed) return;
        if (GameManager.phase1Active)
        {
            _animator.Play("PlayerRunPhase1");
            runningVolume = _defaultRunningVolume;
        }
        else if (GameManager.phase2Active)
        {
            _animator.Play("PlayerRunPhase2");
            runningVolume = _defaultRunningVolume;
        }
        else if (GameManager.phase3Active)
        {
            _animator.Play("PlayerRunPhase3");
            runningVolume = _defaultRunningVolume;
        }
        else if (GameManager.phase4Active)
        {
            _animator.Play("PlayerRunPhase4");
            runningVolume = _defaultRunningVolume;
        }
    }

    private IEnumerator PlayerCrashed()
    {
        // Start Event to Pause Everything.
        PlayerHasCrashed = true;
        yield return null;
        _rigidbody2D.velocity = new Vector2(0, 0);
        _animator.Play("PlayerCrash");
        audioSourceRunning.volume = _defaultRunningVolume;
        audioSourceRunning.PlayOneShot(playerCrashed);
        yield return new WaitForSeconds(0.2f);
        audioSourceOther.volume = _defaultOtherVolume;
        audioSourceOther.PlayOneShot(sadFailedRunToot);
        yield return new WaitForSeconds(2.5f);
        _gameManager.LoadRunEndedScene();
    }

    #endregion

    #region ---Collision & Triggers---

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("DeathZone"))
        {
            Debug.Log("Player Crashed into Obstacle / DeathZone");
            StartCoroutine(PlayerCrashed());
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

        var collision = other.GetComponent<Platforms>().collision;

        if (_platform.Contains(collision))
        {
            Physics2D.IgnoreCollision(headCollider, collision, false);
            Physics2D.IgnoreCollision(bodyCollider, collision, false);
            _platform.Remove(collision);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Platform")) return;

        if (!_platform.Contains(other.collider)) _platform.Add(other.collider);
    }

    #endregion

    public void PlayNewPhaseRunSound()
    {
        Debug.Log("PlayNewPhaseRunSound called upon.");
        if (GameManager.phase1Active)
        {
            audioSourceOther.clip = phaseShiftRunClips[0];
            audioSourceOther.Play();
            audioSourceRunning.clip = runClips[0];
        }
        else if (GameManager.phase2Active)
        {
            audioSourceOther.clip = phaseShiftRunClips[1];
            audioSourceOther.Play();
            audioSourceRunning.clip = runClips[0];
        }
        else if (GameManager.phase3Active)
        {
            audioSourceOther.clip = phaseShiftRunClips[2];
            audioSourceOther.Play();
            audioSourceRunning.clip = runClips[1];
        }
        else if (GameManager.phase4Active)
        {
            audioSourceOther.clip = phaseShiftRunClips[3];
            audioSourceOther.Play();
            audioSourceRunning.clip = runClips[1];
        }
    }
}
    
