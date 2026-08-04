using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //serializefield allows you edit speed in the unity-it creates field for it there

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rigidBody;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _characterBody;

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

    private void Move()
    {
        rigidBody.MovePosition(rigidBody.position + movement * (moveSpeed * Time.fixedDeltaTime));
        bool characterIsWalking = movement.magnitude > 0f;
        _animator.SetBool("isWalking", characterIsWalking);

        bool flipSprite = movement.x < 0f;
        _characterBody.flipX = flipSprite;
    }
}
