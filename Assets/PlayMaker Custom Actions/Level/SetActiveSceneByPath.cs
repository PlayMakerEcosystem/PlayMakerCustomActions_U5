// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager, Set Active Scene By Path
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Set the scene to be active by scene path.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class SetActiveSceneByPath : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Scene name")]
		public FsmString nameSource;

		private Scene sceneTarget;

		public override void Reset()
		{
			nameSource = null;

		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER


			sceneTarget = SceneManager.GetSceneByPath(nameSource.Value);

			SceneManager.SetActiveScene(sceneTarget);

			Finish();

	

			#else
			isLoaded.Value = false;
			Debug.LogWarning("<b>[SetActiveSceneByPath]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}


	}
}
