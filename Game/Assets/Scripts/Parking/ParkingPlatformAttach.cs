using System;
using Car;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

namespace Parking
{
	public class ParkingPlatformAttach : MonoBehaviour
	{
		public GameObject attachedVehicle;
		
		private const float PLATFORM_VEHICLE_Y_OFFSET = 0.3f;

		private Renderer renderer;
		private ParkingMain parkingMain;

		private void Awake()
		{
			parkingMain = GetComponentInParent<ParkingMain>();
			renderer = GetComponent<Renderer>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (attachedVehicle == null && other.GetComponent<CarAI>() is CarAI carAi)
			{
				carAi.StopMoving();
				Attach(other.gameObject);
				other.gameObject.transform.position = renderer.bounds.center + Vector3.up * PLATFORM_VEHICLE_Y_OFFSET;
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
