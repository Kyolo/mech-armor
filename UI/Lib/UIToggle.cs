using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace MechArmor.UI.Lib
{
    // If I was in C++ I could simply do :
    //template<typename T>
    //class UIToggle : T {
    //  static_assert(std::is_base_of<UIElement, T>::value);
    //}
    //And I'll be able to easily add a few new methods to an existing classs
    //Instead I need to create a full container object
    //I never thought I'd miss C++ templates
    public class UIToggle : UIElement
    {

        public bool Visible = true;

        public void ToggleVisibility()
        {
            Visible = !Visible;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
                base.Draw(spriteBatch);
            else
                return;
        }

    }
}
