// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager, Merge Scene Path
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Merge the source scene into the destination scene by scene path. Example : Assets/MyScenes/MyScene.unity")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class MergeScenePath : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("source scene")]
		public FsmString nameSource;
		[RequiredField]
		[Tooltip("destination scene")]
		public FsmString nameTarget;
	
		public override void Reset()
		{
			nameSource = null;
			nameTarget = null;
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER

			Scene sceneSource = SceneManager.GetSceneByPath(nameSource.Value);
			Scene sceneTarget = SceneManager.GetSceneByPath(nameTarget.Value);
			SceneManager.MergeScenes(sceneSource,sceneTarget);
			Finish();

			#else

			Debug.LogWarning("<b>[MergeScenePath]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}


	}
}
