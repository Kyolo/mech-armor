using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Weapons
{
	public class TestSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Sword Of The Developper"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("This is a basic modded sword.");
		}

		public override void SetDefaults() 
		{
			item.damage = 100;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
            if (ModContent.GetInstance<MechArmorServerConfig>().UseTestingRecipes)
            {
                ModRecipe wRecipe = new ModRecipe(mod);
                wRecipe.AddRecipeGroup("Wood", 10);
                wRecipe.AddTile(TileID.WorkBenches);
                wRecipe.SetResult(this);
                wRecipe.AddRecipe();
            }
        }
	}
}