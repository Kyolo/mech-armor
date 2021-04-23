using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    class ManaChannelArmorHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A fragile helmet with advanced targeting systems");
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
            return body.type == ItemType<ManaChannelArmorBreastplate>() && legs.type == ItemType<ManaChannelArmorLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "This mana conductive armor can help you channel magic in different ways";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 5;

            switch(armorPlayer.ArmorState)
            {
                case 0:// Efficient mode : a bit less damage, better efficiency
                    {
                        player.setBonus += "\nMana Mode : Reduced mana cost and increased mana regeneration";
                        player.manaCost -= 0.20f;
                        player.manaRegenBonus += 50;
                    }
                    break;
                case 1:// Damage mode : more damage, less efficiency
                    {
                        player.setBonus += "\nPower Mode : Increased damage and mana cost";
                        player.magicDamage += 0.30f;
                        player.manaCost += 0.10f;
                    }
                    break;
            }


        }

        public override void AddRecipes()
        {
            // TODO: add recipes
            // 
            ModRecipe r = new ModRecipe(mod);
            r.AddTile(TileID.WorkBenches);
            r.AddRecipeGroup("Wood");
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
