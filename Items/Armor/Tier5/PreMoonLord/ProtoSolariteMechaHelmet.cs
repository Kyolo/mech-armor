using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier5.PreMoonLord
{
    [AutoloadEquip(EquipType.Head)]
    class ProtoSolariteMechaHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased Life Regen\n20% Increased Melee Critical Chance");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 90;
            item.rare = ItemRarityID.Orange;
            item.defense = 17;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 20;
            player.lifeRegen += 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<ProtoSolariteMechaBreastplate>() && legs.type == ItemType<ProtoSolariteMechaLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Lunar drones assists you";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 3;

            armorPlayer.LunarDroneCount = 4;

            // And then we allow the use of heavy guns


            switch (armorPlayer.ArmorState)
            {
                case 0:// Setup mode : allow movement to prepare for aiming
                    {
                        player.setBonus += "\nMelee Extender :\nIncreased Melee Range\n50% Melee Damage";
                        player.meleeDamage += 0.50f;
                        armorPlayer.LunarDroneMode = LunarDroneModes.FollowSwing;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nJetpack :\n50% Flight Time Increase\n20% Move Speed";
                        player.moveSpeed += 0.20f;
                        player.wingTimeMax = (int) (player.wingTimeMax * 1.5f);
                        armorPlayer.LunarDroneMode = LunarDroneModes.JetpackWings;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Movement;
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
            regularRecipe.AddIngredient(ItemID.FragmentSolar, 10);
            regularRecipe.AddIngredient(ItemID.Wire, 10);
            regularRecipe.AddIngredient(ItemID.Cog, 10);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
