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
            Tooltip.SetDefault("20% increased minion damage\nIncrease minion slots by 1");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice();
            item.rare = ItemRarityID.Orange;
            item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.20f;
            player.maxMinions += 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<StardustedMechaBreastplate>() && legs.type == ItemType<StardustedMechaLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";

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
            regularRecipe.AddIngredient(ModContent.ItemType<ProtoStardustedMechaHelmet>());
            regularRecipe.AddIngredient(ItemID.LunarBar, 12);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
