using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

// ReSharper disable PossibleLossOfFraction

namespace Parking
{
	public class ParkingPlatformNew : MonoBehaviour
	{
		public float speed = 5f;

		bool platformMoving = false;
		ParkingMain parkingMain;

		void Awake()
		{
			parkingMain = GetComponentInParent<ParkingMain>();
		}

		void Start()
		{
			MovePlatformToParkingSpot(45);
		}

		public void MovePlatformToParkingSpot(int spot)
		{
			int x = (spot - 1) / parkingMain.columns;
			int r = (x + 1) / 2;
			PlatformMove(r * 3 * Vector3.back);

			int e = (spot - 1) % parkingMain.columns;
			int a = e + 1 + (e / 2);
			PlatformMove(a * Vector3.right);

			bool upSide = (x % 2 != 0);
			StartCoroutine(nameof(PlatformDropVehicleCoroutine), upSide);
		}

		public void PlatformMove(Vector3 movement)
		{
			StartCoroutine(nameof(PlatformMoveCoroutine), movement);
		}

		IEnumerator PlatformMoveCoroutine(Vector3 movement)
		{
			while (platformMoving)
			{
				yield return new WaitForFixedUpdate();
			}

			platformMoving = true;

			float t = 0;
			Vector3 initialPosition = transform.position;
			Vector3 targetPosition = initialPosition + movement;
			Vector3 direction = targetPosition - initialPosition;
			while (t < 1)
			{
				t += speed * Time.deltaTime / direction.magnitude;
				transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

				yield return new WaitForFixedUpdate();
			}

			platformMoving = false;
		}
		
		void PlatformDropVehicle(Vector3 direction)
		{
			Debug.Log("Dropped vehicle");
		}

		IEnumerator PlatformDropVehicleCoroutine(bool upSide)
		{
			while (platformMoving)
			{
				yield return new WaitForFixedUpdate();
			}

			if (upSide)
			{
				PlatformDropVehicle(Vector3.forward);
			}
			else
			{
				PlatformDropVehicle(Vector3.back);
			}
		}

//		void MovePlatformToPack(int pack)
//		{
//			PlatformMove((pack / parkingMain.columns) * 3 * Vector3.back);
//			PlatformMove(((pack % parkingMain.columns - 1 + parkingMain.columns) % parkingMain.columns) * 3 * Vector3.right);
//		}
	}
}