using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier2
{
    [AutoloadEquip(EquipType.Body)]
    class TierAdaBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 2, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 5;
        }

        // Set bonus in helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddRecipeGroup("MechArmor:Bars:HMT1", 20)
            .AddRecipeGroup("MechArmor:Bars:HMT2", 20)
            .AddRecipeGroup("MechArmor:Bars:HMT3", 20)
            .Register();
        }
    }
}
