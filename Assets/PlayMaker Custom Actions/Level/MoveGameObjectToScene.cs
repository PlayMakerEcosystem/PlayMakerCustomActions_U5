// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager,Move GameObject To Scene
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Move a GameObject from its current scene to a new scene. It is required that the GameObject is at the root of its current scene.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class MoveGameObjectToScene : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Gameobject to move to another scene")]
		public FsmGameObject gameobject;
		[Tooltip("destination scene")]
		public FsmString nameTarget;
	
		public override void Reset()
		{
			gameobject = null;
			nameTarget = null;
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER

			GameObject go = gameobject.Value;

			Scene sceneTarget = SceneManager.GetSceneByName(nameTarget.Value);
			SceneManager.MoveGameObjectToScene(go,sceneTarget);
			Finish();

			#else

			Debug.LogWarning("<b>[Move GameObject To Scene]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}


	}
}
