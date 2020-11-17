using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace MechArmor.Commands
{
    class ArmorStateCommand : ModCommand
    {
        public override string Command => "ma_debug_as";

        public override CommandType Type => CommandType.World;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MechArmorPlayer plr = caller.Player.GetModPlayer<MechArmorPlayer>();

            caller.Reply("Your ArmorState is "+plr.ArmorState, Color.White);
        }
    }
}
