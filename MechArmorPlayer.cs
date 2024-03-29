﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;

using MechArmor.Buffs;
using MechArmor.Config;
using MechArmor.Projectiles;
using MechArmor.Items.Armor.Tier5;

namespace MechArmor
{
    class MechArmorPlayer : ModPlayer
    {

        #region Fields

        /// <summary>
        /// The state of the current armor.
        /// State effect depend on armor;
        /// </summary>
        public byte ArmorState;

        /// <summary>
        /// The type of the current armor state
        /// </summary>
        public byte ArmorStateType;

        /// <summary>
        /// The type of the previous current armor state
        /// </summary>
        public byte PreviousArmorStateType;

		/// <summary>
		/// The number of states from the currently equiped armor
		/// </summary>
		public byte MaxArmorStates;



        /// <summary>
        /// Is on cooldown between changes ?
        /// </summary>
        public bool IsArmorOnCooldown;

        /// <summary>
        /// The duration in seconds of the armor cooldown when the change is triggered
        /// </summary>
        public int ArmorCooldownDuration;

        /// <summary>
        /// The armor cooldown modifier.
        /// </summary>
        public float ArmorCooldownDurationModifier;



        /// <summary>
        /// Is on warmup after a change ?
        /// </summary>
        public bool IsArmorOnWarmup;

        /// <summary>
        /// The duration in seconds of the armor warmup
        /// </summary>
        public int ArmorWarmupDuration;

        /// <summary>
        /// The armor warmup modifier.
        /// </summary>
        public float ArmorWarmupDurationModifier;



        /// <summary>
        /// Percent of damage that should be inflicted on the user's mana instead of their health
        /// </summary>
        public float MagicDamageAbsorptionAmount;

        /// <summary>
        /// If we need to absorb damage
        /// </summary>
        public bool MagicDamageAbsorption;

        /// <summary>
        /// The amount of damage absorbed by the MagicDamageAbsorption
        /// </summary>
        public int MagicDamageAbsorbed;



        /// <summary>
        /// If we are lifestealing with magical attacks
        /// </summary>
        public bool MagicalLifeSteal;

        /// <summary>
        /// The percent of damage healed by lifesteal
        /// </summary>
        public float MagicalLifeStealAmount;



        /// <summary>
        /// Modify the UseTime of a Magic Weapon
        /// </summary>
        public float MagicUseTimeModifier;

        /// <summary>
        /// Modify the UseTime of a Ranged Weapon
        /// </summary>
        public float RangedUseTimeModifier;



        /// <summary>
        /// If we need to attract nearby enemy projectiles
        /// </summary>
        public bool ProjectileAttractor;

        /// <summary>
        /// The range of the projectile attractor;
        /// </summary>
        public float ProjectileAttractorRange;



        /// <summary>
        /// The amount of lunar drones following this player.
        /// Used by the lunar and proto-lunar armors
        /// </summary>
        public byte LunarDroneCount;

        /// <summary>
        /// The mode of the lunar 
        /// </summary>
        public LunarDroneModes LunarDroneMode;

        #endregion

        #region Fields accesors

        /// <summary>
        /// Sets the current maximum number of the armor states
        /// </summary>
        /// <param name="value">The number of possible armor states</param>
        public void SetMaxArmorStates(byte value)
        {
            MaxArmorStates = value;
            // If we are in an illegal armor status, we reset it to 0
            if (ArmorState >= value)
                ArmorState = 0;
        }

        #endregion

        #region Effects reset

        public override void ResetEffects()
        {
			// The number of states this armor has
            MaxArmorStates = 0;
            // The type of the current armor
            PreviousArmorStateType = ArmorStateType;// The previous state, because UpdateInventory is called before the armor set update is called
            ArmorStateType = 0;
            // Cooldown stuff
            ArmorCooldownDuration = 0;
            ArmorCooldownDurationModifier = 1;
            IsArmorOnCooldown = false;
            // Warmup stuff
            ArmorWarmupDuration = 0;
            ArmorWarmupDurationModifier = 1;
            IsArmorOnWarmup = false;
            // Part of damage inflicted on mana instead of health
            MagicDamageAbsorptionAmount = 0;

            // Magical Life Steal
            MagicalLifeSteal = false;
            MagicalLifeStealAmount = 0.0f;

            // A modifier for the UseTime of magical weapons
            MagicUseTimeModifier = 1.0f;
            RangedUseTimeModifier = 1.0f;

            // If this player attract projectile
            ProjectileAttractor = false;
            // The range of the attractor
            ProjectileAttractorRange = 0.0f;

            //The amount of lunar drones this player should have
            LunarDroneCount = 0;
            LunarDroneMode = LunarDroneModes.Idle;
        }

