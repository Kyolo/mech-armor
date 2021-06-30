using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier3
{
    [AutoloadEquip(EquipType.Head)]
    class HolyTierMagicHelmet : ModItem
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
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 100;
            player.magicDamage += 0.10f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<HolyTierBreastplate>() && legs.type == ItemType<HolyTierLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "WIP";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(3);
            armorPlayer.ArmorCooldownDuration = 5;

            switch (armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "\nOffense Mode :\n +20% Magic Damage\n -10% Mana Cost";
                        player.manaCost -= 0.20f;
                        player.manaRegenBonus += 50;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    player.setBonus += "\nDefense Mode :\n +20% Armor\n +10% Magical Damage Absorption";
                    player.statDefense += 6; // (5 + 15 +11) * 0.2 = 6
                    armorPlayer.MagicDamageAbsorption = true;
                    armorPlayer.MagicDamageAbsorptionAmount = 0.10f;
                    armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    
                    break;
                case 2:
                    player.setBonus += "\nMovement Mode :\n +20% Movement Speed\n +25% Wing Time";
                    player.moveSpeed += 0.20f;
                    player.wingTimeMax = (int)((float)player.wingTimeMax * 1.25f);
                    armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Movement;
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
            regularRecipe.AddIngredient(ItemID.HallowedBar, 10);
            regularRecipe.AddIngredient(ItemID.SoulofSight, 5);
            regularRecipe.AddIngredient(ItemID.PixieDust, 20);
            regularRecipe.AddIngredient(ItemID.LightShard, 2);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();

        }
    }
}
