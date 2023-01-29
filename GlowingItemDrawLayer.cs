
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

using MechArmor.Buffs;

namespace MechArmor
{
    public class GlowingItemDrawLayer : PlayerDrawLayer {

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return drawInfo.drawPlayer.HeldItem.ModItem is GlowingItem && drawInfo.drawPlayer.controlUseItem;
        }

        // We are drawing just after the held item layer
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HeldItem);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {

            // We only get here when a player has the correct buff, so it currently hold a glowing item

            GlowingItem gItem = drawInfo.heldItem.ModItem as GlowingItem;

            if (gItem != null)
            {
                int num = 10;
                // Drawing code adapted from vanilla terraria
                Vector2 itemCenter = new Vector2(drawInfo.heldItem.width / 2, drawInfo.heldItem.height / 2);
                Vector2 dpItemPos = Main.DrawPlayerItemPos(drawInfo.drawPlayer.gravDir, drawInfo.heldItem.type);
                num = (int)dpItemPos.X;
                itemCenter.Y = dpItemPos.Y;

                Vector2 origin = new Vector2(-num, drawInfo.heldItem.height / 2);
                if (drawInfo.drawPlayer.direction == -1)
                {
                    origin = new Vector2(drawInfo.heldItem.width + num, drawInfo.heldItem.height / 2);
                }
                DrawData drawData = new DrawData(
                    gItem.GetGlowMask(),
                    (drawInfo.ItemLocation - Main.screenPosition + itemCenter).Floor(),
                    null,
                    gItem.GlowMaskColor(),
                    drawInfo.drawPlayer.itemRotation,
                    origin,
                    drawInfo.drawPlayer.GetAdjustedItemScale(drawInfo.heldItem),
                    drawInfo.itemEffect,
                    0
                );

                drawInfo.DrawDataCache.Add(drawData);
            }
        }
    }
}