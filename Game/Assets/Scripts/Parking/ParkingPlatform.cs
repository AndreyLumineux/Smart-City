using System.Collections;
using UnityEditor;
using UnityEngine;

// ReSharper disable PossibleLossOfFraction

namespace Parking
{
	public class ParkingPlatform : MonoBehaviour
	{
		public float speed = 50f;

		bool platformMoving = false;
		ParkingMain parkingMain;

		void Awake()
		{
			parkingMain = GetComponentInParent<ParkingMain>();
		}

		public void MovePlatformToSlot(int slot)
		{
			int pack = slot / 4;
			MovePlatformToPack(pack);

			int count = slot % 4;
			switch (count)
			{
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
				case 0:
					PlatformMove(Vector3.back * 3);
					PlatformMove(Vector3.right * 3);
					PlatformMove(Vector3.forward * 2);
					PlatformDropVehicle(Vector3.left);
					break;
			}
		}

		void PlatformDropVehicle(Vector3 right)
		{
			throw new System.NotImplementedException();
		}

		public void MovePlatformToPack(int pack)
		{
			PlatformMove(Vector3.back * (pack / parkingMain.columns));
			PlatformMove(Vector3.right * ((pack % parkingMain.columns - 1 + parkingMain.columns) % parkingMain.columns));
		}

		public void PlatformMove(Vector3 movement)
		{
			StartCoroutine(nameof(PlatformMoveCoroutine), transform.position + movement);
		}

		IEnumerator PlatformMoveCoroutine(Vector3 targetPosition)
		{
			if (platformMoving)
				yield return new WaitForSeconds(0.1f);

			platformMoving = true;
			Vector3 direction = targetPosition - transform.position;
			while (!Mathf.Approximately(0, direction.magnitude))
			{
				transform.Translate(Time.deltaTime * speed * direction.normalized);

				yield return new WaitForFixedUpdate();
			}

			platformMoving = false;
		}
	}
}