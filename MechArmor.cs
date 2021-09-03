using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace MechArmor
{
    public class MechArmor : Mod
    {

        // Key bindings
        public static ModHotKey MechArmorStateChangeKey;
        public static ModHotKey MechArmorStateChangeReverseKey;

        // UI Elements
        private UserInterface MechArmorUI;
        public UI.ArmorStateIndicator ArmorStateIndicatorState;

        public override void Load()
        {
            // Creating the hotkey for the pst-hm armor state change
            MechArmorStateChangeKey = RegisterHotKey("MechArmorStateChangeKey", "V");
            MechArmorStateChangeReverseKey = RegisterHotKey("MechArmorStateChangeReverseKey", "C");
            // Keeping a static instance of the mod in the packet handler
            MechArmorPacketHandler.mod = this;


            // Code only started on client
            if (!Main.dedServ)
            {
                ArmorStateIndicatorState = new UI.ArmorStateIndicator();
                ArmorStateIndicatorState.Activate();

                MechArmorUI = new UserInterface();
                MechArmorUI.SetState(ArmorStateIndicatorState);
            }
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

            //Adding recipe group for early HM Bars
            RecipeGroup groupHMT1Bars = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Cobalt Bar", new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            });
            RecipeGroup groupHMT2Bars = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Mythril Bar", new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar
            });
            RecipeGroup groupHMT3Bars = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Adamantite Bar", new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
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

            RecipeGroup.RegisterGroup("MechArmor:Bars:HMT1", groupHMT1Bars);
            RecipeGroup.RegisterGroup("MechArmor:Bars:HMT2", groupHMT2Bars);
            RecipeGroup.RegisterGroup("MechArmor:Bars:HMT3", groupHMT3Bars);

            //// Armors
            // Silver/Tungsten
            RecipeGroup.RegisterGroup("MechArmor:Armor:Head:T2", groupHelmet);
            RecipeGroup.RegisterGroup("MechArmor:Armor:Chest:T2", groupChestplate);
            RecipeGroup.RegisterGroup("MechArmor:Armor:Pants:T2", groupLegging);
        }


        // UI Update stuff
        public override void UpdateUI(GameTime gameTime)
        {
            MechArmorUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MechArmor: Multi-State Armor state display",
                    delegate
                    {
                        MechArmorUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
