using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier3
{
    [AutoloadEquip(EquipType.Head)]
    class HolyTierMeleeHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
/* Removed because of localization change
            DisplayName.SetDefault("Holy Fencer Helmet");
*/
*/
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("5% increased melee damage");
*/
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 24;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<HolyTierBreastplate>() && legs.type == ItemType<HolyTierLeggings>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.05f;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = "";

            MechArmorPlayer mAP = player.GetModPlayer<MechArmorPlayer>();
            mAP.SetMaxArmorStates(3);
            mAP.ArmorCooldownDuration = 10;


            switch(mAP.ArmorState)
            {
                case 0:
                    player.setBonus += "Offensive Movement Mode :\n 20% Increased movement speed\n +30% Melee Swing Speed";
                    player.moveSpeed += 0.20f;
                    player.GetAttackSpeed(DamageClass.Melee) += 0.30f;
                    mAP.ArmorStateType = (byte)EnumArmorStateType.Offense | (byte)EnumArmorStateType.Movement;
                    break;
                case 1:
                    player.setBonus += "Defense Mode :\n 50% additional armor\n 10% increased damage reduction\n 20% reduced movement speed";
                    player.statDefense += 25; // (24 + 15 +11) * 0.5 = 24
                    player.moveSpeed -= 0.20f;
                    player.endurance += 0.10f;
                    mAP.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    break;
                case 2:
                    player.setBonus += "Movement Mode :\n 20% increased movement speed\n 25% increased wing time";
                    player.moveSpeed += 0.20f;
                    player.wingTimeMax = (int)((float)player.wingTimeMax * 1.25f);
                    mAP.ArmorStateType = (byte)EnumArmorStateType.Movement;
                    break;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddIngredient(ItemID.SoulofSight, 5)
            .AddIngredient(ItemID.UnicornHorn, 5)
            .Register();
        }
    }
}
