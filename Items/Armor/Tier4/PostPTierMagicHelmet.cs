using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Head)]
    class PostPTierMagicHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Mana +100\n+10% Magic Damage\n+10% Magical Critical Strike Chance");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 90;
            item.rare = ItemRarityID.Orange;
            item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 100;
            player.magicDamage += 0.10f;
            player.magicCrit += 10;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<PostPTierMagicBreastplate>() && legs.type == ItemType<PostPTierMagicLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "WIP";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 10;

            // And then we allow the use of heavy guns


            switch(armorPlayer.ArmorState)
            {
                case 0:// Setup mode : allow movement to prepare for aiming
                    {
                        player.setBonus += "\nDefense Mode :\n +15% Magic Damage Absorption\n -10% Magic UseTime";
                        armorPlayer.MagicDamageAbsorption = true;
                        armorPlayer.MagicDamageAbsorptionAmount += 0.15f;
                        armorPlayer.MagicUseTimeModifier -= 0.10f;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nOffense Mode :\n Killing an enemy release a Healing orb (WIP)\n +50% Mana Cost\n -10% Movement Speed";
                        player.ghostHeal = true;
                        player.manaCost += 0.50f;
                        player.moveSpeed -= 0.10f;
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

            ModRecipe regularRecipe = new ModRecipe(mod);
            regularRecipe.AddTile(TileID.MythrilAnvil);
            regularRecipe.AddIngredient(ItemID.SpectreBar, 10);
            regularRecipe.AddIngredient(ItemID.Diamond, 2);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
