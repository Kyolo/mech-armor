using System;
using System.IO;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

using MechArmor.Buffs;
using MechArmor.Items.Armor.Tier5;

using Microsoft.Xna.Framework;

namespace MechArmor.Projectiles
{
    public class LunarArmorDrone : ModProjectile
    {

        public const int NEBULAR_FRAME = 1;
        public const int VORTEXIAN_FRAME = 2;
        public const int STARDUSTED_FRAME = 3;
        public const int SOLARITE_FRAME = 4;

        /// <summary>
        /// Tells which lunar drone it is (the 0th, 1st, 2nd, etc.)
        /// </summary>
        public sbyte LunarDroneIndex;

        /// <summary>
        /// Tells where the lunar drone need to point toward
        /// </summary>
        public Vector2 TargetPosition;

        /// <summary>
        /// The mouse update time.
        /// </summary>
        public int MouseUpdateTick;

        /// <summary>
        /// The time this drone takes to shoot.
        /// Can vary depending on the current mode
        /// </summary>
        public int ShootCooldown;

        /// <summary>
        /// The number of tick between world mouse coordinate transmission
        /// </summary>
        private const int MouseUpdateTime = 20;

        /// <summary>
        /// The duration of the animation loop
        /// </summary>
        private const int AnimationLoopDuration = 240;

        /// <summary>
        /// The basic distance of the drones from the player
        /// </summary>
        public const int DroneDistance = 128;

        /// <summary>
        /// If the drone is charged and ready to launch a bolt.
        /// Used by the Full Nebular Armor 3rd state
        /// </summary>
        public bool ManaCharged;

        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            DisplayName.SetDefault("Lunar Drone");
*/
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[Projectile.type] = 5;


            // These below are needed for a minion
            // Denotes that this Projectile is a pet or minion
            Main.projPet[Projectile.type] = true;

        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 52;
            Projectile.height = 32;
            // Makes the minion go through tiles freely
            Projectile.tileCollide = false;

            // Needed so the minion doesn't despawn on collision with enemies or tiles
            Projectile.penetrate = -1;

            // By default they don't do any damage
            Projectile.damage = 0;
            // They can deal damage to enemies, just not always
            Projectile.friendly = true;
            // Position timer
            Projectile.ai[0] = 0;
            // Shooting timer
            Projectile.ai[1] = 0;

            // We don't start knowing which drone it is
            // we default to -1
            LunarDroneIndex = -1;
            TargetPosition = new Vector2(0, 0);
            MouseUpdateTick = 0;
            ShootCooldown = 60;// Default shooting time of 2s
            ManaCharged = false;
        }

        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles()
        {
            return false;
        }

        // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        public override bool MinionContactDamage()
        {
            return Projectile.damage > 0;
        }

        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            MechArmorPlayer mAPlayer = player.GetModPlayer<MechArmorPlayer>();

            #region Active check
            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
            //if (player.dead || !player.active)
            //{
            //    // Is checked in MechArmorPlayer::UpdateDead
            //}

            // We only check if we have enough drones
            if(mAPlayer.LunarDroneCount > LunarDroneIndex)
            {
                // We need this drone, refresh timer
                Projectile.timeLeft = 2;
                // We also increase the value used to handle moving placement
                Projectile.ai[0]++;
                if ((int)Projectile.ai[0] == AnimationLoopDuration)
                    Projectile.ai[0] = 0;

                // And the shooting counter
                Projectile.ai[1]++;
                if ((int)Projectile.ai[1] == ShootCooldown)
                    Projectile.ai[1] = 0;

            }

            #endregion

            #region ControlUpdates

            // Some patterns require knowing the position of the mouse
            // Obviously, only the owner should change it
            if(Main.myPlayer == player.whoAmI)
            {
                TargetPosition = Main.MouseWorld;
                MouseUpdateTick++;
                if(MouseUpdateTick >= MouseUpdateTime)
                {
                    Projectile.netUpdate = true;
                    MouseUpdateTick = 0;
                }
            }

            #endregion

            #region General behavior

            // The drones can have a lot of very different behavior
            // But they (almost) never stray far from their player
            Vector2 offset = new Vector2(0, 0);

            float positionLoopProgression = Projectile.ai[0] / AnimationLoopDuration;
            float droneIndexFloat = ((float)LunarDroneIndex) / mAPlayer.LunarDroneCount;

