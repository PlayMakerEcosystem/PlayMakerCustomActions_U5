// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the GameObject before or after the specified GameObject (any GameObject under the same parent).")]
	public class GetGameObjectSibling : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to start from.")]
		public FsmOwnerDefault startingFrom;

		[Tooltip("0 = Owner, Negative = before, positive = after the 'Starting From' GameObject. E.g.: -1 would be the GameObject above in the Hirarchy, if any. If none, it wouldn't get the parent, but throw an Error.")]
		public FsmInt index;

		[UIHint(UIHint.Variable)]
		[Tooltip("The final GameObject it reached.")]
		public FsmGameObject storeResult;

		[UIHint(UIHint.Variable)]
		[Tooltip("The Name of the final GameObject it reached.")]
		public FsmString storeResultName;

		[Tooltip("Event send if there could be no sibling found at the specified index.")]
		public FsmEvent notFoundEvent;

		public override void Reset()
		{
			startingFrom = null;
			index = 1;
			storeResult = null;
			storeResultName = null;
			notFoundEvent = null;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(startingFrom);

			int currChildID = go.transform.GetSiblingIndex();
      currChildID += index.Value;

      Transform parent = go.transform.parent;

      if (parent == null)
      {
	      //LogError("Trying to get the sibling of a root object is not allowed!");
	      Fsm.Event(notFoundEvent);
      }
      else
      {
	      if (currChildID < 0 || currChildID > parent.childCount - 1) {
		      //LogError("Specified sibling index is out of bounds!");
		      Fsm.Event(notFoundEvent);
	      } else {
		      if (parent.GetChild(currChildID) != null)
		      {
			      storeResult.Value = parent.GetChild(currChildID).gameObject;
			      storeResultName.Value = storeResult.Value.name;
		      }
		      else Fsm.Event(notFoundEvent);
	      } 
      }

			Finish();
		}
	}
}
