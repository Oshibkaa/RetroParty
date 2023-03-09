using UnityEngine;

namespace qtools.qmaze.example4
{
	public class FinishTrigger : MonoBehaviour 
	{
		public delegate void QFinishTriggerHandler();
		public event QFinishTriggerHandler triggerHandlerEvent;

		void OnTriggerEnter () 
		{
			if (triggerHandlerEvent != null)
			{
				triggerHandlerEvent();
			}
		}
	}
}