using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier5.PreMoonLord
{
    [AutoloadEquip(EquipType.Head)]
    class ProtoStardustedMechaHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("20% increased minion damage\nIncrease minion slot by 1");
*/
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 7, 75, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.20f;
            player.maxMinions += 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<ProtoStardustedMechaBreastplate>() && legs.type == ItemType<ProtoStardustedMechaLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Lunar Drones assists you";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 10;

            armorPlayer.LunarDroneCount = 4;


            switch (armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "\nSummon Empowering :\n"+armorPlayer.LunarDroneCount+" summons are empowered";
                        armorPlayer.LunarDroneMode = LunarDroneModes.SummonBoost;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nNano Shield :\n15% increased damage reduction\nNearby enemies are periodicaly shot by nano bullets";
                        player.endurance += 0.15f;
                        armorPlayer.LunarDroneMode = LunarDroneModes.Confusion;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Utility;
                    }
                    break;
            }


        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.LunarCraftingStation)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ItemID.Wire, 10)
            .AddIngredient(ItemID.Cog, 10)
            .Register();
        }
    }
}
