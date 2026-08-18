
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private static readonly int IsWalkingHash = Animator.StringToHash("isWalking");
    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rigidBody;


    //serializefield allows you edit speed in the unity-it creates field for it there
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _characterBody;
    [SerializeField] AudioClip _footstep;


    float _nextFootstepAudio = 0f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

    }


    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        //Debug.Log(movement.x);
    }

    void HandleWalkingSounds()
    {
        if (Time.time >= _nextFootstepAudio)
        {
            AudioManager.Instance.PlayAudio(_footstep, AudioManager.SoundType.SFX, 1f, false);

            float audioFrequency = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / 2f;
            _nextFootstepAudio = Time.time + audioFrequency;
        }
    }
    private void Move()
    {
        rigidBody.MovePosition(rigidBody.position + movement * (moveSpeed * Time.fixedDeltaTime));
        bool characterIsWalking = movement.magnitude > 0f;
        _animator.SetBool(IsWalkingHash, characterIsWalking);

        if (characterIsWalking)
        {
            HandleWalkingSounds();
        }

        bool flipSprite = movement.x < 0f;
        _characterBody.flipX = flipSprite;
    }
}
