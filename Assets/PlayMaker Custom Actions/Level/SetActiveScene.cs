// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager, Set Active Scene
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Set the scene to be active.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class SetActiveScene : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Scene name")]
		public FsmString name;
	

		private Scene sceneTarget;

		public override void Reset()
		{
			name = null;
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER
		

			sceneTarget = SceneManager.GetSceneByName(name.Value);

			Debug.Log(sceneTarget.name);

			SceneManager.SetActiveScene(sceneTarget);

			Finish();

	

			#else
			isLoaded.Value = false;
			Debug.LogWarning("<b>[SetActiveScene]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}
			
	}
}
