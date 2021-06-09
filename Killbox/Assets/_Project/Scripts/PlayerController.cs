using UnityEngine;

namespace Gisha.Killbox.Core
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private Joystick joystick;
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 10f;

        public Vector3 MoveInput => new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (MoveInput.magnitude > 0)
                transform.rotation = Quaternion.LookRotation(MoveInput);
        }

        private void FixedUpdate()
        {
            _rb.velocity = MoveInput * moveSpeed;
        }

        public void Attack()
        {
            Debug.Log("Attack");
        }
    }
}
