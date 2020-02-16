// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.Rendering;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Gets the RenderSettings Ambient mode.")]
	public class GetAmbientMode : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(AmbientMode))]
		[Tooltip("Store the Ambient mode")]
		public FsmEnum ambientMode;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the Ambient mode as a string")]
		public FsmString ambientModeAsString;

		public override void Reset()
		{
			ambientMode = null;
			ambientModeAsString = null;
		}

		public override void OnEnter()
		{
			DoSetAmbientColor();

			Finish();
		}
		
		void DoSetAmbientColor()
		{
			if (!ambientMode.IsNone)  ambientMode.Value = RenderSettings.ambientMode;
			if (!ambientModeAsString.IsNone) ambientModeAsString.Value = RenderSettings.ambientMode.ToString ();
		}
	}
}