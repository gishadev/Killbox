using Gisha.Killbox.NPC;
using System.Linq;
using UnityEngine;

namespace Gisha.Killbox.Armory
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private float minAimRadius;

        public float MinAimRadius => minAimRadius;

        public abstract void Use();
        public Enemy FindNearestTarget(Vector3 playerPos, Enemy[] nearbyEnemies)
        {
            return nearbyEnemies
                .OrderBy(x => (x.transform.position - playerPos).sqrMagnitude)
                .FirstOrDefault();
        }
    }
}
