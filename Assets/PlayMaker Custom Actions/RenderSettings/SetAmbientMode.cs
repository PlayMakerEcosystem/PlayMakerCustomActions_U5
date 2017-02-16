// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.Rendering;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Sets the RenderSettings Ambient mode.")]
	public class SetAmbientMode : FsmStateAction
	{
		[RequiredField]
		[ObjectType(typeof(AmbientMode))]
		[Tooltip("The Ambient Mode")]
		public FsmEnum ambientMode;


		public override void Reset()
		{
			ambientMode = null;
		}

		public override void OnEnter()
		{
			DoSetAmbientColor();

			Finish();
		}
		
		void DoSetAmbientColor()
		{
			RenderSettings.ambientMode = (AmbientMode)ambientMode.Value;
		}
	}
}