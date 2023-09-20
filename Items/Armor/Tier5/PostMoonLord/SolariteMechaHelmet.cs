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
/* 
Removed because of localization update
            Tooltip.SetDefault("Increased life regeneration\n20% increased melee critical chance");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 90;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 22;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Melee) += 20;
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



            switch (armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "\nMelee Extender :\nIncreased melee range\n50% increased melee damage";
                        player.GetDamage(DamageClass.Melee) += 0.50f;
                        armorPlayer.LunarDroneMode = LunarDroneModes.FollowSwing;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nJetpack :\n50% increased flight yime increase\n20% increased move speed";
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
            CreateRecipe()
            .AddTile(TileID.LunarCraftingStation)
            .AddIngredient(ModContent.ItemType<ProtoSolariteMechaHelmet>())
            .AddIngredient(ItemID.LunarBar, 12)
            .Register();
        }
    }
}
