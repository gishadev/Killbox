using Gisha.Killbox.Core;
using Gisha.Killbox.NPC;
using UnityEngine;

namespace Gisha.Killbox.Armory
{
    public class Firearm : Weapon
    {
        public override void Use(PlayerController player, Vector3 attackDirection)
        {
            Debug.Log("Shoot");

            var hitInfo = SolidRaycast(player.transform.position, attackDirection, MinAimRadius * 2f);

            if (hitInfo.collider != null && hitInfo.collider.CompareTag("Enemy"))
                Destroy(hitInfo.collider.gameObject);
        }
    }
}
