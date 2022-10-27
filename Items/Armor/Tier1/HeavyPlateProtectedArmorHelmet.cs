using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier1
{
    [AutoloadEquip(EquipType.Head)]
    class HeavyPlateProtectedArmorHelmet : ModItem
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
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<HeavyPlateProtectedArmorBreastplate>() && legs.type == ItemType<HeavyPlateProtectedArmorLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Melee attacks light enemies on fire";
            player.magmaStone = true;

            // When health is high, we give a better defense
            if(player.statLife > player.statLifeMax2 *.20f)
            {
                player.setBonus += "\nDefense Mode :\n +60% Armor\n -25% Movement Speed";
                player.statDefense += 11; //(6+7+6)*.6 = 11
                player.moveSpeed -= 0.25f;
                player.GetModPlayer<MechArmorPlayer>().ArmorStateType = (byte)EnumArmorStateType.Defense;
            }
            else // Otherwise when health is low, we add move speed to better move away
            {
                player.setBonus += "\nMovement Mode :\n -50% Armor\n +50% Movement Speed\n -10% Damage";
                player.statDefense -= 10;
                player.moveSpeed += 0.5f;

                player.GetDamage(DamageClass.Generic) -= 0.1f;
                player.GetModPlayer<MechArmorPlayer>().ArmorStateType = (byte)EnumArmorStateType.Movement;
            }
        }
        public override void AddRecipes()
        {

            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ModContent.ItemType<PlateProtectedArmorHelmet>())
            .AddIngredient(ItemID.HellstoneBar, 10)
            .Register();
        }
    }
}
