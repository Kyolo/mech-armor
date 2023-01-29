using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MechArmor
{
    public class MechArmor : Mod
    {

        // Key bindings
        public static ModKeybind MechArmorStateChangeKey;
        public static ModKeybind MechArmorStateChangeReverseKey;

        
        public override void Load()
        {
            // Creating the hotkey for the pst-hm armor state change
            MechArmorStateChangeKey = KeybindLoader.RegisterKeybind(this, "MechArmorStateChangeKey", "V");
            MechArmorStateChangeReverseKey = KeybindLoader.RegisterKeybind(this, "MechArmorStateChangeReverseKey", "C");
            // Keeping a static instance of the mod in the packet handler
            MechArmorPacketHandler.mod = this;

        }

        public override void Unload()
        {
            // We need to unload all static instance of our stuff
            MechArmorPacketHandler.mod = null;
        }

        public override void HandlePacket(BinaryReader reader, int sender)
        {
            //Called when this mod receive a packet
            //For the sake of a small main file, it is handled in another file
            MechArmorPacketHandler.HandlePacket(reader, sender);
        }

        
    }
}
