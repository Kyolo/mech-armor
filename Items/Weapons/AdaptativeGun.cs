using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

using MechArmor.Buffs;

namespace MechArmor.Items.Weapons
{
    public class AdaptativeGun : ModItem, GlowingItem
    {   

        #region GlowingItem

        public static Asset<Texture2D> GlowMask;

        // Store the armor state of the last holder, because we can't access the player from the PostDraw functions
        private byte HolderArmorState;


        // Glowing item stuff
        public Color GlowMaskColor(){
            
            Color output = Color.White;

            if((HolderArmorState & (byte)EnumArmorStateType.Defense) != 0) {
                output = Color.Lerp(output, Color.Green, 0.5f);
            }

            if ((HolderArmorState & (byte)EnumArmorStateType.Offense) != 0) {
                output = Color.Lerp(output, Color.Red, 0.5f);
            }

            if ((HolderArmorState & (byte)EnumArmorStateType.Movement) != 0) {
                output = Color.Lerp(output, Color.Yellow, 0.5f);
            }

            if ((HolderArmorState & (byte)EnumArmorStateType.Utility) != 0) {
                output = Color.Lerp(output, Color.Purple, 0.5f);
            }


            return output;
        }

        public Texture2D GetGlowMask() => GlowMask.Value;

        #endregion

        #region Defaults

        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults() {
            Item.width = 98;
            Item.height = 34;
            Item.rare = ItemRarityID.Yellow;

            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;

            // TODO: find vanilla gun sound that match, I can't create one by myself
            Item.UseSound = SoundID.Item40;

            // Weapon properties
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 50;
            Item.knockBack = 7;

            // Gun properties
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 16f;//TODO: tweaks
            Item.useAmmo = AmmoID.Bullet;

            HolderArmorState = 0;
            
        }

        #endregion

        #region Loading/Unloading

        public override void Load(){
            if(!Main.dedServ)// Loading only on clients
                GlowMask = ModContent.Request<Texture2D>("MechArmor/Items/Weapons/AdaptativeGun_Glowmask");
        }

        public override void Unload()
        {
            GlowMask = null;
        }

        #endregion

        #region Ticking
        public override void UpdateInventory(Player player)
        {
            // We store the armor state of the last holder, because we can't access the player from the PostDraw functions
            this.HolderArmorState = player.GetModPlayer<MechArmorPlayer>().PreviousArmorStateType;

            // We also check if we need to apply the Glowing Item buff
        }
        #endregion

        #region Drawing
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.Draw(GlowMask.Value,
                position,
                null,
                GlowMaskColor(),
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            spriteBatch.Draw(
                GlowMask.Value,
                new Vector2(
			    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
			    Item.position.Y - Main.screenPosition.Y + Item.height - GlowMask.Value.Height * 0.5f
	            ),
                null,// NOTE: null draw the entire texture
                GlowMaskColor(),
                rotation,
                GlowMask.Value.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
                );
        }

        public override Vector2? HoldoutOffset(){
            return new Vector2(-16f, 6f);// Offset render to allow for a better render
        }


        /*
        // This hooks doesn't exist yet
        // See https://github.com/tModLoader/tModLoader/pull/2768
        public void PostDrawHeldItem(ref PlayerDrawSet drawInfo, List<DrawData> heldItemDrawData)
        {
            int num = 10;
            // Drawing code adapted from vanilla terraria
            Vector2 itemCenter = new Vector2(Item.width / 2, Item.height / 2);
            Vector2 dpItemPos = Main.DrawPlayerItemPos(drawInfo.drawPlayer.gravDir, Item.type);
            num = (int)dpItemPos.X;
            itemCenter.Y = dpItemPos.Y;

            Vector2 origin = new Vector2(-10, Item.height / 2);
            if (drawInfo.drawPlayer.direction == -1) 
            {      
                origin = new Vector2(Item.width + 10, Item.height / 2);
            }
            DrawData drawData = new DrawData(
                GlowMask.Value,
                (drawInfo.ItemLocation - Main.screenPosition + itemCenter).Floor(),// NOTE: .Floor will have a similar effect to the (int) cast in the vanilla code (next commented line)
                //new Vector2((int)(drawInfo.ItemLocation.X - Main.screenPosition.X + itemCenter.X), (int)(drawInfo.ItemLocation.Y - Main.screenPosition.Y + itemCenter.Y)),
                null,
                GlowMaskColor(),
                drawInfo.drawPlayer.itemRotation,
                origin,
                drawInfo.drawPlayer.GetAdjustedItemScale(Item),
                drawInfo.itemEffect,
                0
            );

            heldItemDrawData.Add(drawData);
        }
        TEST*/

        #endregion

        #region Stats Modification
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if((HolderArmorState & (byte)EnumArmorStateType.Defense) != 0) {
                // If we are in a defensive state, we increase knockback, at the price of a bit of damage
                knockback *= 2.0f;
                damage = (int)((float)damage * 0.9f);
            }

            if ((HolderArmorState & (byte)EnumArmorStateType.Offense) != 0) {
                // If we are in a offensive state, we decrease knockback for more damage
                knockback /= 2.0f;
                damage = (int)((float)damage * 1.2f);
            }

            if ((HolderArmorState & (byte)EnumArmorStateType.Movement) != 0) {
                // If we are in a movement state, we increase the projectile velocity
                velocity *= 2.0f;
            }

            if ((HolderArmorState & (byte)EnumArmorStateType.Utility) != 0) {
                // If we are in an utility state, we move the bullet to spawn behind the player
                // mostly because I don't have any better idea
                position = (position - player.position) * -1;
                // Maybe change bullet to something else ? Some kind of piercing laser ?
            }

        }

        #endregion


        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddTile(TileID.Autohammer)
            .AddIngredient(ItemID.SniperRifle)
            .AddIngredient(ItemID.TacticalShotgun)
            .AddIngredient(ItemID.ShroomiteBar, 10)
            .Register();
        }
    }
}
