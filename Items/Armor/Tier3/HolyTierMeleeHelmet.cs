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
            Tooltip.SetDefault("+5% Melee Critical Strike Chance\n+10% Damage\nA helmet with basic control systems");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            //TODO: find best values
            item.value = 90;
            item.rare = ItemRarityID.Orange;
            item.defense = 24;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<HolyTierBreastplate>() && legs.type == ItemType<HolyTierLeggings>();
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.5f;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = "This armor can either make you faster or protect you.";

            MechArmorPlayer mAP = player.GetModPlayer<MechArmorPlayer>();
            mAP.SetMaxArmorStates(3);
            mAP.ArmorCooldownDuration = 10;


            switch(mAP.ArmorState)
            {
                case 0:
                    player.setBonus += "\nOffensive Movement Mode :\n +20% Movement Speed\n +30% Melee Swing Speed";
                    player.moveSpeed += 0.20f;
                    player.meleeSpeed += 0.30f;
                    mAP.ArmorStateType = (byte)EnumArmorStateType.Offense | (byte)EnumArmorStateType.Movement;
                    break;
                case 1:
                    player.setBonus += "\nDefense Mode :\n +50% Armor\n +10% Damage Reduction\n -20% Movement Speed";
                    player.statDefense += 25; // (24 + 15 +11) * 0.5 = 24
                    player.moveSpeed -= 0.20f;
                    player.endurance += 0.10f;
                    mAP.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    break;
                case 2:
                    player.setBonus += "\nMovement Mode :\n +20% Movement Speed\n +25% Wing Time";
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
