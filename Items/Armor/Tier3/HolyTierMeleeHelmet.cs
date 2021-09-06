using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier3
{
    [AutoloadEquip(EquipType.Head)]
    class HolyTierMeleeHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holy Fencer Helmet");
            Tooltip.SetDefault("5% increased melee damage");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 24;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<HolyTierBreastplate>() && legs.type == ItemType<HolyTierLeggings>();
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.05f;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = "";

            MechArmorPlayer mAP = player.GetModPlayer<MechArmorPlayer>();
            mAP.SetMaxArmorStates(3);
            mAP.ArmorCooldownDuration = 10;


            switch(mAP.ArmorState)
            {
                case 0:
                    player.setBonus += "Offensive Movement Mode :\n 20% Increased movement speed\n +30% Melee Swing Speed";
                    player.moveSpeed += 0.20f;
                    player.meleeSpeed += 0.30f;
                    mAP.ArmorStateType = (byte)EnumArmorStateType.Offense | (byte)EnumArmorStateType.Movement;
                    break;
                case 1:
                    player.setBonus += "Defense Mode :\n 50% additional armor\n 10% increased damage reduction\n 20% reduced movement speed";
                    player.statDefense += 25; // (24 + 15 +11) * 0.5 = 24
                    player.moveSpeed -= 0.20f;
                    player.endurance += 0.10f;
                    mAP.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    break;
                case 2:
                    player.setBonus += "Movement Mode :\n 20% increased movement speed\n 25% increased wing time";
                    player.moveSpeed += 0.20f;
                    player.wingTimeMax = (int)((float)player.wingTimeMax * 1.25f);
                    mAP.ArmorStateType = (byte)EnumArmorStateType.Movement;
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
            regularRecipe.AddIngredient(ItemID.HallowedBar, 10);
            regularRecipe.AddIngredient(ItemID.SoulofSight, 5);
            regularRecipe.AddIngredient(ItemID.UnicornHorn, 5);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
