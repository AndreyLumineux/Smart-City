using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Serialization;

namespace Parking
{
	public class ParkingMain : MonoBehaviour
	{
		public int maxSpots;
		public int columns;
		public int rows;
		public List<ParkingSpot> parkingSpots;

		ParkingElevator parkingElevator;
		ParkingPlatform parkingPlatform;

		void Awake()
		{
			parkingSpots = new List<ParkingSpot>();
			
			ParkingSpot spot;
			for (int i = 0; i < maxSpots; i++)
			{
				spot = new ParkingSpot();
				parkingSpots.Add(spot);
			}
			
			parkingElevator = GetComponent<ParkingElevator>();
			parkingPlatform = GetComponentInChildren<ParkingPlatform>();

			parkingElevator.onElevatorDown.AddListener(MovePlatform);
		}

		void Update()
		{
			foreach (ParkingSpot spot in parkingSpots)
			{
				spot.TimeElapsed += Time.deltaTime;
			}
		}

		public void StockVehicle()
		{
			parkingElevator.ElevatorDown();
		}

		void MovePlatform()
		{
			ParkingSpot spot = FindFirstFreeParkingSpot();
			parkingPlatform.MovePlatformToParkingSpot(parkingSpots.IndexOf(spot) + 1);
			spot.Occupied = true;
		}

		ParkingSpot FindFirstFreeParkingSpot()
		{
			foreach (ParkingSpot parkingSpot in parkingSpots)
			{
				if (!parkingSpot.Occupied)
				{
					return parkingSpot;
				}
			}

			return null;
		}
	}
}
