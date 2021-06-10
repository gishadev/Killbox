using Gisha.Killbox.Armory;
using Gisha.Killbox.NPC;
using System.Linq;
using UnityEngine;

namespace Gisha.Killbox.Core
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private Joystick joystick;
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 10f;
        [Header("Armory")]
        [SerializeField] private Weapon[] weapons = new Weapon[3];

        public Weapon SelectedWeapon => weapons[_selectedWeaponIndex];
        public Enemy TargetEnemy { get; private set; }
        public Vector3 MoveInput => new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;

        int _selectedWeaponIndex = 0;
        Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            UpdateWeapon();
        }

        private void Update()
        {
            AutoAim();

            if (TargetEnemy != null)
            {
                var dirToEnemy = (TargetEnemy.transform.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(dirToEnemy);
            }
            else if (MoveInput.magnitude > 0)
                transform.rotation = Quaternion.LookRotation(MoveInput);
        }

        private void FixedUpdate()
        {
            _rb.velocity = MoveInput * moveSpeed;
        }

        public void Attack()
        {
            SelectedWeapon.Use(TargetEnemy);
        }

        public void SelectNextWeapon(int step)
        {
            int rawIndex = _selectedWeaponIndex;

            // Clamping.
            if (Mathf.Abs(step) > 1)
            {
                Debug.LogError("Absolute step cannot be more than 1");
                step = Mathf.Clamp(step, -1, 1);
            }

            rawIndex += step;

            if (rawIndex > 2)
                rawIndex = 0;
            else if (rawIndex < 0)
                rawIndex = 2;

            int initIndex = rawIndex;
            while (weapons[rawIndex] == null)
            {
                Debug.Log(rawIndex);
                rawIndex += step;

                if (rawIndex > 2)
                    rawIndex = 0;
                else if (rawIndex < 0)
                    rawIndex = 2;

                if (rawIndex == initIndex)
                {
                    Debug.LogError("There are no weapons!");
                    return;
                }
            }

            _selectedWeaponIndex = rawIndex;
            UpdateWeapon();
        }

        private void UpdateWeapon()
        {
            foreach (var weap in weapons)
            {
                if (weap == null)
                    continue;

                weap.gameObject.SetActive(false);
            }

            SelectedWeapon.gameObject.SetActive(true);
        }

        private void AutoAim()
        {
            Enemy[] nearbyEnemies = Physics.OverlapSphere(transform.position, SelectedWeapon.MinAimRadius)
                .Where(x => x.CompareTag("Enemy"))
                .Select(x => x.GetComponent<Enemy>())
                .ToArray();

            TargetEnemy = SelectedWeapon.FindNearestTarget(transform.position, nearbyEnemies);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, SelectedWeapon.MinAimRadius);

            if (TargetEnemy != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(TargetEnemy.transform.position, 1f);
            }
        }
    }
}
