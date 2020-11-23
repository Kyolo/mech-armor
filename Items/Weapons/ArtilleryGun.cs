using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using MechArmor.Items.Armor;

namespace MechArmor.Items.Weapons
{
    class ArtilleryGun : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A heavy gun, can only be reliably used with a purpose-built armor or heavier.");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 24;
			item.ranged = true;
			
			item.width = 26;
			item.height = 28;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 3;
			item.value = Item.buyPrice(0, 30, 0, 0);
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item44;
			item.shoot = AmmoID.Bullet;
			
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if(player.GetModPlayer<MechArmorPlayer>().CanUseHeavyGuns)
            {
				// Increased damage for now
				damage *= 2;
            }
			
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}
		
	}
}
