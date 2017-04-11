// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager,scene count
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("The total number of scenes.The number of currently loaded scenes will be returned.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class SceneCount : FsmStateAction
	{
		[ActionSection("Count output")]
		[RequiredField]
		[Tooltip("Scene count")]
		public FsmInt sceneCount;
		 
	
		public override void Reset()
		{
			sceneCount = 0;
		
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER

			sceneCount.Value = SceneManager.sceneCount;
			Finish();

			#else

			Debug.LogWarning("<b>[SceneCount]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}


	}
}
