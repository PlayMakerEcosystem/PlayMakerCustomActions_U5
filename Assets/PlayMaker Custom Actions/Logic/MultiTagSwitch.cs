using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Iterate over the tags on the given gameobject. Defines for each an exit point as a switch flow control statement.")]
	public class MultiTagSwitch : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The GameObject to be tested.")]
		public FsmGameObject gameObject;

		[UIHint(UIHint.Tag)]
		[Tooltip("The Tag to check for.")]
		[CompoundArray("Tag switches", "Compare Tag", "Send Event")]
		public FsmString[] compareTo;

		[Tooltip("On compare success, the event to be sent.")]
		public FsmEvent[] sendEvent;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		[Tooltip("Store the matching Tag in a String variable.")]
		public FsmString storeTag;

		public override void Reset()
		{
			gameObject = null;
			compareTo = new FsmString[1];
			sendEvent = new FsmEvent[1];
			everyFrame = false;
			storeTag = null;
		}

		// Code that runs on entering the state.
		public override void OnEnter()
		{
			CompareAllTags();

			if (!everyFrame)
				Finish();
		}

		// Code that runs every frame.
		public override void OnUpdate()
		{
			CompareAllTags();
		}

		void CompareAllTags()
		{
			for (int i=0; i<compareTo.Length; i++)
			{
				if (gameObject.Value != null)
				{
					if (gameObject.Value.CompareTag(compareTo[i].Value))
					{
						Fsm.Event(sendEvent[i]);
						storeTag = compareTo[i].Value;
					}
				}
			}
		}
	}
}
