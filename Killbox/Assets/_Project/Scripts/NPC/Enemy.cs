using UnityEngine;

namespace Gisha.Killbox.NPC
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        Transform _target;
        Rigidbody _rb;



        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            MoveTowardsPlayer();
        }

        private void OnDestroy()
        {
            FindObjectOfType<WaveManager>().OnEnemyDestroy(gameObject);
        }

        private void MoveTowardsPlayer()
        {
            var dir = (_target.position - transform.position).normalized;
            _rb.velocity = dir * moveSpeed;
        }
    }
}
