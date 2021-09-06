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
            DisplayName.SetDefault("Shifting Spectral Hood");
            Tooltip.SetDefault("Increase maximum mana by 100\n10% increased magical damage\n10% increased magical critical chance");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 8, 50, 0);
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
            player.setBonus = "";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 10;


            switch(armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "Defense Mode :\n 15% Magic Damage Absorption\n 10% reduced magic UseTime";
                        armorPlayer.MagicDamageAbsorption = true;
                        armorPlayer.MagicDamageAbsorptionAmount += 0.15f;
                        armorPlayer.MagicUseTimeModifier -= 0.10f;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "Offense Mode :\n Killing an enemy release a Healing orb (WIP)\n 50% increased mana cost\n 10% reduced movement speed";
                        player.ghostHeal = true;
                        player.manaCost += 0.50f;
                        player.moveSpeed -= 0.10f;
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

            ModRecipe regularRecipe = new ModRecipe(mod);
            regularRecipe.AddTile(TileID.MythrilAnvil);
            regularRecipe.AddIngredient(ItemID.SpectreBar, 10);
            regularRecipe.AddIngredient(ItemID.Diamond, 2);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
