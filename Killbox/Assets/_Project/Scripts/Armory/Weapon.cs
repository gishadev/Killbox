using Gisha.Killbox.Core;
using Gisha.Killbox.NPC;
using System.Linq;
using UnityEngine;

namespace Gisha.Killbox.Armory
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private float minAimRadius;

        public float MinAimRadius => minAimRadius;

        LayerMask _whatIsSolid;

        private void Start()
        {
            _whatIsSolid = 1 << LayerMask.NameToLayer("Solid");
        }

        public abstract void Use(PlayerController player, Vector3 attackDirection);

        public Enemy FindNearestTarget(Vector3 playerPos, Enemy[] nearbyEnemies)
        {
            return nearbyEnemies
                .OrderBy(x => (x.transform.position - playerPos).sqrMagnitude)
                .FirstOrDefault();
        }

        public RaycastHit SolidRaycast(Vector3 origin, Vector3 direction, float maxRayDist)
        {
            Physics.Raycast(origin, direction, out RaycastHit hitInfo, maxRayDist, _whatIsSolid);
            return hitInfo;
        }
    }
}
