
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface GlowingItem
{
    public abstract Color GlowMaskColor();

    public abstract Texture2D GetGlowMask();

}