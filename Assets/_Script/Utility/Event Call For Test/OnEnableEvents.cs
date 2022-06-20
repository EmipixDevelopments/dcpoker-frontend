using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokerGame
{
	public class OnEnableEvents : EventStorer
	{
		public float eventCallingHoldTime;

		void OnEnable()
		{
			CallStoredEventAfterSomeTime(eventCallingHoldTime);
		}
	}
}
