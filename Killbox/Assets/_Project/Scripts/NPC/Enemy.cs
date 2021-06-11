using Gisha.Killbox.Core;
using UnityEngine;

namespace Gisha.Killbox.NPC
{
    public class Enemy : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed;
        [Header("Damaging")]
        [SerializeField] private float dmgAreaRadius;
        [SerializeField] private float dmgAreaDistance;

        LayerMask _whatIsSolid;
        Transform _target;
        Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _whatIsSolid = 1 << LayerMask.NameToLayer("Solid");
        }

        private void Update()
        {
            CheckForPlayer();
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
            transform.rotation = Quaternion.LookRotation(dir);
            _rb.velocity = dir * moveSpeed;
        }

        private void CheckForPlayer()
        {
            // Рэйкаст для обнаружение игрока.
            bool isRaycastedSolid = Physics.SphereCast(transform.position, dmgAreaRadius, transform.forward, out RaycastHit hitInfo, dmgAreaDistance, _whatIsSolid);
            // Нанесение урона.
            if (isRaycastedSolid && hitInfo.collider.CompareTag("Player"))
                hitInfo.collider.GetComponent<PlayerController>().Die();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, dmgAreaRadius);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position + transform.forward * dmgAreaDistance, dmgAreaRadius);
        }
    }
}
