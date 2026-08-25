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
