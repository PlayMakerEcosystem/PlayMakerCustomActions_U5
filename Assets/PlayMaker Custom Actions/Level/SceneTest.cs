// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: scenemanager,scene manager,Scene Test
// require minimum 5.3

using UnityEngine;
using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Sends Events based on the value of isCurrentScene if the current scene match the name input.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12649.0")]
	public class SceneTest : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("Scene name")]
		public FsmString name;

		[ActionSection("Output")]
		public FsmBool isCurrentScene;

		[Tooltip("Event to send if the Bool variable is True.")]
		public FsmEvent isTrue;

		[Tooltip("Event to send if the Bool variable is False.")]
		public FsmEvent isFalse;

		[Tooltip("Repeat every frame while the state is active.")]
		public FsmBool everyFrame;

		public override void Reset()
		{
			name = null;
			isCurrentScene = null;
			isTrue = null;
			isFalse = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{

			#if UNITY_5_3_OR_NEWER

			if (SceneManager.GetActiveScene().name == name.Value)
			{
				
				isCurrentScene.Value = true;
			}

			else 
			{
				isCurrentScene.Value = false;
			}

			Fsm.Event(isCurrentScene.Value ? isTrue : isFalse);

			if (!everyFrame.Value)
			{
				Finish();
			}

			#else

			Debug.LogWarning("<b>[SceneTest]</b><color=#FF9900ff> Need minimum unity5.3 !</color>", this.Owner);
			Finish ();
			#endif
		}

		public override void OnUpdate()
		{
			if (SceneManager.GetActiveScene().name == name.Value)
			{

				isCurrentScene.Value = true;
			}

			else 
			{
				isCurrentScene.Value = false;
			}

			Fsm.Event(isCurrentScene.Value ? isTrue : isFalse);

			if (!everyFrame.Value)
			{
				Finish();
			}
		}


	}
}
