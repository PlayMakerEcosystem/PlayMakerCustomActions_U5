// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager, Merge Scene
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Merge the source scene into the destination scene.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class MergeScene : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("source scene")]
		public FsmString nameSource;
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

			Scene sceneSource = SceneManager.GetSceneByName(nameSource.Value);
			Scene sceneTarget = SceneManager.GetSceneByName(nameTarget.Value);
			SceneManager.MergeScenes(sceneSource,sceneTarget);
			Finish();

			#else

			Debug.LogWarning("<b>[MergeScene]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}


	}
}
