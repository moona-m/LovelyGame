using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent _agent;
    GameObject _target;
    [SerializeField] AudioClip _deathSound;
    EntityHealth _entityHealth;

    void Awake()
    {
        _entityHealth = GetComponent<EntityHealth>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
    }

    void Start()
    {
        _entityHealth.OnDeath += DestroyEnemy;
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        _agent.SetDestination(_target.transform.position);

        if (_agent.velocity.sqrMagnitude > 0.01f)
        {
            Vector3 direction = _agent.velocity;

            if (direction.x != 0)
            {
                transform.localScale = new Vector3(
                    Mathf.Abs(transform.localScale.x) * Mathf.Sign(direction.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
        }
    }

    void OnDisable()
    {
        _entityHealth.OnDeath -= DestroyEnemy;
    }

    public void DestroyEnemy()
    {
        AudioManager.Instance.PlayAudio(_deathSound, AudioManager.SoundType.SFX, 1.0f, false);
        Destroy(gameObject);
    }

}
