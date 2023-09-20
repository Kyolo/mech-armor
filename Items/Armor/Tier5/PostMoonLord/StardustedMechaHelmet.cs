using MechArmor.Items.Armor.Tier5.PreMoonLord;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier5.PostMoonLord
{
    [AutoloadEquip(EquipType.Head)]
    class StardustedMechaHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            Tooltip.SetDefault("20% increased minion damage\nIncrease minion slots by 1");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice();
            Item.rare = ItemRarityID.Orange;
            Item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.20f;
            player.maxMinions += 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<StardustedMechaBreastplate>() && legs.type == ItemType<StardustedMechaLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Lunar drones assits you";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(3);
            armorPlayer.ArmorCooldownDuration = 10;

            armorPlayer.LunarDroneCount = 8;


            switch (armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "Summons Empowering :\n" + armorPlayer.LunarDroneCount + " summons are empowered";
                        armorPlayer.LunarDroneMode = LunarDroneModes.SummonBoost;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "Nano Shield :\n15% Damage Reduction\nNearby enemies are periodicaly shot by nano bullets";
                        player.endurance += 0.15f;
                        armorPlayer.LunarDroneMode = LunarDroneModes.Confusion;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Utility;
                    }
                    break;
                case 2:
                    {
                        player.setBonus += "Jetpack Platform :\n50% Increased flight time\n30% Increased movement speed";
                        player.moveSpeed += 0.30f;
                        player.wingTimeMax = (int) (player.wingTimeMax * 0.50f);
                        armorPlayer.LunarDroneMode = LunarDroneModes.JetpackPlatform;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Movement;
                    }
                    break;
            }


        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.LunarCraftingStation)
            .AddIngredient(ModContent.ItemType<ProtoStardustedMechaHelmet>())
            .AddIngredient(ItemID.LunarBar, 12)
            .Register();
        }
    }
}
