using System;
using UnityEngine;

namespace Parking
{
	public class ParkingElevator : MonoBehaviour
	{
		Animator animator;
		static readonly int UpAnimHash = Animator.StringToHash("Up");
		static readonly int DownAnimHash = Animator.StringToHash("Down");

		void Awake()
		{
			animator = GetComponent<Animator>();
		}
		
		public void ElevatorDown()
		{
			animator.SetTrigger(DownAnimHash);
		}
		
		public void ElevatorUp()
		{
			animator.SetTrigger(UpAnimHash);
		}
	}
}
