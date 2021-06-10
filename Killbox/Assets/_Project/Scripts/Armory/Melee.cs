using Gisha.Killbox.NPC;
using UnityEngine;

namespace Gisha.Killbox.Armory
{
    public class Melee : Weapon
    {
        public override void Use(Enemy targetEnemy)
        {
            Debug.Log("Smash!");

            if (targetEnemy != null)
                Destroy(targetEnemy.gameObject);

            else
                Debug.Log("Fake swing.");
        }
    }
}
