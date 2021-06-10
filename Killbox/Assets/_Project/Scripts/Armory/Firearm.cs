using Gisha.Killbox.NPC;
using UnityEngine;

namespace Gisha.Killbox.Armory
{
    public class Firearm : Weapon
    {
        public override void Use(Enemy targetEnemy)
        {
            Debug.Log("Shoot");

            if (targetEnemy != null)
                Destroy(targetEnemy.gameObject);

            else
                Debug.Log("Fake shot.");
        }
    }
}
