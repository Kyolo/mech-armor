using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Head)]
    class PostPTierSummonHelmet : ModItem
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
            item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
            player.minionDamage += 0.10f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<PostPTierSummonBreastplate>() && legs.type == ItemType<PostPTierSummonLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "WIP";


            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 10;

            // And then we allow the use of heavy guns


            switch(armorPlayer.ArmorState)
            {
                case 0:// Setup mode : allow movement to prepare for aiming
                    {
                        player.setBonus += "\nDefense Mode :\n +2 Armor per minion slot filed\n +5% Movement Speed per minion slot filed";
                        player.statDefense += 2 * player.numMinions;
                        player.moveSpeed += 0.05f * player.numMinions;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nOffense Mode :\n +20% Minion Damage\n +2 Minion Slot";

                        player.minionDamage += 0.20f;
                        player.maxMinions += 2;
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
            regularRecipe.AddIngredient(ItemID.Cog, 20);

            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
