using UnityEngine;
using UnityEngine.Events;

namespace Parking
{
	public class ParkingElevator : MonoBehaviour
	{
		public UnityEvent onElevatorDown;
		public UnityEvent onElevatorUp;

		private Animator animator;
		private static readonly int UpAnimHash = Animator.StringToHash("Up");
		private static readonly int DownAnimHash = Animator.StringToHash("Down");

		private void Awake()
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
