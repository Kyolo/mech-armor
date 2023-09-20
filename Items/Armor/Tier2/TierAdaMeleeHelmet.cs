using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier2
{
    [AutoloadEquip(EquipType.Head)]
    class TierAdaMeleeHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 3, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 22;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<TierAdaBreastplate>() && legs.type == ItemType<TierAdaLeggings>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Melee) += 5;
            player.GetDamage(DamageClass.Melee) += 0.10f;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = "";

            MechArmorPlayer mAP = player.GetModPlayer<MechArmorPlayer>();
            mAP.SetMaxArmorStates(2);
            mAP.ArmorCooldownDuration = 10;


            switch(mAP.ArmorState)
            {
                case 0:
                    player.setBonus += "Offensive Movement Mode :\n 20% Increased Movement Speed\n 20% Increased Melee Swing Speed";
                    player.moveSpeed += 0.20f;
                    player.GetAttackSpeed(DamageClass.Melee) += 0.20f;
                    mAP.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    break;
                case 1:
                    player.setBonus += "Defense Mode :\n 50% Increased Armor\n 10% Reduced Movement Speed";
                    player.statDefense += 24; // (22 + 15 +11) * 0.5 = 24
                    player.moveSpeed -= 0.10f;
                    mAP.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    break;
            }
        }
        public override void AddRecipes()
        {

            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddRecipeGroup("MechArmor:Bars:HMT1", 10)
            .AddRecipeGroup("MechArmor:Bars:HMT2", 10)
            .AddRecipeGroup("MechArmor:Bars:HMT3", 10)
            .AddIngredient(ItemID.CrystalShard, 40)
            .Register();
        }
    }
}
