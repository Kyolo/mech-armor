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
/* 
Removed because of localization update
/* Removed because of localization change
            DisplayName.SetDefault("Strange Wood Helmet");
*/
*/
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("10% increased minion damage\nIncrease minion slots by 1");
*/
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
            player.GetDamage(DamageClass.Summon) += 0.10f;
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

                        player.GetDamage(DamageClass.Summon) += 0.20f;
                        player.maxMinions += 2;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
            }


        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ItemID.Cog, 20)
            .AddIngredient(ModContent.ItemType<StrangeWood>(), 5)
            .Register();
        }
    }
}
