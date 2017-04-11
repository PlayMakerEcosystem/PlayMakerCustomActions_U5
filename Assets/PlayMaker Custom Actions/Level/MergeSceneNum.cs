// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager, Merge Scene Num
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Merge the source scene into the destination scene by scene number.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class MergeSceneNum : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("source scene")]
		public FsmInt intSource;
		[Tooltip("destination scene")]
		public FsmInt intTarget;
	
		public override void Reset()
		{
			intSource = 0;
			intTarget = 0;
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER

			Scene sceneSource = SceneManager.GetSceneAt(intSource.Value);
			Scene sceneTarget = SceneManager.GetSceneAt(intTarget.Value);
			SceneManager.MergeScenes(sceneSource,sceneTarget);
			Finish();

			#else

			Debug.LogWarning("<b>[MergeSceneNum]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}


	}
}
