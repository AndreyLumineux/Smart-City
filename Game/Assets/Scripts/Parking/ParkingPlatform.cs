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
		ParkingElevator parkingElevator;
		ParkingPlatformAttach parkingPlatformAttach;

		void Awake()
		{
			parkingMain = GetComponentInParent<ParkingMain>();
			parkingElevator = GetComponentInParent<ParkingElevator>();
			parkingPlatformAttach = GetComponent<ParkingPlatformAttach>();
		}

		void Start()
		{
//			MovePlatformToParkingSpot(65);
		}

		public void RetrievePlatform(int verticalMoves, int horizontalMoves)
		{
			PlatformMove(horizontalMoves * 2 * Vector3.left);
			PlatformMove(verticalMoves * 6 * Vector3.forward);
		}
		
		public void MovePlatformToParkingSpot(int spot)
		{
			int a = (spot - 1) / parkingMain.columns;
			int backMoves = (a + 1) / 2;
			PlatformMove(backMoves * 6 * Vector3.back);

			int b = (spot - 1) % parkingMain.columns;
			int rightMoves = b + 1 + (b / 2);
			PlatformMove(rightMoves * 2 * Vector3.right);

			bool upSide = (a % 2 != 0);
			StartCoroutine(nameof(PlatformDropVehicleCoroutine), upSide);
			
			RetrievePlatform(backMoves, rightMoves);

			StartCoroutine(nameof(ElevatorUpCoroutine));
		}

		public IEnumerator ElevatorUpCoroutine()
		{
			while (platformMoving)
			{
				yield return new WaitForFixedUpdate();
			}

			platformMoving = true;
			parkingElevator.ElevatorUp();
			platformMoving = false;
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

			platformMoving = true;

			if (transform.childCount == 0)
			{
				Debug.LogError("Platform has no vehicle attached, but DropVehicle has been tried.");
			}
			Transform vehicle = transform.GetChild(0);
			parkingPlatformAttach.Dettach(vehicle.gameObject);
			
			Vector3 direction = (upSide) ? Vector3.forward : Vector3.back;
			direction = direction * 2;
			float t = 0;
			Vector3 initialPosition = vehicle.position;
			Vector3 targetPosition = initialPosition + direction;
			while (t < 1)
			{
				t += speed * Time.deltaTime / direction.magnitude;
				vehicle.position = Vector3.Lerp(initialPosition, targetPosition, t);

				yield return new WaitForFixedUpdate();
			}

			platformMoving = false;
		}

		
		
		
		
		
		
		
//		void MovePlatformToPack(int pack)
//		{
//			PlatformMove((pack / parkingMain.columns) * 3 * Vector3.back);
//			PlatformMove(((pack % parkingMain.columns - 1 + parkingMain.columns) % parkingMain.columns) * 3 * Vector3.right);
//		}
	}
}