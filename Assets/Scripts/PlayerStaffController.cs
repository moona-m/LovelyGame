using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStaffController : MonoBehaviour
{
    public InputActionReference fireAction;
    public InputActionReference attackFireAction;
    public Camera playerCamera;
    public float damage = 25f;
    public float range = 100f;

    [SerializeField] Projectile _projectile;
    [SerializeField] Projectile1 _projectile1;
    [SerializeField] AudioClip _shootSound;
    [SerializeField] Transform _tip;
    [SerializeField] float _fireRate;
    [SerializeField] float _fireRate1;
    float _nextFireTime;
    Vector2 _lookDirection;


    private void OnEnable()
    {
        fireAction.action.Enable();
        attackFireAction.action.Enable();
    }

    private void OnDisable()
    {
        fireAction.action.Disable();
        attackFireAction.action.Disable();
    }

    void Update()
    {


        if (Time.timeScale == 0f)
            return;

        SetLookDirection();
        RotateStaff();

        if (fireAction.action.IsPressed() && Time.time >= _nextFireTime)
        {
            Shoot();
            nextFireRate();
            Debug.Log("FIRE BUTTON PRESSED!");

        }

        if (attackFireAction.action.IsPressed() && Time.time >= _nextFireTime)
        {
            Shoot1();
            nextFireRate();
        }
    }

    private void nextFireRate()
    {
        _nextFireTime = Time.time + 1f / _fireRate;
    }
    // Update is called once per frame
    void RotateStaff()
    {

        float angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void ShootBase()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        AudioManager.Instance.PlayAudio(_shootSound, AudioManager.SoundType.SFX, 0.4f, false);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {

            Debug.Log("Hit: " + hit.collider.name);

        }
        else
        {
            Debug.Log("Missed");
        }
    }

    private void Shoot()
    {
        ShootBase();
        Projectile newProjectile = Instantiate(_projectile, _tip.position, Quaternion.identity);
        newProjectile.IntializeProjectile(_lookDirection);
    }

    private void Shoot1()
    {
        ShootBase();
        Projectile1 newProjectile = Instantiate(_projectile1, _tip.position, Quaternion.identity);
        newProjectile.IntializeProjectile(_lookDirection);
    }

    void SetLookDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _lookDirection = (mousePosition - (Vector2)transform.position).normalized;
    }


}