		public override void UpdateDead()
		{
			MaxArmorStates = 0;

            ArmorStateType = 0;
            PreviousArmorStateType = 0;

            ArmorCooldownDuration = 0;
            ArmorCooldownDurationModifier = 1;
            IsArmorOnCooldown = false;

            ArmorWarmupDuration = 0;
            ArmorWarmupDurationModifier = 1;
            IsArmorOnWarmup = false;

            MagicDamageAbsorptionAmount = 0;
            MagicDamageAbsorption = false;

            MagicalLifeSteal = false;
            MagicalLifeStealAmount = 0.0f;

            MagicUseTimeModifier = 1.0f;
            RangedUseTimeModifier = 1.0f;

            // If this player attract projectile
            ProjectileAttractor = false;
            // The range of the attractor
            ProjectileAttractorRange = 0.0f;

            //The amount of lunar drones this player should have
            LunarDroneCount = 0;
            LunarDroneMode = LunarDroneModes.Idle;

        }

        #endregion

        #region Equipement effects

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            HandleMagicDamageAbsorption(ref modifiers);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            HandleMagicDamageAbsorption(ref modifiers);
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            HandleMagicDamageRetribution(hurtInfo);
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            HandleMagicDamageRetribution(hurtInfo);
        }

        private void HandleMagicDamageRetribution(Player.HurtInfo hurtInfo)
        {
            if (MagicDamageAbsorption)
            {
                // When hurt, we remove maa and charge the magic damage absorption
                int diff = hurtInfo.SourceDamage - hurtInfo.Damage;

                if (diff > 0)
                {
                    int manaRemoved = Math.Min(diff, Player.statMana);

                    if (manaRemoved > 0)
                    {
                        Player.statMana -= manaRemoved;
                        Player.manaRegenDelay = (int)Player.maxRegenDelay;

                        MagicDamageAbsorbed += diff;
                    }

                }
            }
        }

        // Damage Modification
        private void HandleMagicDamageAbsorption(ref Player.HurtModifiers modifiers)
        {

            if (MagicDamageAbsorption)
            {
                // First we compute the amount of damage avoided
                modifiers.FinalDamage *= MagicDamageAbsorptionAmount;

                /*
                // Doesn't work since the hit rework
                // We check if there is damage to remove
                if (removedDamage > 0)
                {
                    // We don't remove more damage than we have mana
                    int manaRemoved = Math.Min(removedDamage, Player.statMana);

                    // If we can remove damage
                    if (manaRemoved > 0)
                    {
                        // we remove it from the total
                        damage -= manaRemoved;
                        // Then we remove the mana
                        Player.statMana -= manaRemoved;
                        // And lastly we restart the mana regeneration delay
                        Player.manaRegenDelay = (int)Player.maxRegenDelay;

                        MagicDamageAbsorbed += damage;
                    }

                }TEST*/
            }
        }

        // Use Time modification
        public override float UseTimeMultiplier(Item item)
        {
            // If this is a magic weapon
            if (item.DamageType == DamageClass.Magic)
            {
                return MagicUseTimeModifier;
            }
            if (item.DamageType == DamageClass.Ranged)
            {
                return RangedUseTimeModifier;
            }
            
            return 1.0f;
        }

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Lunar drone multishoot
            // XXX: doesn't works on some weapons, because the hook is not called for those
            // Will strangely works better on mooded weapons
            if (item.DamageType == DamageClass.Ranged && LunarDroneMode == LunarDroneModes.Multishot)
            {
                MechArmorServerConfig conf = ModContent.GetInstance<MechArmorServerConfig>();
                // We need to find our lunar drones
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];

