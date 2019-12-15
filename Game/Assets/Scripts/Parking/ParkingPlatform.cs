using System.Collections;
using UnityEngine;

namespace Parking
{
	public class ParkingPlatform : MonoBehaviour
	{
		public float speed = 5f;
		public float loadSpeed = 1f;
		public new Renderer renderer;
		public bool platformMoving = false;
		
		private ParkingMain parkingMain;
		private ParkingElevator parkingElevator;
		private ParkingPlatformAttach parkingPlatformAttach;

		private void Awake()
		{
			parkingMain = GetComponentInParent<ParkingMain>();
			parkingElevator = GetComponentInParent<ParkingElevator>();
			parkingPlatformAttach = GetComponent<ParkingPlatformAttach>();
			
			renderer = GetComponent<Renderer>();

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

		private IEnumerator PlatformMoveCoroutine(Vector3 movement)
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

		private IEnumerator PlatformDropVehicleCoroutine(bool upSide)
		{
			while (platformMoving)
			{
				yield return new WaitForFixedUpdate();
			}

			platformMoving = true;

			if (transform.childCount == 0)
			{
				Debug.LogError("Platform has no vehicle attached, but DropVehicle has been called.");
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

		private IEnumerator MoveObjectCoroutine(GameObject obj, Vector3 targetPosition)
		{
			while (platformMoving)
			{
				yield return new WaitForFixedUpdate();
			}

			platformMoving = true;
			
			float t = 0;
			Vector3 initialPosition = obj.transform.position;
			while (t < 1)
			{
				t += loadSpeed * Time.deltaTime;
				obj.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

				yield return new WaitForFixedUpdate();
			}

			platformMoving = false;
		}
		
		public void LoadVehicle(GameObject vehicle)
		{
			StartCoroutine(MoveObjectCoroutine(vehicle, renderer.bounds.center +
			                                            Vector3.up * ParkingPlatformAttach.PLATFORM_VEHICLE_Y_OFFSET));

		}
	}
}
