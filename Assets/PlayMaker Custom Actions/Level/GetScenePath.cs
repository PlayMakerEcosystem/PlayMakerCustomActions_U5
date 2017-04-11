// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager,Get Scene Path
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Returns the relative path of the scene. example: Assets/MyScenes/MyScene.unity")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class GetScenePath : FsmStateAction
	{
		[ActionSection("Output")]
		[RequiredField]
		[Tooltip("Scene path")]
		public FsmString scenePath;
		 
	
		public override void Reset()
		{
			scenePath = null;
		
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER

			Scene target = SceneManager.GetActiveScene();
			scenePath.Value = target.path;
			Finish();

			#else

			Debug.LogWarning("<b>[GetScenePath]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}


	}
}
