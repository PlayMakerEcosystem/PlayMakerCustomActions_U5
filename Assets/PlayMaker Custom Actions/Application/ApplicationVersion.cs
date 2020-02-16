// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Gets the Application version and can optionnaly format it inside a string")]
	public class ApplicationVersion : FsmStateAction
	{
        [Tooltip("optional format text, use {0} as the replacement tag, Leave to non for no effect")]
        public FsmString format;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmString version;


        public override void Reset()
		{
			format = new FsmString() { UseVariable = true };
			version = null;
		}

		public override void OnEnter()
		{


			if (format.Value.Contains("{0}"))
			{
				version.Value = string.Format(Application.version, format);
			}
			else
			{
				version.Value = Application.version;
			}
            
            Finish();
		}

		public override string ErrorCheck()
		{
			if (!string.IsNullOrEmpty(format.Value) && !format.Value.Contains("{0}"))
			{
				return "format must contains {0} as the replacement tag";
			}
			return "";
		}
	}
}