using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    class PowerAssistedArmorBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A breastplate with alloyed hellstone and evil metal armor plates");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            //TODO: find best values
            item.value = 70;
            item.rare = ItemRarityID.Orange;
            item.defense = 7;
        }

        // Set bonus in helmet(s)

        public override void AddRecipes()
        {
            // TODO : find recipe
        }
    }
}
