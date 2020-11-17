using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

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
        

        public override void ResetEffects()
        {
			// The number of states this armor has
            MaxArmorStates = 0;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
			// When pressing the key, change the state of the armor
			// Could be changed for a number
			if (MechArmor.MechArmorStateChangeKey.JustPressed)
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
                }
				//TODO: check how to do a player sync
			}
        }

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

        public override void UpdateDead()
        {
			MaxArmorStates = 0;
        }
    }
}
