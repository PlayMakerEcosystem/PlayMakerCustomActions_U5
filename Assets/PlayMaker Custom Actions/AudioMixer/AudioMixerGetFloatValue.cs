// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// original actions from Tropical: http://hutonggames.com/playmakerforum/index.php?topic=11218.msg52963#msg52963
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;
using UnityEngine.Audio;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[ActionTarget(typeof(AudioMixer), "theMixer")]
	[Tooltip("Gets the float value of an exposed parameter for a Unity Audio Mixer. Prior to calling SetFloat and after ClearFloat has been called on this parameter the value returned will be that of the current snapshot or snapshot transition.")]
	public class AudioMixerGetFloatValue : FsmStateAction
	{
		[RequiredField]
		[ObjectType(typeof(AudioMixer))]
		[Tooltip("The Audio Mixer with the exposed parameter.")]
		public FsmObject theMixer;

		[RequiredField]
		[Tooltip("The name of the exposed parameter.")]
		[Title("Name of Parameter")]
		public FsmString exposedFloatName;

		[RequiredField]
		[UIHintAttribute(UIHint.Variable)]
		[Tooltip("Store the Float value in a variable")]
		public FsmFloat storeFloatValue;

		public bool everyFrame;

		public override void Reset()
		{
			theMixer = null;
			exposedFloatName = null;
			storeFloatValue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetMixerFloatValue();

			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetMixerFloatValue();
		}

		public void DoGetMixerFloatValue()
		{
			var _TheMixer = theMixer.Value as AudioMixer;
			var _ExposedFloatName = exposedFloatName.Value;

			if (_TheMixer!=null && !string.IsNullOrEmpty(_ExposedFloatName))
			{
				var _storeFloatValue = 0F;
				_TheMixer.GetFloat (_ExposedFloatName, out _storeFloatValue);
				storeFloatValue.Value = _storeFloatValue;
			}
		}
	}
}