using System;

namespace Assets.Scripts.World
{
    public enum WorldPropertyStatus
    {
        Open,
        Owned,
        Extorted,
        EnemyOwned
    }

    [Serializable]
    public class WorldPropertyState
    {
        public WorldPropertyStatus Status;
        public WorldPropertySO WorldProperty;

        public WorldPropertyState() { }

        public WorldPropertyState(WorldPropertyState copy)
        {
            Status = copy.Status;
            WorldProperty = copy.WorldProperty;
        }
    }
}