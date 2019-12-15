using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ParkAPIClient;
using UnityEngine;
using UnityEngine.Serialization;

namespace Parking
{
	public class ParkingMain : MonoBehaviour
	{
		public int id;
		public int maxSpots;
		public int columns;
		public int rows;
		public List<ParkingSpot> parkingSpots;

		private ParkingElevator parkingElevator;
		private ParkingPlatform parkingPlatform;
		
		private readonly ParkApiClient client = new ParkApiClient("http://localhost");

		private void Awake()
		{
			parkingSpots = new List<ParkingSpot>();

			for (int i = 0; i < maxSpots; i++)
			{
				parkingSpots.Add(new ParkingSpot());
			}
			
			parkingElevator = GetComponent<ParkingElevator>();
			parkingPlatform = GetComponentInChildren<ParkingPlatform>();

			parkingElevator.onElevatorDown.AddListener(MovePlatform);
		}

		private void Update()
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

		private void MovePlatform()
		{
			ParkingSpot spot = FindFirstFreeParkingSpot();
			parkingPlatform.MovePlatformToParkingSpot(parkingSpots.IndexOf(spot) + 1);
			spot.Occupied = true;
			int current = parkingSpots.Count(s => s.Occupied);
			client.UpdateCurrentAsync(id, current);
		}

		private ParkingSpot FindFirstFreeParkingSpot()
		{
			return parkingSpots.FirstOrDefault(parkingSpot => !parkingSpot.Occupied);
		}
	}
}
