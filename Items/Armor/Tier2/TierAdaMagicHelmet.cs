using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier2
{
    [AutoloadEquip(EquipType.Head)]
    class TierAdaMagicHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            DisplayName.SetDefault("Hard Metal Hood");
*/
/* 
Removed because of localization update
            Tooltip.SetDefault("+100 maximum mana");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 3, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 100;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<TierAdaBreastplate>() && legs.type == ItemType<TierAdaLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 5;

            switch (armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "\nUtility Mode :\n 20% Reduced Mana Cost\n Increased Mana Regeneration";
                        player.manaCost -= 0.20f;
                        player.manaRegenBonus += 50;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Utility;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nOffense Mode :\n 30% Increased Magic Damage\n 60% Increased Mana Cost";
                        player.GetDamage(DamageClass.Magic) += 0.30f;
                        player.manaCost += 0.60f;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
            }


        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddRecipeGroup("MechArmor:Bars:HMT1", 10)
            .AddRecipeGroup("MechArmor:Bars:HMT2", 10)
            .AddRecipeGroup("MechArmor:Bars:HMT3", 10)
            .AddIngredient(ItemID.CursedFlame, 20)
            .Register();

            // Crimson world recipe
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddRecipeGroup("MechArmor:Bars:HMT1", 10)
            .AddRecipeGroup("MechArmor:Bars:HMT2", 10)
            .AddRecipeGroup("MechArmor:Bars:HMT3", 10)
            .AddIngredient(ItemID.Ichor, 20)
            .Register();
        }
    }
}
