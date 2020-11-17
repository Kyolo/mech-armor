using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    class PowerAssistedArmorHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A helmet with alloyed hellstone and evilmetal armor plates");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            //TODO: find best values
            item.value = 90;
            item.rare = ItemRarityID.Orange;
            item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<PowerAssistedArmorBreastplate>() && legs.type == ItemType<PowerAssistedArmorLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            MechArmorPlayer mAP = player.GetModPlayer<MechArmorPlayer>();
            mAP.SetMaxArmorStates(2);

            //TODO: find correct value
            switch(mAP.ArmorState)
            {
                case 0:
                    player.moveSpeed += 1.0f;
                    break;
                case 1:
                    player.statDefense += 20;
                    break;
            }
        }
        public override void AddRecipes()
        {
            //TODO: find cost
        }
    }
}
