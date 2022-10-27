using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;


namespace MechArmor.UI
{
    using Lib;
    using Config;

    public class ArmorStateIndicator : UIState
    {
        // All the components of the indicator
        // The text showing the current state / the number of state
        private UIText stateIndicator;
        // The background wheel
        private UIImage background;
        // The overlay symbols
        private UIImage overlay;
        // The symbols shown only when one is activated
        private UIToggle offense;
        private UIToggle defense;
        private UIToggle utility;
        private UIToggle mobility;

        // The container for the components
        private UIElement container;


        // Used to drag the UI
        private Vector2 offset;
        private bool dragging;


        public override void OnInitialize()
        {

            MechArmorDisplayConfig conf = ModContent.GetInstance<MechArmorDisplayConfig>();

            container = new UIElement();
            container.Left.Set(conf.DefaultArmorStatusPositionX, 0);
            container.Top.Set(conf.DefaultArmorStatusPositionY, 0);
            container.Width.Set(148, 0f);
            container.Height.Set(148, 0f);
            container.OnMouseUp += OnContainerMouseUp;
            container.OnMouseDown += OnContainerMouseDown;

            // setting up the background wheel
            background = new UIImage(ModContent.Request<Texture2D>("MechArmor/UI/status_indicator_base"));
            background.Width.Set(148, 0f);
            background.Height.Set(148, 0f);

            // The icons overlay
            overlay = new UIImage(ModContent.Request<Texture2D>("MechArmor/UI/status_indicator_overlay"));
            overlay.Width.Set(148, 0f);
            overlay.Height.Set(148, 0f);

            // And the differents state that will be displayed as required
            offense = new UIToggle();
            offense.Append(new UIImage(ModContent.Request<Texture2D>("MechArmor/UI/status_indicator_offense_active")));
            offense.Width.Set(148, 0f);
            offense.Height.Set(148, 0f);

            //defense = new UIImage(ModContent.Request<Texture2D>("MechArmor/UI/status_indicator_defense_active"));
            defense = new UIToggle();
            defense.Append(new UIImage(ModContent.Request<Texture2D>("MechArmor/UI/status_indicator_defense_active")));
            defense.Width.Set(148, 0f);
            defense.Height.Set(148, 0f);

            //utility = new UIImage(ModContent.Request<Texture2D>("MechArmor/UI/status_indicator_utility_active"));
            utility = new UIToggle();
            utility.Append(new UIImage(ModContent.Request<Texture2D>("MechArmor/UI/status_indicator_utility_active")));
            utility.Width.Set(148, 0f);
            utility.Height.Set(148, 0f);

            //mobility = new UIImage(ModContent.Request<Texture2D>("MechArmor/UI/status_indicator_mobility_active"));
            mobility = new UIToggle();
            mobility.Append(new UIImage(ModContent.Request<Texture2D>("MechArmor/UI/status_indicator_mobility_active")));
            mobility.Width.Set(148, 0f);
            mobility.Height.Set(148, 0f);

            // And we end with the text
            stateIndicator = new UIText("0/0");
            //stateIndicator.Width.Set(148, 0f);
            //stateIndicator.Height.Set(148, 0f);
            stateIndicator.HAlign = 0.5f;
            stateIndicator.VAlign = 0.5f;

            container.Append(background);
            container.Append(offense);
            container.Append(defense);
            container.Append(utility);
            container.Append(mobility);
            container.Append(overlay);
            container.Append(stateIndicator);

            this.Append(container);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // We only draw this UI if we have a compatible armor
            if (Main.LocalPlayer.GetModPlayer<MechArmorPlayer>().ArmorStateType != 0)
                base.Draw(spriteBatch);
            else
                return;
        }
        

        public void OnContainerMouseDown(UIMouseEvent evt, UIElement listeningElement)
        {
            //Based on DragStart from the DragableUIPanel from the ExampleMod
            offset = new Vector2(evt.MousePosition.X - container.Left.Pixels, evt.MousePosition.Y - container.Top.Pixels);
            dragging = true;
        }

        public void OnContainerMouseUp(UIMouseEvent evt, UIElement listeningElement)
        {
            Vector2 end = evt.MousePosition;
            dragging = false;

            container.Left.Set(end.X - offset.X, 0f);
            container.Top.Set(end.Y - offset.Y, 0f);

            container.Recalculate();

            // Change configuration
            //MechArmorDisplayConfig conf = ModContent.GetInstance<MechArmorDisplayConfig>();
            //conf.DefaultArmorStatusPositionX = (int)container.Left.Pixels;
            //conf.DefaultArmorStatusPositionX = (int)container.Top.Pixels;
        }


        public override void Update(GameTime gameTime)
        {
            // Starts directly comes from ExampleMod/DragableUIPanel
            base.Update(gameTime); // don't remove.


            // Checking ContainsPoint and then setting mouseInterface to true is very common. This causes clicks on this UIElement to not cause the player to use current items. 
            if (container.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (dragging)
            {
                container.Left.Set(Main.mouseX - offset.X, 0f); // Main.MouseScreen.X and Main.mouseX are the same.
                container.Top.Set(Main.mouseY - offset.Y, 0f);
                container.Recalculate();
            }

            // Here we check if the DragableUIPanel is outside the Parent UIElement rectangle. 
            // (In our example, the parent would be ExampleUI, a UIState. This means that we are checking that the DragableUIPanel is outside the whole screen)
            // By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution.
            Rectangle parentSpace = GetDimensions().ToRectangle();
            if (!container.GetDimensions().ToRectangle().Intersects(parentSpace))
            {
                container.Left.Pixels = Utils.Clamp(container.Left.Pixels, 0, parentSpace.Right - container.Width.Pixels);
                container.Top.Pixels = Utils.Clamp(container.Top.Pixels, 0, parentSpace.Bottom - container.Height.Pixels);
                // Recalculate forces the UI system to do the positioning math again.
                container.Recalculate();
            }

            //We only display what we need to
            MechArmorPlayer mAP = Main.LocalPlayer.GetModPlayer<MechArmorPlayer>();

            offense.Visible = (mAP.ArmorStateType & (byte)EnumArmorStateType.Offense) != 0;
            defense.Visible = (mAP.ArmorStateType & (byte)EnumArmorStateType.Defense) != 0;
            mobility.Visible = (mAP.ArmorStateType & (byte)EnumArmorStateType.Movement) != 0;
            utility.Visible = (mAP.ArmorStateType & (byte)EnumArmorStateType.Utility) != 0;

            // And we don't forget to set the correct indicator
            stateIndicator.SetText((mAP.ArmorState+1) + "/" + (mAP.MaxArmorStates));
        }
    }
}