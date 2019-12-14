using System;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

namespace Parking
{
	public class ParkingPlatformAttach : MonoBehaviour
	{
		ParkingMain parkingMain;

		void Awake()
		{
			parkingMain = GetComponentInParent<ParkingMain>();
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Vehicle"))
			{
				Attach(other.gameObject);
				parkingMain.StockVehicle();
			}
		}

		public void Attach(GameObject vehicle)
		{
			vehicle.transform.SetParent(this.transform);
		}

		public void Dettach(GameObject vehicle)
		{
			vehicle.transform.parent = null;
		}
	}
}
