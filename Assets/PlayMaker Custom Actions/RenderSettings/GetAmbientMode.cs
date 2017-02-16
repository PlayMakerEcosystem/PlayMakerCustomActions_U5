// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
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