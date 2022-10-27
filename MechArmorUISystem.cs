using Terraria;
using Terraria.UI;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace MechArmor {
    public class MechArmorUISystem : ModSystem {

        // UI Elements
        private UserInterface MechArmorUI;
        public UI.ArmorStateIndicator ArmorStateIndicatorState;


        public override void Load()
        {
            // Code only started on client
            if (!Main.dedServ)
            {
                ArmorStateIndicatorState = new UI.ArmorStateIndicator();
                ArmorStateIndicatorState.Activate();

                MechArmorUI = new UserInterface();
                MechArmorUI.SetState(ArmorStateIndicatorState);
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                ArmorStateIndicatorState = null;
                MechArmorUI = null;
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            MechArmorUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MechArmor: Multi-State Armor state display",
                    delegate
                    {
                        MechArmorUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}