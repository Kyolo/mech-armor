using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier1
{
    [AutoloadEquip(EquipType.Head)]
    class PlateProtectedArmorHelmet : ModItem
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
            Item.defense = 4;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<PlateProtectedArmorBreastplate>() && legs.type == ItemType<PlateProtectedArmorLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Gives additionnal armor and reduce speed when healthy.\nAdd move speed to flee when heavily damaged.";

            // When health is high, we give a better defense
            if(player.statLife > player.statLifeMax2 * .15f)
            {
                player.setBonus += "\nDefense Mode :\n +50% Armor\n -20% Movement Speed";
                player.statDefense += 6; //(5+6+5)/2 = 8
                player.moveSpeed -= 0.2f;
                player.GetModPlayer<MechArmorPlayer>().ArmorStateType = (byte)EnumArmorStateType.Defense;
            }
            else // Otherwise when health is low, we add move speed to better move away
            {
                player.setBonus += "\nMovement Mode :\n -50% Armor\n +50% Movement Speed\n -10% Damage";
                player.statDefense -= 8;
                player.moveSpeed += 0.5f;

                player.GetDamage(DamageClass.Generic) -= 0.1f;
                player.GetModPlayer<MechArmorPlayer>().ArmorStateType = (byte)EnumArmorStateType.Movement;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ItemID.DartTrap, 2)
            .AddRecipeGroup("MechArmor:Armor:Head:T2")
            .Register();
        }
    }
}
