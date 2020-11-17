using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    class PowerAssistedArmorLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A leggings with alloyed hellstone and evilmetal armor plates");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            //TODO: find best values
            item.value = 70;
            item.rare = ItemRarityID.Orange;
            item.defense = 6;
            
        }

        // Set bonus in the helmet(s)

        public override void AddRecipes()
        {
            //TODO : Find recipe
        }
    }
}
