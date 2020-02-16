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
	[Tooltip("Sets the float values of multiple exposed parameters for a Unity Audio Mixer.")]
	public class AudioMixerSetMultiFloatValues : FsmStateAction
	{
		[RequiredField]
		[ObjectType(typeof(AudioMixer))]
		[Tooltip("The Audio Mixer with the exposed parameter(s).")]
		public FsmObject theMixer;

		[CompoundArray("Count", "Name of Parameter", "Float Value")]
		[RequiredField]
		[Tooltip("The name of the exposed parameter.")]
		public FsmString[] exposedFloatName;
		public FsmFloat[] floatvalue;
		public bool everyFrame;

		private string _ExposedFloatName;
		private float _Floatvalue;

		public override void Reset()
		{
			theMixer = null;
			exposedFloatName = new FsmString[1];
			floatvalue = new FsmFloat[1];
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetMixerFloatValues();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetMixerFloatValues();
		}
		
		public void DoSetMixerFloatValues()
		{
			var _TheMixer = theMixer.Value as AudioMixer;

			if (_TheMixer!=null)
			{
				for (int i = 0; i<exposedFloatName.Length;i++)
				{
					if(!exposedFloatName[i].IsNone || !exposedFloatName[i].Value.Equals("")) 
						_ExposedFloatName = exposedFloatName[i].Value;
						_Floatvalue = floatvalue[i].IsNone ? 0f : floatvalue[i].Value;
						_TheMixer.SetFloat (_ExposedFloatName, _Floatvalue);
				}
			}
		}
	}
}