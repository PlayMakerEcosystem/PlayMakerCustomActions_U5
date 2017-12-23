// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

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

		[Tooltip("The Name of the final GameObject it reached.")]
		public FsmString storeResultName;

		public override void Reset()
		{
			startingFrom = null;
			index = 1;
			storeResult = null;
			storeResultName = null;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(startingFrom);
			var currentChildID = go.transform.GetSiblingIndex();
			currentChildID += index.Value;
			storeResult.Value = go.transform.parent.GetChild(currentChildID).gameObject;
			Finish();
		}
	}
}
