using MechArmor.Items.Armor.Tier5.PreMoonLord;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier5.PostMoonLord
{
    [AutoloadEquip(EquipType.Head)]
    class SolariteMechaHelmet : ModItem
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
            item.defense = 22;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 20;
            player.lifeRegen += 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<SolariteMechaBreastplate>() && legs.type == ItemType<SolariteMechaLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Lunar drones assists you";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(3);
            armorPlayer.ArmorCooldownDuration = 3;

            armorPlayer.LunarDroneCount = 8;

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
                case 2:
                    {
                        player.setBonus += "\nProjectile Shield :\nIntercept nearby projectile, taking damage but protecting your allies";
                        armorPlayer.LunarDroneMode = LunarDroneModes.ProjectileShield;
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
            regularRecipe.AddIngredient(ModContent.ItemType<ProtoSolariteMechaHelmet>());
            regularRecipe.AddIngredient(ItemID.LunarBar, 12);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
