using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MechArmor
{
	public class MechArmor : Mod
	{

        public static ModHotKey MechArmorStateChangeKey;
       

        public override void Load()
        {
            // Creating the hotkey for the pst-hm armor state change
            MechArmorStateChangeKey = RegisterHotKey("MechArmorStateChangeKey", "V");
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

        public override void AddRecipeGroups()
        {
            //Adding recipe group for Silver/Tungsten Bars
            RecipeGroup groupT2Bars = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silver Bar", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup groupEvilBars = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Bar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });

            //Groups for Silver/Tungsten Armors
            RecipeGroup groupHelmet = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silver Helmet", new int[]
            {
                ItemID.SilverHelmet,
                ItemID.TungstenHelmet
            });
            RecipeGroup groupChestplate = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silver Chainmail", new int[]
            {
                ItemID.SilverChainmail,
                ItemID.TungstenChainmail
            });
            RecipeGroup groupLegging = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silver Greaves", new int[]
            {
                ItemID.SilverGreaves,
                ItemID.TungstenGreaves
            });

            //// Items
            // Bars
            RecipeGroup.RegisterGroup("MechArmor:Bars:T2", groupT2Bars);
            RecipeGroup.RegisterGroup("MechArmor:Bars:Evil", groupEvilBars);

            //// Armors
            // Silver/Tungsten
            RecipeGroup.RegisterGroup("MechArmor:Armor:Head:T2", groupHelmet);
            RecipeGroup.RegisterGroup("MechArmor:Armor:Chest:T2", groupChestplate);
            RecipeGroup.RegisterGroup("MechArmor:Armor:Pants:T2", groupLegging);
        }
    }
}