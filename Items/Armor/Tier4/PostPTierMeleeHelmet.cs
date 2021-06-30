using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Head)]
    class PostPTierMeleeHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("WIP");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 90;
            item.rare = ItemRarityID.Orange;
            item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.05f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<PostPTierMeleeBreastplate>() && legs.type == ItemType<PostPTierMeleeLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "WIP";
            player.aggro += 350;

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 5;

            // And then we allow the use of heavy guns


            switch(armorPlayer.ArmorState)
            {
                case 0:// Setup mode : allow movement to prepare for aiming
                    {
                        player.setBonus += "\nOffense Mode :\n +40% Melee Swing Speed\n +25% Movement Speed";
                        player.meleeSpeed += 0.40f;
                        player.moveSpeed += 0.25f;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nDefensive Utility Mode :\n -50% Movement Speed\n +50% Armor";
                        armorPlayer.ProjectileAttractor = true;
                        armorPlayer.ProjectileAttractorRange = 10*16;//10 blocks times 16pixel per block
                        player.moveSpeed -= 0.50f;
                        player.statDefense += 30; // (20 + 25 + 15) * 0.5 = 30
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
            regularRecipe.AddTile(TileID.MythrilAnvil);
            regularRecipe.AddIngredient(ModContent.ItemType<PaladinArmorShard>(), 6);
            regularRecipe.AddIngredient(ItemID.ChlorophyteBar, 6);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
