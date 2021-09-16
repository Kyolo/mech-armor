using System;
using System.IO;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            DisplayName.SetDefault("Lunar Drone");
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[projectile.type] = 5;


            // These below are needed for a minion
            // Denotes that this projectile is a pet or minion
            Main.projPet[projectile.type] = true;

        }

        public sealed override void SetDefaults()
        {
            projectile.width = 52;
            projectile.height = 32;
            // Makes the minion go through tiles freely
            projectile.tileCollide = false;

            // Needed so the minion doesn't despawn on collision with enemies or tiles
            projectile.penetrate = -1;

            // By default they don't do any damage
            projectile.damage = 0;
            // They can deal damage to enemies, just not always
            projectile.friendly = true;
            // Position timer
            projectile.ai[0] = 0;
            // Shooting timer
            projectile.ai[1] = 0;

            // We don't start knowing which drone it is
            // we default to -1
            LunarDroneIndex = -1;
            TargetPosition = new Vector2(0, 0);
            MouseUpdateTick = 0;
            ShootCooldown = 120;// Default shooting time of 2s
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
            return projectile.damage > 0;
        }

        public override void AI()
        {

            Player player = Main.player[projectile.owner];
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
                projectile.timeLeft = 2;
                // We also increase the value used to handle moving placement
                projectile.ai[0]++;
                if ((int)projectile.ai[0] == AnimationLoopDuration)
                    projectile.ai[0] = 0;

                // And the shooting counter
                projectile.ai[1]++;
                if ((int)projectile.ai[1] == ShootCooldown)
                    projectile.ai[1] = 0;

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
                    projectile.netUpdate = true;
                    MouseUpdateTick = 0;
                }
            }

            #endregion

            #region General behavior

            // The drones can have a lot of very different behavior
            // But they (almost) never stray far from their player
            Vector2 offset = new Vector2(0, 0);

            float positionLoopProgression = projectile.ai[0] / AnimationLoopDuration;
            float droneIndexFloat = ((float)LunarDroneIndex) / mAPlayer.LunarDroneCount;

            float animationVar = droneIndexFloat + positionLoopProgression;

            // Reset damage
            projectile.damage = 0;
            projectile.melee = false;

            switch (mAPlayer.LunarDroneMode)
            {
                case LunarDroneModes.Idle:
                    offset.X = (float)Math.Cos(2.0f * Math.PI * animationVar) * DroneDistance * player.direction;
                    offset.Y = (float)Math.Sin(2.0f * Math.PI * animationVar) * DroneDistance * player.direction;
                    break;
                case LunarDroneModes.FollowSwing:
                    //player.itemRotation
                    // We only extend the range for melee weapons
                    if (player.HeldItem.melee && player.HeldItem.useStyle == 1 && player.itemAnimation > 0)
                    {
                        float droneIndexInc = ((float)LunarDroneIndex + 1) / (mAPlayer.LunarDroneCount + 1);
                        offset.X = (float)Math.Cos(player.itemRotation + (Math.PI / 4.0f) * -player.direction) * DroneDistance * 2.0f * droneIndexInc * player.direction;
                        offset.Y = (float)Math.Sin(player.itemRotation + (Math.PI / 4.0f) * -player.direction) * DroneDistance * 2.0f * droneIndexInc * player.direction;


                        projectile.rotation = player.itemRotation + (float)Math.PI / 4.0f;

                        if (player.direction > 0)
                            projectile.rotation += (float)Math.PI / 2.0f;

                        projectile.damage = player.HeldItem.damage;
                        projectile.melee = true;
                    }
                    else
                    {
                        // Otherwise we stand behind the player
                        offset.X = player.direction * 32f;
                        offset.Y = player.height - droneIndexFloat * player.height;

                        projectile.rotation = 0;
                    }

                    projectile.frame = SOLARITE_FRAME;
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

                    projectile.frame = SOLARITE_FRAME;
                    break;
                case LunarDroneModes.Multishot:
                    //Drone float behind the player in an ellipsis, pointed in the direction of their next target
                    offset.X = -40 + (float)Math.Cos(Math.PI * 2.0f * animationVar) * DroneDistance / 4.0f;
                    offset.Y = -17 + (float)Math.Sin(Math.PI * 2.0f * animationVar) * DroneDistance / 2.0f;

                    offset.X *= player.direction;

                    projectile.frame = VORTEXIAN_FRAME;

                    if(player.HeldItem.ranged && player.itemAnimation > 0)
                        projectile.rotation = player.itemRotation + (float)Math.PI / 2.0f;// We point in the same direction than the player's gun
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

                        projectile.rotation = dir.ToRotation();
                    }
                    projectile.frame = VORTEXIAN_FRAME;
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

                        projectile.rotation = dir.ToRotation() + ((LunarDroneIndex & 1) == 1 ? 3.0f : 1.0f) * (float)Math.PI / 4.0f;

                    }
                    projectile.frame = NEBULAR_FRAME;
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
                    
                    projectile.rotation = crossPosition.ToRotation() + (float)Math.PI / 2.0f;

                    projectile.frame = NEBULAR_FRAME;
                    break;
                case LunarDroneModes.SummonBoost:
                    // Boost one summon per drone
                    int minionCount = -1; // Counter for the number of minion found
                    bool minionFound = false;
                    for(int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile otherProj = Main.projectile[i];
                        if(otherProj.owner == projectile.owner && otherProj.minion)
                        {
                            minionCount++;
                            if(minionCount == LunarDroneIndex)
                            {
                                // We found the correct minion
                                // Because for once we don't follow the player, we need to compensate
                                offset = -player.Center;
                                offset += otherProj.Center;
                                projectile.rotation = otherProj.rotation;

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
                    }

                    projectile.frame = STARDUSTED_FRAME;
                    break;
                case LunarDroneModes.Confusion:
                    // Fly arround the player (like in idle) and shoot nearby enemy for a bit of damage
                    // also inflict confusion
                    offset.X = (float)Math.Cos(2.0f * Math.PI * animationVar) * DroneDistance * player.direction;
                    offset.Y = (float)Math.Sin(2.0f * Math.PI * animationVar) * DroneDistance * player.direction;

                    // 

                    // If this drone is ready to shoot
                    if ((int)projectile.ai[1] == 0)
                    {

                        int target = AcquireTarget(DroneDistance * 2);

                        if (target >= 0)
                        {
                            // We found a target
                            Vector2 direction = Main.npc[target].Center - projectile.Center;
                            direction.Normalize();
                            // We only shoot on the owner's game
                            if (Main.myPlayer == player.whoAmI)
                            {
                                int proj = Projectile.NewProjectile(projectile.Center, direction * player.maxMinions, ProjectileID.NanoBullet, 5 * player.maxMinions, 10, projectile.owner);

                                // And then we sync the new projectile
                                //projectile.netUpdate = true;// no need for us, we don't change anything
                                Main.projectile[proj].netUpdate = true;
                                Main.projectile[proj].timeLeft = 300;// 5s lifetime
                            }
                            // We also change our orientation toward our target
                            projectile.rotation = direction.ToRotation() + (float)Math.PI / 4.0f;

                        }
                    }

                    projectile.frame = STARDUSTED_FRAME;
                    break;
                case LunarDroneModes.ProjectileShield:
                    // Nothing fancy here
                    // The magic happens in MechArmorPlayer::PostUpdate
                    offset.X = (float)Math.Cos(Math.PI * animationVar * ((LunarDroneIndex & 1) == 1 ? -2.0f : 2.0f)) * DroneDistance * player.direction;
                    offset.Y = (float)Math.Sin(Math.PI * animationVar * ((LunarDroneIndex & 1) == 1 ? -2.0f : 2.0f)) * DroneDistance * player.direction;
                    projectile.rotation = (player.Center - projectile.Center).ToRotation() + (float)Math.PI / 4.0f;
                    
                    projectile.frame = SOLARITE_FRAME;
                    break;
                case LunarDroneModes.ManaShield:
                    // Ellipsis
                    offset.X = (float)Math.Cos(2.0f * Math.PI * animationVar ) * DroneDistance * player.direction * 0.5f;
                    offset.Y = (float)Math.Sin(2.0f * Math.PI * animationVar ) * DroneDistance * player.direction;
                    // Behind the player
                    offset.X += -player.direction * DroneDistance * 0.75f;

                    projectile.rotation = ((projectile.Center + offset) - player.Center).ToRotation() + (float)Math.PI / 4.0f;

                    // We may need to fire a bolt
                    if (ManaCharged)
                    {
                        // We discharge it
                        ManaCharged = false;

                        // The owner also need to send the projectile
                        if (Main.myPlayer == player.whoAmI)
                        {
                            int target = AcquireTarget(DroneDistance * 4);
                            if (target != -1)
                            {
                                // We found a target
                                Vector2 direction = Main.npc[target].Center - projectile.Center;
                                direction.Normalize();
                                // We have a target
                                int proj = Projectile.NewProjectile(projectile.Center, direction * 64.0f, ProjectileID.DiamondBolt, 100, 0, player.whoAmI, 1);
                                Main.projectile[proj].netUpdate = true;
                                Main.projectile[proj].timeLeft = 300;// 5s lifetime
                            }
                        }
                    }
                    projectile.frame = NEBULAR_FRAME;
                    break;
                case LunarDroneModes.JetpackPlatform:
                    // Two drones under, the rest like regular jetpack
                    if(LunarDroneIndex < 2)
                    {
                        offset.Y = player.height;
                        offset.X = player.direction * projectile.width * 1.5f;
                    }
                    else
                    {
                        if ((LunarDroneIndex >> 1 & 1) == 0)
                        {
                            offset.X = 22.0f + (float)Math.Cos(Math.PI / 3) * DroneDistance / 2.0f * (LunarDroneIndex >> 2);
                            offset.Y = -24.0f + (float)Math.Sin(Math.PI / 3) * DroneDistance / 2.0f * (LunarDroneIndex >> 2);
                        }
                        else
                        {
                            offset.X = 42.0f + (float)Math.Cos(Math.PI / 3) * DroneDistance / 2.0f * (LunarDroneIndex >> 2);
                            offset.Y = -23.5f + (float)Math.Sin(Math.PI / 3) * DroneDistance / 2.0f * (LunarDroneIndex >> 2);
                        }
                    }
                    projectile.frame = STARDUSTED_FRAME;
                    break;

                
            }
            // We move the projectile to the correct position
            // we also center the coordinates
            projectile.Center = player.Center + offset;
            #endregion

            #region Animation and visuals

            // This is a simple "loop through all frames from top to bottom" animation
            //int frameSpeed = 5;
            //projectile.frameCounter++;
            //if (projectile.frameCounter >= frameSpeed)
            //{
            //    projectile.frameCounter = 0;
            //    projectile.frame++;
            //    if (projectile.frame >= Main.projFrames[projectile.type])
            //    {
            //        projectile.frame = 0;
            //    }
            //}

            if (ManaCharged)
                Dust.NewDust(projectile.Center, 8, 8, 255);

            switch (projectile.frame)
            {
                case SOLARITE_FRAME:
                    Lighting.AddLight(projectile.Center, Color.DarkOrange.ToVector3());
                    break;
                case STARDUSTED_FRAME:
                    Lighting.AddLight(projectile.Center, Color.Cyan.ToVector3());
                    break;
                case VORTEXIAN_FRAME:
                    Lighting.AddLight(projectile.Center, Color.LightSeaGreen.ToVector3());
                    break;
                case NEBULAR_FRAME:
                    Lighting.AddLight(projectile.Center, Color.Pink.ToVector3());
                    break;
                default:
                    Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
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
                    float newDistance = Vector2.DistanceSquared(npc.Center, projectile.Center);
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
