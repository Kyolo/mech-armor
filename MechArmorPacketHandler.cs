using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace MechArmor
{
    class MechArmorPacketHandler
    {

        public static MechArmor mod;

        public static void HandlePacket(BinaryReader reader, int sender)
        {
            //Note : We may need to pass the mod instance one day
            MechArmorMessageType type = (MechArmorMessageType)reader.ReadByte();
            switch (type)
            {
                case MechArmorMessageType.MechArmorPlayerSync:
                    {
                        //We need to sync data from a MechArmorPlayer
                        int otherPlayer = reader.ReadByte();
                        MechArmorPlayer modPlr = Main.player[otherPlayer].GetModPlayer<MechArmorPlayer>();
                        modPlr.ArmorState = reader.ReadByte();
                        break;
                    }
                case MechArmorMessageType.MechArmorPlayerArmorStateChanged:
                    {
                        //We need to change the state of the armor
                        int otherPlayer = reader.ReadByte();
                        MechArmorPlayer modPlr = Main.player[otherPlayer].GetModPlayer<MechArmorPlayer>();
                        modPlr.ArmorState = reader.ReadByte();

                        // We also need to send it back to all connected clients from the server
                        if(Main.netMode == Terraria.ID.NetmodeID.Server)
                        {
                            ModPacket p = mod.GetPacket();
                            p.Write((byte)MechArmorMessageType.MechArmorPlayerArmorStateChanged);
                            p.Write(otherPlayer);
                            p.Write(modPlr.ArmorState);
                            // We send the data to all players except the original one
                            p.Send(-1, otherPlayer);
                        }

                        break;
                    }
                default:
                    {
                        //We shouldn't enter here, but a bit of caution never hurt anyone
                        ModContent.GetInstance<MechArmor>().Logger.ErrorFormat("Received unknown packet type {0}", type);
                        break;
                    }
            
            }
        }
    }
}
