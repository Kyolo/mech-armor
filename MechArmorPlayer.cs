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
        }

		public override void UpdateDead()
		{
			MaxArmorStates = 0;

            ArmorCooldownDuration = 0;
            ArmorCooldownDurationModifier = 1;
            IsArmorOnCooldown = false;

            ArmorWarmupDuration = 0;
            ArmorWarmupDurationModifier = 1;
            IsArmorOnWarmup = false;

            ArmorHeavyGun = false;
		}

		// Key trigger

		public override void ProcessTriggers(TriggersSet triggersSet)
        {
			// When pressing the key, change the state of the armor
			// Could be changed for a number
			if (MechArmor.MechArmorStateChangeKey.JustPressed && !IsArmorOnCooldown)
			{
				//First, we need to check if we have an illegal armor state
				if(ArmorState >= MaxArmorStates && MaxArmorStates != 0)
                {
					//We are, so we reset the armor state
					ArmorState = 0;
                }
				else
                {
					//Otherwise we increase the state by one
					ArmorState++;
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
