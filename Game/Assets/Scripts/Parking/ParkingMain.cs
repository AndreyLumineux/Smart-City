using System;
using System.Collections.Generic;
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
			parkingElevator = GetComponent<ParkingElevator>();
			parkingPlatform = GetComponentInChildren<ParkingPlatform>();
			
			parkingElevator.onElevatorDown.AddListener(MovePlatform);
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Vehicle"))
			{
				Invoke(nameof(StockVehicle), 1f);
			}
		}

		void StockVehicle()
		{
			parkingElevator.ElevatorDown();
		}

		void MovePlatform()
		{
			ParkingSpot spot = FindFirstFreeParkingSpot();
			parkingPlatform.MovePlatformToParkingSpot(parkingSpots.IndexOf(spot));
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
