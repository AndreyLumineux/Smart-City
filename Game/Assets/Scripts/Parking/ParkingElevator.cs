using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Parking
{
	public class ParkingElevator : MonoBehaviour
	{
		public UnityEvent onElevatorDown;
		public UnityEvent onElevatorUp;

		Animator animator;
		static readonly int UpAnimHash = Animator.StringToHash("Up");
		static readonly int DownAnimHash = Animator.StringToHash("Down");

		void Awake()
		{
			animator = GetComponent<Animator>();
		}

		public void InvokeOnElevatorDown()
		{
			onElevatorDown.Invoke();
		}
		
		public void InvokeOnElevatorUp()
		{
			onElevatorUp.Invoke();
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
