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
            DisplayName.SetDefault("Strange Wood Helmet");
            Tooltip.SetDefault("10% increased minion damage\nIncrease minion slots by 1");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 8, 0, 0);
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
            player.setBonus = "";


            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 10;


            switch(armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "Defense Mode :\n Increase armor by 2 per minion slot filed\n 5% increased movement speed per minion slot filed";
                        player.statDefense += 2 * player.numMinions;
                        player.moveSpeed += 0.05f * player.numMinions;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "Offense Mode :\n 20% increased minion Damage\n Increase minion slots by 2";

                        player.minionDamage += 0.20f;
                        player.maxMinions += 2;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
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
            regularRecipe.AddIngredient(ModContent.ItemType<StrangeWood>(), 5);

            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
