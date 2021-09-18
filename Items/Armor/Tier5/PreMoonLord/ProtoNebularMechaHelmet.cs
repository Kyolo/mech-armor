using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier5.PreMoonLord
{
    [AutoloadEquip(EquipType.Head)]
    class ProtoNebularMechaHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increase maximum mana by 200\n10% reduced mana cost\n5% increased magical critical chance");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 5, 50, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 11;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 200;
            player.manaCost -= 0.10f;
            player.magicCrit += 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<ProtoNebularMechaBreastplate>() && legs.type == ItemType<ProtoNebularMechaLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Lunar Drones assists you";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 2;

            // We add the lunar drones for the player
            armorPlayer.LunarDroneCount = 4;


            switch (armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "\nMana Amplifier :\n50% increased magical damage\n100% increased mana cost";
                        player.magicDamage += 0.5f;
                        player.manaCost += 1.0f;
                        // Because we use lunar drones, we need to tell them how to behave
                        armorPlayer.LunarDroneMode = LunarDroneModes.ManaAmplifier;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\n10% Lifesteal with magical weapon\n50% decreased magical damage";
                        armorPlayer.MagicalLifeSteal = true;
                        armorPlayer.MagicalLifeStealAmount = 0.10f;
                        //TODO: healing pulse on drinking mana potion
                        armorPlayer.LunarDroneMode = LunarDroneModes.ManaLifeSteal;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense;
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
            regularRecipe.AddTile(TileID.LunarCraftingStation);
            regularRecipe.AddIngredient(ItemID.FragmentNebula, 10);
            regularRecipe.AddIngredient(ItemID.Cog, 10);
            regularRecipe.AddIngredient(ItemID.Wire, 10);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