                    // When we find one of our drone
                    if(proj.owner == Player.whoAmI && proj.type == ModContent.ProjectileType<LunarArmorDrone>())
                    {
                        // we shoot a new projectile
                        int newProj = Projectile.NewProjectile(source, proj.Center, velocity, type, (int)(damage * (conf.BoostLunarMultishot ? 0.75f : 0.15f)), knockback, Player.whoAmI);
                        Main.projectile[newProj].netUpdate = true;

                    }
                }
            }

            return true;
        }


        // Lunar Drone Projectile Creation & Destruction, as required
        public override void PostUpdateEquips()
        {
            int existingDrones = Player.ownedProjectileCounts[ModContent.ProjectileType<LunarArmorDrone>()];
            // Do we have enough drones created ?
            if (existingDrones < LunarDroneCount)
            {
                //Nope, do we also need to add the buff ?
                if(existingDrones == 0)
                {
                    //Yes we do
                    Player.AddBuff(ModContent.BuffType<BuffLunarArmorDrone>(), 18000, true);
                }

                // We create a new drone
                LunarArmorDrone proj = (LunarArmorDrone)Projectile.NewProjectileDirect(Player.GetSource_Misc("SetBonus_LunarDroneSet"), Player.position, new Vector2(0, 0), ModContent.ProjectileType<LunarArmorDrone>(), 0, 0f, Player.whoAmI).ModProjectile;
                // We set its index
                proj.LunarDroneIndex = (sbyte)existingDrones;
            }
            
        }


        public override void PostUpdate()
        {
            // NOTE : for now, we don't need to unify the loops into one
            if(ProjectileAttractor)
            {
                foreach(Projectile proj in Main.projectile)
                {
                    if (proj.hostile && ModContent.GetInstance<MechArmorServerConfig>().CanAffectProjectile(proj))
                    {
                        // Are we are in range ?
                        Vector2 projToPlayer = Player.position - proj.position;
                        if (ProjectileAttractorRange * ProjectileAttractorRange > projToPlayer.LengthSquared())
                        {
                            //Yes, attract the projectile
                            //But first, do we need to kill it ?
                            if(256 > projToPlayer.LengthSquared())
                            {
                                //1 tile = 16 pixel
                                //16² = 256
                                proj.Kill();
                            }
                            else
                            {
                                //float speed = 100f / projToPlayer.LengthSquared();
                                //projToPlayer.Normalize();
                                projToPlayer.Normalize();
                                proj.velocity = projToPlayer * proj.velocity.Length();
                            }

                        }
                    }
                }
            }

            // If we have a Lunar projectile shield
            if(LunarDroneMode == LunarDroneModes.ProjectileShield)
            {
                // We check for hostile projectile in range
                foreach(Projectile proj in Main.projectile)
                {
                    if (proj.hostile && ModContent.GetInstance<MechArmorServerConfig>().CanAffectProjectile(proj))
                    {

                        if(Vector2.DistanceSquared(proj.Center, Player.Center) < LunarArmorDrone.DroneDistance * LunarArmorDrone.DroneDistance)
                        {
                            // If we have one, we teleport it to the player
                            proj.Center = Player.Center;
                            // We force damage calcultation
                            proj.Damage();
                            // And we kill it to clear it
                            proj.Kill();

                            proj.netUpdate = true;
                        }
                    }
                }
            }

            // If we need to charge a drone
            if(LunarDroneMode == LunarDroneModes.ManaShield)
            {
                if(MagicDamageAbsorbed > 100)
                {
                    // We remove some damage stored
                    MagicDamageAbsorbed -= 100;
                    int foundDrone = 0;
                    // And we sarch the player's drones
                    foreach(Projectile proj in Main.projectile)
                    {
                        if(proj.owner == Player.whoAmI && proj.type == ModContent.ProjectileType<LunarArmorDrone>())
                        {
                            // We found one
                            LunarArmorDrone drone = (LunarArmorDrone)proj.ModProjectile;
                            foundDrone++;

                            // If this the 1st found, we have a 1/n-drone chance to choose this one
                            // The second will have a 2/n-drone chance to be choosen
                            // until the n-th that will be garanteed to be choosed
                            // it's not equal, but it works
                            if(foundDrone > Main.rand.Next(LunarDroneCount) && !drone.ManaCharged)
                            {
                                drone.ManaCharged = true;
                                break;
                            }

                        }
                    }

                    // If no available drones was found, the chargeis simply lost
                }
            }
        }

        #endregion

        #region Attack effects



        #endregion

        #region Visual Effects

        private static byte ProjectileAttractorDrawingRotationOffset = 0;
        // (More or less) Fancy particles effects
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (ModContent.GetInstance<MechArmorDisplayConfig>().BulletAttractorGenerateDust)
            {
                // If we have a projectile attractor and at the first shadow pass (I think, I'll have to check).
                if (ProjectileAttractor)
                {
                    // We make a full circle of dust arround the player
                    for (int i = 0; i < 360; i += 8)
                    {
                        float dX = (float)Math.Cos(Math.PI / 180f * (i + ProjectileAttractorDrawingRotationOffset)) * ProjectileAttractorRange;
                        float dY = (float)Math.Sin(Math.PI / 180f * (i + ProjectileAttractorDrawingRotationOffset)) * ProjectileAttractorRange;
                        Vector2 toPlayer = Player.position - new Vector2(dX, dY);
                        toPlayer.Normalize();
                        Dust.NewDustPerfect(this.Player.position + new Vector2(dX, dY), 55, toPlayer, 1, default, 0.5f);

                    }
                }
                ProjectileAttractorDrawingRotationOffset++;// As we are using a byte, we'll wrap around to 0 when reaching 255
            }
            
        }

        #endregion

        #region Input processing

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            // When pressing the key, change the state of the armor
            sbyte stateChange = 0;


            if(MechArmor.MechArmorStateChangeKey.JustPressed)
            {
                stateChange = 1;//Forward
            }

            if(MechArmor.MechArmorStateChangeReverseKey.JustPressed)
            {
                stateChange = -1;//Backward
            }

            if (stateChange != 0 && !IsArmorOnCooldown)
			{
				//First, we need to check if we have an illegal armor state
				if(ArmorState >= MaxArmorStates && MaxArmorStates != 0)
                {
					//We are, so we reset the armor state
					ArmorState = 0;
                }
				else
                {
                    //Otherwise we increase or decrease the state by one
                    ArmorState = (byte)(((short)ArmorState) + stateChange);
                    // If we end up on 255, it means we need the last position
                    if (ArmorState == 255)
                        ArmorState = (byte)(MaxArmorStates - 1);
                    else
                        ArmorState %= MaxArmorStates;

                    // We also add the buffs, as required
                    if(ArmorCooldownDuration > 0)
                        Player.AddBuff(ModContent.BuffType<BuffStateCooldown>(), (int)(ArmorCooldownDuration * ArmorCooldownDurationModifier * 60));
                    if (ArmorWarmupDuration > 0)
                        Player.AddBuff(ModContent.BuffType<BuffStateWarmup>(), (int)(ArmorWarmupDuration * ArmorWarmupDurationModifier * 60));
                }
			}
        }

        #endregion

        #region Saving and loading

        public override void LoadData(TagCompound tag)
        {
            //We load the armor state
			//The MaxArmorState is useless because it is set from the armor itself
            if(tag.ContainsKey("ArmorState"))
            {
                ArmorState = tag.GetByte("ArmorState");
            } 
            else
            {
                ArmorState = 0;
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("ArmorState", ArmorState);
        }

        #endregion

        #region Multiplayer synchronisation

        /*
        // In MP, other clients need accurate information about your player or else bugs happen.
        // clientClone, SyncPlayer, and SendClientChanges, ensure that information is correct.
        // We only need to do this for data that is changed by code not executed by all clients, 
        // or data that needs to be shared while joining a world.
        // For example, examplePet doesn't need to be synced because all clients know that the player is wearing the ExamplePet item in an equipment slot. 
        // The examplePet bool is set for that player on every clients computer independently (via the Buff.Update), keeping that data in sync.
        // ExampleLifeFruits, however might be out of sync. For example, when joining a server, we need to share the exampleLifeFruits variable with all other clients.
        // In addition, in ExampleUI we have a button that toggles "Non-Stop Party". We need to sync this whenever it changes.
        public override void clientClone(ModPlayer clientClone)
		{
			MechArmorPlayer clone = clientClone as MechArmorPlayer;
			// Here we would make a backup clone of values that are only correct on the local players Player instance.
			// Some examples would be RPG stats from a GUI, Hotkey states, and Extra Item Slots
			clone.ArmorState = ArmorState;
            clone.ArmorStateType = ArmorStateType;
            clone.PreviousArmorStateType = PreviousArmorStateType;
		}
        TEST*/

        public override void CopyClientState(ModPlayer targetCopy)
        {
            MechArmorPlayer clone = targetCopy as MechArmorPlayer;
			clone.ArmorState = ArmorState;
            clone.ArmorStateType = ArmorStateType;
            clone.PreviousArmorStateType = PreviousArmorStateType;
        }

        // I think this is used to sync players when a player first come into the world
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			//Create a packet
			ModPacket packet = Mod.GetPacket();
			//Packet type
			packet.Write((byte)MechArmorMessageType.MechArmorPlayerSync);
			
			// 0-th field : whose player is updated
			packet.Write((byte)Player.whoAmI);
			
			// MechArmorPlayer fields updated
			packet.Write(ArmorState);
			//INSERT OTHER FIELDS HERE
            packet.Write(MagicDamageAbsorbed);
			
			// And finally we send the packet
			packet.Send(toWho, fromWho);
		}

        // Send any information that need syncing but was not already synced by Terraria (like buffs and items)
		public override void SendClientChanges(ModPlayer clientPlayer)
		{
			// Here we would sync something like an RPG stat whenever the player changes it.
			MechArmorPlayer clone = clientPlayer as MechArmorPlayer;
			if (clone.ArmorState != ArmorState)
			{
				// Send a Mod Packet with the changes.
				ModPacket packet = Mod.GetPacket();
				packet.Write((byte)MechArmorMessageType.MechArmorPlayerArmorStateChanged);
				packet.Write((byte)Player.whoAmI);
				packet.Write(ArmorState);
				packet.Send();
			}
            if(clone.MagicDamageAbsorbed != MagicDamageAbsorbed) {
                ModPacket packet = Mod.GetPacket();
				packet.Write((byte)MechArmorMessageType.MechArmorPlayerMagicAbsorptionChanged);
                packet.Write((byte)Player.whoAmI);
                packet.Write(MagicDamageAbsorbed);
                packet.Send();
            }
		}

        #endregion
    }
}
