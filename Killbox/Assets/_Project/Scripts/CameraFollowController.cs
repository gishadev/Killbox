using UnityEngine;

namespace Gisha.Killbox.Core
{
    public class CameraFollowController : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private float followSpeed = 1f;
        [SerializeField] private Vector3 offset;

        private void FixedUpdate()
        {
            Vector3 newPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * followSpeed);
        }
    }
}
