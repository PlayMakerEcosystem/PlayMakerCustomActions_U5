// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: uri

using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Converts a string to its unescaped representation.")]
	public class UriDecode : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;
		public FsmBool everyFrame;

		public override void Reset()
		{
			stringVariable = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoReplace();
			
			if (!everyFrame.Value)
				Finish();
		}

		public override void OnUpdate()
		{
			DoReplace();
		}
		
		void DoReplace()
		{
			if (stringVariable == null) return;
			if (storeResult == null) return;
			
			storeResult.Value = Uri.UnescapeDataString(stringVariable.Value);
		}
		
	}
}

