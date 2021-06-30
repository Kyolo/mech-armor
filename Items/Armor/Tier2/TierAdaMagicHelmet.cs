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
            Tooltip.SetDefault("+100 maximum mana\nA fragile helmet that help channeling mana");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 90;
            item.rare = ItemRarityID.Orange;
            item.defense = 4;
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
            player.setBonus = "This mana conductive armor can help you channel magic in different ways";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 5;

            switch (armorPlayer.ArmorState)
            {
                case 0:// Efficient mode : a bit less damage, better efficiency
                    {
                        player.setBonus += "\nUtility Mode :\n -20% Mana Cost\n Increased mana regeneration";
                        player.manaCost -= 0.20f;
                        player.manaRegenBonus += 50;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Utility;
                    }
                    break;
                case 1:// Damage mode : more damage, less efficiency
                    {
                        player.setBonus += "\nOffense Mode :\n +30% Magic Damage\n +10% Mana Cost";
                        player.magicDamage += 0.30f;
                        player.manaCost += 0.10f;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
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

            // Corrupted world recipe
            ModRecipe regularRecipe = new ModRecipe(mod);
            regularRecipe.AddTile(TileID.MythrilAnvil);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT1", 10);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT2", 10);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT3", 10);
            regularRecipe.AddIngredient(ItemID.CursedFlame, 20);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();

            // Crimson world recipe
            ModRecipe otherRecipe = new ModRecipe(mod);
            otherRecipe.AddTile(TileID.MythrilAnvil);
            otherRecipe.AddRecipeGroup("MechArmor:Bars:HMT1", 10);
            otherRecipe.AddRecipeGroup("MechArmor:Bars:HMT2", 10);
            otherRecipe.AddRecipeGroup("MechArmor:Bars:HMT3", 10);
            otherRecipe.AddIngredient(ItemID.Ichor, 20);
            otherRecipe.SetResult(this);
            otherRecipe.AddRecipe();
        }
    }
}
