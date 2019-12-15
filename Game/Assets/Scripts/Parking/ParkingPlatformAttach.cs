using System;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

namespace Parking
{
	public class ParkingPlatformAttach : MonoBehaviour
	{
		private ParkingMain parkingMain;

		private void Awake()
		{
			parkingMain = GetComponentInParent<ParkingMain>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Vehicle"))
			{
				Attach(other.gameObject);
				other.gameObject.transform.position = transform.position;
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
