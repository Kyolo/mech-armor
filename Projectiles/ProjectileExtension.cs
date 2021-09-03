using System;

using Terraria;
using Terraria.ModLoader;

namespace MechArmor.Projectiles
{
    public class ProjectileExtension : GlobalProjectile
    {
        // This needs to be included in every Projectile
        public override bool InstancePerEntity => true;

        /// <summary>
        /// Used to decide if this minion is boosted by a Lunar Drone
        /// </summary>
        public bool LunarBoostedMinion;

        public override void SetDefaults(Projectile projectile)
        {
            LunarBoostedMinion = false;
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (LunarBoostedMinion)
                damage = (int)(damage * 1.5f);
        }

        public override void PostAI(Projectile projectile)
        {
            // We reset the flag
            LunarBoostedMinion = false;
        }
    }
}
