// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Text.RegularExpressions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Clean UTF - 8 non-ASCII characters")]
	public class CleanUtf8String : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

        [UIHint(UIHint.TextArea)]
		public FsmString stringValue;
		
        public FsmBool everyFrame;

		public override void Reset()
		{
			stringVariable = null;
			stringValue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetStringValue();
			
			if (!everyFrame.Value)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetStringValue();
		}
		
		void DoSetStringValue()
		{
			if (stringVariable == null) return;
			if (stringValue == null) return;

			
			stringVariable.Value =  Regex.Replace(stringValue.Value, @"[^\u0000-\u007F]", string.Empty);
		}
		
	}
}
