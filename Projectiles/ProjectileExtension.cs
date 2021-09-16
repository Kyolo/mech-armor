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

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            // We check if this projectile is magic or if it has an owner
            if (projectile.magic && projectile.owner == Main.myPlayer)
            {
                Player player = Main.player[projectile.owner];
                MechArmorPlayer mAPlayer = player.GetModPlayer<MechArmorPlayer>();

                if (mAPlayer.MagicalLifeSteal)
                {
                    // NOTE: as I'm using the lifesteal from the vampire knives, I need to compensate for the vanilla behavior
                    projectile.vampireHeal((int)(((float)damage) * mAPlayer.MagicalLifeStealAmount * (1.0f / 0.075f)), projectile.Center);
                }
            }
        }

        public override void PostAI(Projectile projectile)
        {
            // We reset the flag
            LunarBoostedMinion = false;
        }
    }
}