            float animationVar = droneIndexFloat + positionLoopProgression;

            // Reset damage
            Projectile.damage = 0;
            Projectile.DamageType = DamageClass.Default;

            switch (mAPlayer.LunarDroneMode)
            {
                case LunarDroneModes.Idle:
                    offset.X = (float)Math.Cos(2.0f * Math.PI * animationVar) * DroneDistance * player.direction;
                    offset.Y = (float)Math.Sin(2.0f * Math.PI * animationVar) * DroneDistance * player.direction;
                    break;
                case LunarDroneModes.FollowSwing:
                    // We only extend the range for melee weapons
                    if (player.HeldItem.DamageType == DamageClass.Melee && player.itemAnimation > 0)
                    {
                        // Swords
                        if (player.HeldItem.useStyle == ItemUseStyleID.Swing)
                        {
                            float droneIndexInc = ((float)LunarDroneIndex + 1) / (mAPlayer.LunarDroneCount + 1);
                            offset.X = (float)Math.Cos(player.itemRotation + (Math.PI / 4.0f) * -player.direction) * DroneDistance * 2.0f * droneIndexInc * player.direction;
                            offset.Y = (float)Math.Sin(player.itemRotation + (Math.PI / 4.0f) * -player.direction) * DroneDistance * 2.0f * droneIndexInc * player.direction;


                            Projectile.rotation = player.itemRotation + (float)Math.PI / 4.0f;

                            if (player.direction > 0)
                                Projectile.rotation += (float)Math.PI / 2.0f;

                            Projectile.damage = player.HeldItem.damage;
                            Projectile.DamageType = DamageClass.Melee;
                        }
                        else if(ItemID.Sets.Spears[player.HeldItem.type])
                        {
                            // Somehow the spear animation is reversed ?
                            float animationProgresion = (float)player.itemAnimation/ (float)player.itemAnimationMax;
                            float distanceProgression = DroneDistance * 2 - animationProgresion * DroneDistance;
                            
                            float correctedRotation = player.itemRotation ;
                            float droneIndexInc = ((float)LunarDroneIndex + 1) / (mAPlayer.LunarDroneCount + 1);

                            offset.X = (float)Math.Cos(correctedRotation * -player.direction) * distanceProgression * 2.0f * droneIndexInc * player.direction;
                            offset.Y = (float)Math.Sin(correctedRotation * -player.direction) * distanceProgression * 2.0f * droneIndexInc * player.direction;


                            //Projectile.rotation = player.itemRotation + (float)Math.PI / 4.0f;
                            Projectile.rotation = correctedRotation;

                            if (player.direction > 0)
                                offset.Y *= -1.0f;
                            //    Projectile.rotation += (float)Math.PI / 2.0f;

                            Projectile.damage = player.HeldItem.damage;
                            Projectile.DamageType = DamageClass.Melee;
                        }
                        
                    }
                    else
                    {
                        // Otherwise we stand behind the player
                        offset.X = -player.direction * 32f;
                        offset.Y = player.height - droneIndexFloat * player.height;

                        Projectile.rotation = 0;
                    }

                    Projectile.frame = SOLARITE_FRAME;
                    break;
                case LunarDroneModes.JetpackWings:
                    // They simply stand behind the player in the rough shape of a jetpack
                    if((LunarDroneIndex & 1) == 0)
                    {
                        offset.X = 22.0f + (float)Math.Cos(Math.PI / 3) * DroneDistance / 2.0f * (LunarDroneIndex >> 1);
                        offset.Y = - 24.0f + (float)Math.Sin(Math.PI / 3) * DroneDistance / 2.0f * (LunarDroneIndex >> 1);
                    }
                    else
                    {
                        offset.X = 42.0f + (float)Math.Cos(Math.PI / 3) * DroneDistance / 2.0f * (LunarDroneIndex >> 1);
                        offset.Y = - 23.5f + (float)Math.Sin(Math.PI / 3) * DroneDistance / 2.0f * (LunarDroneIndex >> 1);
                    }

                    offset.X *= - player.direction;

                    Projectile.rotation = -player.direction;
                    Projectile.frame = SOLARITE_FRAME;
                    break;
                case LunarDroneModes.Multishot:
                    //Drone float behind the player in an ellipsis, pointed in the direction of their next target
                    offset.X = -40 + (float)Math.Cos(Math.PI * 2.0f * animationVar) * DroneDistance / 4.0f;
                    offset.Y = -17 + (float)Math.Sin(Math.PI * 2.0f * animationVar) * DroneDistance / 2.0f;

                    offset.X *= player.direction;

                    Projectile.frame = VORTEXIAN_FRAME;

                    if(player.HeldItem.DamageType == DamageClass.Ranged && player.itemAnimation > 0)
                        Projectile.rotation = player.itemRotation + (float)Math.PI / 2.0f;// We point in the same direction than the player's gun
                    break;
                case LunarDroneModes.ProjectileBarrier:
                    // A simple bar behind the player
                    //if(Main.myPlayer == player.whoAmI)
                    {
                        // Only the owner can do that
                        //// Reverse direction the player is looking at
                        Vector2 dir = player.Center - TargetPosition;
                        dir.Normalize();
                        //// Position behind
                        Vector2 back = dir * DroneDistance;
                        //// Actual position of the drone
                        Vector2 droneOffset =
                            dir.RotatedBy(Math.PI / 2.0f * ((LunarDroneIndex & 1) == 1 ? -1 : 1)) * //Rotate the direction by 90° clockwise or counter clockwise
                            DroneDistance * // Maximum distance
                            (LunarDroneIndex >> 1) / (mAPlayer.LunarDroneCount >> 1);// Distance from back to drone position
                            

                        offset = back + droneOffset;

                        Projectile.rotation = dir.ToRotation();

                        // We also make all Projectile arround us disappear
                        for(int i = 0; i < Main.projectile.Length; i++)
                        {
                            Projectile proj = Main.projectile[i];
                            if (proj.hostile)
                            {
                                MechArmorServerConfig conf = ModContent.GetInstance<MechArmorServerConfig>();
                                if (conf.CanAffectProjectile(proj)) {
                                    if ((Projectile.Center - proj.Center).LengthSquared() < Projectile.width * Projectile.width)
                                    {
                                        proj.Kill();
                                    }
                                }
                            }
                        }

                    }
                    Projectile.frame = VORTEXIAN_FRAME;
                    break;
                case LunarDroneModes.ManaAmplifier:
                    // A triangle in front of the player
                    //if(Main.myPlayer == player.whoAmI)
                    {
                        //The direction the player is targeting
                        Vector2 dir = TargetPosition - player.Center;
                        dir.Normalize();

                        float droneRelativePosition = DroneDistance * ((LunarDroneIndex >> 1) + 1) / ((mAPlayer.LunarDroneCount >> 1) + 1);

                        // The "forward" position
                        Vector2 advance = dir * droneRelativePosition * 2.0f;
                        // The "height" of the drone under/over the mid point;
                        Vector2 height = dir.RotatedBy(Math.PI / 2.0f * ((LunarDroneIndex & 1) == 1 ? -1 : 1)) * (DroneDistance - droneRelativePosition);

                        offset = advance + height;

                        Projectile.rotation = dir.ToRotation() + ((LunarDroneIndex & 1) == 1 ? 3.0f : 1.0f) * (float)Math.PI / 4.0f;

                    }
                    Projectile.frame = NEBULAR_FRAME;
                    break;
                case LunarDroneModes.ManaLifeSteal:
                    Vector2 c = (new Vector2(0, -1) * player.height * 1.5f) + //height
                        new Vector2(-player.direction, 0) * DroneDistance / 3.0f;// lateral offset

                    float animationVarAdj = ((float)(LunarDroneIndex % 4)) / 4 + positionLoopProgression;
                    float distance = .25f * ((LunarDroneIndex >> 2) + 1) * DroneDistance;

                    Vector2 crossPosition = new Vector2(
                        (float)Math.Cos(Math.PI * 2.0f * animationVarAdj) * distance,
                        (float)Math.Sin(Math.PI * 2.0f * animationVarAdj) * distance
                    );
                    offset = c + crossPosition;
                    
                    Projectile.rotation = crossPosition.ToRotation() + (float)Math.PI / 2.0f;

                    Projectile.frame = NEBULAR_FRAME;
                    break;
                case LunarDroneModes.SummonBoost:
                    // Boost one summon per drone
                    int minionCount = -1; // Counter for the number of minion found
                    bool minionFound = false;
                    for(int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile otherProj = Main.projectile[i];
                        if(otherProj.owner == Projectile.owner && otherProj.minion && otherProj.active)
                        {
                            minionCount++;
                            if(minionCount == LunarDroneIndex)
                            {
                                // We found the correct minion
                                // Because for once we don't follow the player, we need to compensate
                                offset = -player.Center;
                                offset += otherProj.Center;
                                Projectile.rotation = otherProj.rotation;

                                // We boost the damage of the minion
                                otherProj.GetGlobalProjectile<ProjectileExtension>().LunarBoostedMinion = true;

                                minionFound = true;
                                break;
                            }
                        }
                    }

                    // If we didn't find any minion
                    if (!minionFound)
                    {
                        // We'll hover in circle over our player
                        offset = new Vector2(0, player.height * -2.0f)
                            + new Vector2(
                                (float)Math.Cos(2.0f * Math.PI * animationVar) * DroneDistance,
                                (float)Math.Sin(2.0f * Math.PI * animationVar) * DroneDistance / 2.0f// The circle is wider that it is tall (aka an ellipsis).
                            );

                        Projectile.rotation = 0;
                    }

                    Projectile.frame = STARDUSTED_FRAME;
                    break;
                case LunarDroneModes.Confusion:
                    // Fly arround the player (like in idle) and shoot nearby enemy for a bit of damage
                    // also inflict confusion
                    offset.X = (float)Math.Cos(2.0f * Math.PI * animationVar) * DroneDistance;
                    offset.Y = (float)Math.Sin(2.0f * Math.PI * animationVar) * DroneDistance;

                    // 

                    // If this drone is ready to shoot
                    if ((int)Projectile.ai[1] == 0)
                    {

                        int target = AcquireTarget(DroneDistance * 2);

                        if (target >= 0)
                        {
                            // We found a target
                            Vector2 direction = Main.npc[target].Center - Projectile.Center;
                            direction.Normalize();
                            // We only shoot on the owner's game
                            if (Main.myPlayer == player.whoAmI)
                            {
                                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, direction * player.maxMinions, ProjectileID.NanoBullet, 5 * player.maxMinions, 10, Projectile.owner);

                                // And then we sync the new Projectile
                                //Projectile.netUpdate = true;// no need for us, we don't change anything
                                Main.projectile[proj].netUpdate = true;
                                Main.projectile[proj].timeLeft = 300;// 5s lifetime
                            }
                            // We also change our orientation toward our target
                            Projectile.rotation = direction.ToRotation() + (float)Math.PI / 4.0f;

                        }
                    }

