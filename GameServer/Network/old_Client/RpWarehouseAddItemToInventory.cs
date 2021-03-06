﻿
using Tera.Data.Enums.Item;

namespace Tera.Network.old_Client
{
    class RpWarehouseAddItemToInventory : ARecvPacket
    {
        protected int FromSlot;
        protected int ItemId;
        protected int Count;
        protected int ToSlot;
        protected int Offset;
        protected int Money;

        public override void Read()
        {
            ReadDword();//1
            ReadDword();//3
            ReadDword();//1
            Offset = ReadDword();
            Money = ReadDword();
            ReadDword();//0
            FromSlot = ReadDword();//from slot
            ReadDword();//
            ReadDword();//7
            ItemId = ReadDword();//id
            Count = ReadDword();//counter
            ToSlot = ReadDword();//to slot
        }

        public override void Process()
        {
            if (Offset < 72)
                Offset = 0;
            else if (Offset < 144)
                Offset = 72;
            else if (Offset < 216)
                Offset = 144;
            else
                Offset = 216;

            if (ItemId == -1 && FromSlot == -1 && ToSlot == -1)
            {
                if (Connection.Player.CharacterWarehouse.Money < Money || Money < 0)
                    return;

                Connection.Player.CharacterWarehouse.Money -= Money;
                Connection.Player.Inventory.Money += Money;
            }
            else
            Communication.Global.StorageService.PlaceItemToOtherStorage(Connection.Player, Connection.Player,
                                                                        Connection.Player.CharacterWarehouse, FromSlot,
                                                                        Connection.Player.Inventory, ToSlot, Count);
            Communication.Global.StorageService.ShowPlayerStorage(Connection.Player, StorageType.Inventory);
            Communication.Global.StorageService.ShowPlayerStorage(Connection.Player, StorageType.CharacterWarehouse, false, Offset % 72 != 0 ? 0 : Offset);
        }
    }
}
