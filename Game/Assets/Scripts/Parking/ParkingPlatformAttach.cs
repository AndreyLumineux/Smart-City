using Car;
using UnityEngine;

namespace Parking
{
	public class ParkingPlatformAttach : MonoBehaviour
	{
		public GameObject attachedVehicle;
		
		public const float PLATFORM_VEHICLE_Y_OFFSET = 0.3f;

		private ParkingMain parkingMain;
		private ParkingPlatform parkingPlatform;

		private void Awake()
		{
			parkingMain = GetComponentInParent<ParkingMain>();
			parkingPlatform = GetComponent<ParkingPlatform>();
		}

		private void OnTriggerEnter(Collider other)
		{
			// ReSharper disable once PatternAlwaysOfType
			if (attachedVehicle == null && !parkingPlatform.platformMoving && other.GetComponent<CarAI>() is CarAI carAi)
			{
				carAi.StopMoving();
				parkingPlatform.LoadVehicle(other.gameObject);
				Attach(other.gameObject);
				parkingMain.StockVehicle();
			}
		}

		public void Attach(GameObject vehicle)
		{
			attachedVehicle = vehicle;
			vehicle.transform.SetParent(this.transform);
		}

		public void Dettach(GameObject vehicle)
		{
			attachedVehicle = null;
			vehicle.transform.parent = null;
		}
	}
}
