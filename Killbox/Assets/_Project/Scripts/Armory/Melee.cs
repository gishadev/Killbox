using Gisha.Killbox.Core;
using Gisha.Killbox.NPC;
using UnityEngine;

namespace Gisha.Killbox.Armory
{
    public class Melee : Weapon
    {
        public override void Use(PlayerController player, Vector3 attackDirection)
        {
            Debug.Log("Smash!");

            var hitInfo = SolidRaycast(player.transform.position, attackDirection, MinAimRadius);

            if (hitInfo.collider != null && hitInfo.collider.CompareTag("Enemy"))
                Destroy(hitInfo.collider.gameObject);
        }
    }
}
