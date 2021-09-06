using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier2
{
    [AutoloadEquip(EquipType.Head)]
    class TierAdaMeleeHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("5% Increased Melee Critical Chance\n10% Melee Damage");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 3, 50, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 22;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<TierAdaBreastplate>() && legs.type == ItemType<TierAdaLeggings>();
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 5;
            player.meleeDamage += 0.10f;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = "";

            MechArmorPlayer mAP = player.GetModPlayer<MechArmorPlayer>();
            mAP.SetMaxArmorStates(2);
            mAP.ArmorCooldownDuration = 10;


            switch(mAP.ArmorState)
            {
                case 0:
                    player.setBonus += "Offensive Movement Mode :\n 20% Increased Movement Speed\n 20% Increased Melee Swing Speed";
                    player.moveSpeed += 0.20f;
                    player.meleeSpeed += 0.20f;
                    mAP.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    break;
                case 1:
                    player.setBonus += "Defense Mode :\n 50% Increased Armor\n 10% Reduced Movement Speed";
                    player.statDefense += 24; // (22 + 15 +11) * 0.5 = 24
                    player.moveSpeed -= 0.10f;
                    mAP.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    break;
            }
        }
        public override void AddRecipes()
        {
            if (ModContent.GetInstance<MechArmorServerConfig>().UseTestingRecipes)
            {
                ModRecipe r = new ModRecipe(mod);
                r.AddTile(TileID.WorkBenches);
                r.AddRecipeGroup("Wood");
                r.SetResult(this);
                r.AddRecipe();
            }

            ModRecipe regularRecipe = new ModRecipe(mod);
            regularRecipe.AddTile(TileID.MythrilAnvil);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT1", 10);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT2", 10);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT3", 10);
            regularRecipe.AddIngredient(ItemID.CrystalShard, 40);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
