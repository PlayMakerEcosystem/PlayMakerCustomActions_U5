// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager, Se Active Scene Num
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Set the scene to be active by scene number.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class SetActiveSceneNum : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Scene number")]
		public FsmInt sceneTargetNum;
	
		private Scene sceneTarget;

		public override void Reset()
		{
			sceneTargetNum = null;
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER


			sceneTarget = SceneManager.GetSceneAt(sceneTargetNum.Value);

			SceneManager.SetActiveScene(sceneTarget);

			Finish();

	

			#else
			isLoaded.Value = false;
			Debug.LogWarning("<b>[SetActiveSceneNum]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}
			
	}
}
