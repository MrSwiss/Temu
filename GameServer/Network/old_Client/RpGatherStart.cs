﻿namespace Tera.Network.old_Client
{
    public class RpGatherStart : ARecvPacket
    {
        protected long GatherUid;

        public override void Read()
        {
            GatherUid = ReadLong();
        }

        public override void Process()
        {
            Communication.Global.PlayerService.StartGather(Connection.Player, GatherUid);
        }
    }
}