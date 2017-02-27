// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager,Get Active Scene At
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Get the active scene name by number.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class GetActiveSceneAt : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Get Scene name")]
		public FsmInt index;
		[ActionSection("Output")]
		public FsmString name;

	
		public override void Reset()
		{
			name = null;
			index = 0;
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER

			name.Value = SceneManager.GetSceneAt(index.Value).name;
			Finish();

			#else

			Debug.LogWarning("<b>[GetActiveSceneAt]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}


	}
}
