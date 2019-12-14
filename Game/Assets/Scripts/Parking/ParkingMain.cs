using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Parking
{
	public class ParkingMain : MonoBehaviour
	{
		public int maxSlots;
		public int columns;
		[FormerlySerializedAs("rounds")] public int rows;
		
		public List<int> slots;

		void Awake()
		{
			slots = new List<int>();
		}
		
		
	}
}
