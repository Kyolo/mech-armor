using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

using MechArmor.Buffs;
using Terraria.DataStructures;

namespace MechArmor
{
    class MechArmorPlayer : ModPlayer
    {
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
        /// If the player can use the heaviest guns of the mod.
        /// Only possible with mecha and heavy armors
        /// </summary>
        public bool CanUseHeavyGuns {
			get { return ArmorHeavyGun; }
		}

		public bool ArmorHeavyGun;

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


        // Effects use


        public override void ResetEffects()
        {
			// The number of states this armor has
            MaxArmorStates = 0;
            // The type of the current armor
            ArmorStateType = 0;
            // Cooldown stuff
            ArmorCooldownDuration = 0;
            ArmorCooldownDurationModifier = 1;
            IsArmorOnCooldown = false;
            // Warmup stuff
            ArmorWarmupDuration = 0;
            ArmorWarmupDurationModifier = 1;
            IsArmorOnWarmup = false;
            //If armor can yield heavy weapons
            ArmorHeavyGun = false;
            // Part of damage inflicted on mana instead of health
            MagicDamageAbsorptionAmount = 0;
            // A modifier for the UseTime of magical weapons
            MagicUseTimeModifier = 1.0f;
            RangedUseTimeModifier = 1.0f;

            // If this player attract projectile
            ProjectileAttractor = false;
            // The range of the attractor
            ProjectileAttractorRange = 0.0f;

        }

		public override void UpdateDead()
		{
			MaxArmorStates = 0;

            ArmorStateType = 0;

            ArmorCooldownDuration = 0;
            ArmorCooldownDurationModifier = 1;
            IsArmorOnCooldown = false;

            ArmorWarmupDuration = 0;
            ArmorWarmupDurationModifier = 1;
            IsArmorOnWarmup = false;

            ArmorHeavyGun = false;

            MagicDamageAbsorptionAmount = 0;
            MagicDamageAbsorption = false;

            MagicUseTimeModifier = 1.0f;
            RangedUseTimeModifier = 1.0f;

            // If this player attract projectile
            ProjectileAttractor = false;
            // The range of the attractor
            ProjectileAttractorRange = 0.0f;

        }

        // Damage Modification
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {

            if (MagicDamageAbsorption)
            {
                // First we compute the amount of damage avoided
                int removedDamage = (int)(damage * MagicDamageAbsorptionAmount);

                // We check if there is damage to remove
                if (removedDamage > 0)
                {
                    // We don't remove more damage than we have mana
                    int manaRemoved = Math.Min(removedDamage, player.statMana);

                    // If we can remove damage
                    if (manaRemoved > 0)
                    {
                        // we remove it from the total
                        damage -= manaRemoved;
                        // Then we remove the mana
                        player.statMana -= manaRemoved;
                        // And lastly we restart the mana regeneration delay
                        player.manaRegenDelay = (int)player.maxRegenDelay;
                    }

                }
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        // Use Time modification
        public override float UseTimeMultiplier(Item item)
        {
            // If this is a magic weapon
            if(item.magic)
            {
                return MagicUseTimeModifier;
            }
            if (item.ranged)
            {
                return RangedUseTimeModifier;
            }
            return 1.0f;
        }




        // Key trigger

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            // When pressing the key, change the state of the armor
            byte stateChange = 0;

            if(MechArmor.MechArmorStateChangeKey.JustPressed)
            {
                stateChange = 1;//Forward
            }

            if(MechArmor.MechArmorStateChangeReverseKey.JustPressed)
            {
                stateChange = 2;//Backward
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
					switch(stateChange)
                    {
                        case 1:
                            ArmorState++;
                            break;
                        case 2:
                            ArmorState--;
                            break;
                        default:
                            break;
                    }
                    // If we end up on 255, it means we need the last position
                    if (ArmorState == 255)
                        ArmorState = (byte)(MaxArmorStates - 1);
                    else
                        ArmorState %= MaxArmorStates;

                    if(ArmorCooldownDuration > 0)
                        player.AddBuff(ModContent.BuffType<BuffStateCooldown>(), (int)(ArmorCooldownDuration*ArmorCooldownDurationModifier* 60));
                    if (ArmorWarmupDuration > 0)
                        player.AddBuff(ModContent.BuffType<BuffStateWarmup>(), (int)(ArmorWarmupDuration * ArmorWarmupDurationModifier * 60));
                }
			}
        }

		// Save

        public override void Load(TagCompound tag)
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

        public override TagCompound Save()
        {
			TagCompound tag = new TagCompound
			{
				{ "ArmorState", ArmorState }
			};

			return tag;
        }

		// Sync

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
		}

        // I think this is used to sync players when a player first come into the world
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			//Create a packet
			ModPacket packet = mod.GetPacket();
			//Packet type
			packet.Write((byte)MechArmorMessageType.MechArmorPlayerSync);
			
			// 0-th field : whose player is updated
			packet.Write((byte)player.whoAmI);
			
			// MechArmorPlayer fields updated
			packet.Write(ArmorState);
			//INSERT OTHER FIELDS HERE
			
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
				ModPacket packet = mod.GetPacket();
				packet.Write((byte)MechArmorMessageType.MechArmorPlayerArmorStateChanged);
				packet.Write((byte)player.whoAmI);
				packet.Write(ArmorState);
				packet.Send();
			}
		}


    }
}
