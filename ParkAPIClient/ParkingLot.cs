using System;

namespace ParkAPIClient
{
    [Serializable]
    public class ParkingLot
    {
        public int Id { get; }
        public int CoordX { get; }
        public int CoordY { get; }
        public int Current { get; }
        public int TotalCapacity { get; }

        public ParkingLot(int id, int coordX, int coordY, int current, int totalCapacity)
        {
            Id = id;
            CoordX = coordX;
            CoordY = coordY;
            Current = current;
            TotalCapacity = totalCapacity;
        }
    }
}