                    Projectile.frame = STARDUSTED_FRAME;
                    break;
                case LunarDroneModes.ProjectileShield:
                    // Nothing fancy here
                    // The magic happens in MechArmorPlayer::PostUpdate
                    {
                        offset.X = (float)Math.Cos(Math.PI * animationVar * ((LunarDroneIndex & 1) == 1 ? -2.0f : 2.0f)) * DroneDistance;
                        offset.Y = (float)Math.Sin(Math.PI * animationVar * ((LunarDroneIndex & 1) == 1 ? -2.0f : 2.0f)) * DroneDistance;

                        Vector2 dir = player.Center - (player.Center + offset);
                        dir.Normalize();
                        Projectile.rotation = dir.ToRotation() + (float)Math.PI / 2.0f;

                        Projectile.frame = SOLARITE_FRAME;
                    }
                    break;
                case LunarDroneModes.ManaShield:
                    // Ellipsis
                    offset.X = (float)Math.Cos(2.0f * Math.PI * animationVar ) * DroneDistance * player.direction * 0.5f;
                    offset.Y = (float)Math.Sin(2.0f * Math.PI * animationVar ) * DroneDistance * player.direction;
                    // Behind the player
                    offset.X += -player.direction * DroneDistance * 0.75f;

                    Projectile.rotation = (Projectile.Center - offset - player.Center).ToRotation();// + (float)Math.PI / 4.0f;

