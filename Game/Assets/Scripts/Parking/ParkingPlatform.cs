using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

// ReSharper disable PossibleLossOfFraction

namespace Parking
{
	public class ParkingPlatform : MonoBehaviour
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
			MovePlatformToParkingSpot(21);
		}

		public void MovePlatformToParkingSpot(int spot)
		{
			int count = spot % 4;
			int pack = (count == 0 ? 0 : 1) + spot / 4;
			MovePlatformToPack(pack);

			StartCoroutine(nameof(PlatformDropVehicleCoroutine), count);
		}

		void PlatformDropVehicle(Vector3 direction)
		{
			Debug.Log("Dropped vehicle");
		}

		IEnumerator PlatformDropVehicleCoroutine(int count)
		{
			while (platformMoving)
			{
				yield return new WaitForFixedUpdate();
			}

			switch (count)
			{
				case 0:
					PlatformMove(Vector3.back * 3);
					PlatformMove(Vector3.right * 3);
					PlatformMove(Vector3.forward * 2);
					PlatformDropVehicle(Vector3.left);
					break;
				case 1:
					PlatformMove(Vector3.back);
					PlatformDropVehicle(Vector3.right);
					break;
				case 2:
					PlatformMove(Vector3.back * 2);
					PlatformDropVehicle(Vector3.right);
					break;
				case 3:
					PlatformMove(Vector3.back * 3);
					PlatformMove(Vector3.right * 2);
					PlatformDropVehicle(Vector3.forward);
					break;
				default:
					throw new Exception();
			}
		}

		void MovePlatformToPack(int pack)
		{
			PlatformMove((pack / parkingMain.columns) * 3 * Vector3.back);
			PlatformMove(((pack % parkingMain.columns - 1 + parkingMain.columns) % parkingMain.columns) * 3 * Vector3.right);
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
	}
}