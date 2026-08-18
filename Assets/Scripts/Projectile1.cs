using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;


public class Projectile1 : MonoBehaviour
{
    [SerializeField] float _travelSpeed;
    [SerializeField] float _damage;
    [SerializeField] Rigidbody2D _rb;

    public void IntializeProjectile(Vector2 direction)
    {
        Launch(direction);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            DestroyProjectile();
        }
    }

    void Launch(Vector2 direction)
    {
        Vector2 movement = direction.normalized * _travelSpeed;
        _rb.linearVelocity = movement;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
