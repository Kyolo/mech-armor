using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier1
{
    [AutoloadEquip(EquipType.Head)]
    class LightPlateProtectedArmorHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 90;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<LightPlateProtectedArmorBreastplate>() && legs.type == ItemType<LightPlateProtectedArmorLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";

            // When health is high, we give a better defense
            if(player.statLife > player.statLifeMax2 *.20)
            {
                player.setBonus += "\nDefense Mode :\n +40% Armor\n -10% Movement Speed";
                player.statDefense += 6; //(5+6+5)*.4 = 8
                player.moveSpeed -= 0.1f;
                player.GetModPlayer<MechArmorPlayer>().ArmorStateType = (byte)EnumArmorStateType.Defense;
            }
            else // Otherwise when health is low, we add move speed to better move away
            {
                player.setBonus += "\nMovement Mode :\n -50% Armor\n +60% Movement Speed\n -5% Damage";
                player.statDefense -= 6;
                player.moveSpeed += 0.6f;

                player.GetDamage(DamageClass.Generic) -= 0.05f;
                player.GetModPlayer<MechArmorPlayer>().ArmorStateType = (byte)EnumArmorStateType.Movement;
            }
        }
        public override void AddRecipes()
        {

            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ModContent.ItemType<PlateProtectedArmorLeggings>())
            .AddRecipeGroup("MechArmor:Bars:Evil", 10)
            .Register();
        }
    }
}
