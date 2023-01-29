
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace MechArmor {
    public class MechArmorRecipeSystem : ModSystem
    {
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
           
        
    }
}