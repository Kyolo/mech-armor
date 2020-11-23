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
			item.damage = 48;
			item.ranged = true;

			item.width = 40;
			item.height = 40;

			item.useTime = 60;
			item.useAnimation = 60;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;

			item.knockBack = 3;
			item.value = Item.buyPrice(0, 30, 0, 0);

			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item11;
			item.useAmmo = AmmoID.Bullet;
			item.shoot = ProjectileID.Bullet;
			item.shootSpeed = 16f;
			
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if(!player.GetModPlayer<MechArmorPlayer>().CanUseHeavyGuns)
            {
				return false;
            }
			
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

    }
}