                    // We may need to fire a bolt
                    if (ManaCharged)
                    {
                        // We discharge it
                        ManaCharged = false;
                        //TODO : change this for a better thing
                        // The owner also need to send the Projectile
                        if (Main.myPlayer == player.whoAmI)
                        {
                            int target = AcquireTarget(DroneDistance * 4);
                            if (target != -1)
                            {
                                // We found a target
                                Vector2 direction = Main.npc[target].Center - Projectile.Center;
                                direction.Normalize();
                                // We have a target
                                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, direction * 64.0f, ProjectileID.DiamondBolt, 100, 0, player.whoAmI, 1);
                                Main.projectile[proj].netUpdate = true;
                                Main.projectile[proj].timeLeft = 300;// 5s lifetime
                            }
                        }
                    }
                    Projectile.frame = NEBULAR_FRAME;
                    break;
                case LunarDroneModes.JetpackPlatform:
                    // Two drones under, the rest like regular jetpack
                    if(LunarDroneIndex < 2)
                    {
                        offset.Y = player.height;
                        offset.X = ((LunarDroneIndex & 1) == 0 ? .5f : -.5f) * Projectile.width;
                    }
                    else
                    {
                        int adjustedDroneIndex = LunarDroneIndex - 2;
                        if ((adjustedDroneIndex & 1) == 0)
                        {
                            offset.X = 22.0f + (float)Math.Cos(Math.PI / 3) * DroneDistance / 2.0f * (adjustedDroneIndex >> 1);
                            offset.Y = -24.0f + (float)Math.Sin(Math.PI / 3) * DroneDistance / 2.0f * (adjustedDroneIndex >> 1);
                        }
                        else
                        {
                            offset.X = 42.0f + (float)Math.Cos(Math.PI / 3) * DroneDistance / 2.0f * (adjustedDroneIndex >> 1);
                            offset.Y = -23.5f + (float)Math.Sin(Math.PI / 3) * DroneDistance / 2.0f * (adjustedDroneIndex >> 1);
                        }
                        offset.X *= -player.direction;
                    }

                    Projectile.rotation = 0;
                    Projectile.frame = STARDUSTED_FRAME;
                    break;

                
            }
            // We move the Projectile to the correct position
            // we also center the coordinates
            Projectile.Center = player.Center + offset;
            #endregion

            #region Animation and visuals

            // This is a simple "loop through all frames from top to bottom" animation
            //int frameSpeed = 5;
            //Projectile.frameCounter++;
            //if (Projectile.frameCounter >= frameSpeed)
            //{
            //    Projectile.frameCounter = 0;
            //    Projectile.frame++;
            //    if (Projectile.frame >= Main.projFrames[Projectile.type])
            //    {
            //        Projectile.frame = 0;
            //    }
            //}

            if (ManaCharged)
                Dust.NewDust(Projectile.Center, 8, 8, 255, 0, 0, 0, Color.Pink);

            switch (Projectile.frame)
            {
                case SOLARITE_FRAME:
                    Lighting.AddLight(Projectile.Center, Color.DarkOrange.ToVector3());
                    break;
                case STARDUSTED_FRAME:
                    Lighting.AddLight(Projectile.Center, Color.Cyan.ToVector3());
                    break;
                case VORTEXIAN_FRAME:
                    Lighting.AddLight(Projectile.Center, Color.LightSeaGreen.ToVector3());
                    break;
                case NEBULAR_FRAME:
                    Lighting.AddLight(Projectile.Center, Color.Pink.ToVector3());
                    break;
                default:
                    Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);
                    break;
            }
            #endregion
        }

        /// <summary>
        /// Find the nearest attackable NPC
        /// </summary>
        /// <returns>The index of the NPC choosen as a target.</returns>
        private int AcquireTarget(float maximumDistance)
        {
            int target = -1;
            float oldDistance = float.MaxValue;
            float maximumDistanceSq = maximumDistance * maximumDistance;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy())
                {
                    // If the NPC is relatively near
                    float newDistance = Vector2.DistanceSquared(npc.Center, Projectile.Center);
                    if (newDistance < oldDistance && newDistance < maximumDistanceSq)
                    {
                        target = i;
                        oldDistance = newDistance;
                    }
                }
            }

            return target;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(LunarDroneIndex);
            writer.WriteVector2(TargetPosition);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            LunarDroneIndex = reader.ReadSByte();
            TargetPosition = reader.ReadVector2();
        }
    }
}
