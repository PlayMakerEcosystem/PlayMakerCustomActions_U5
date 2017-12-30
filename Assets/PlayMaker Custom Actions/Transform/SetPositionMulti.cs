// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Sets the Position of multiple Game Object to one Vector3.")]
	public class SetPositionMulti : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to position.")]
		public FsmGameObject[] gameObject;

		[Tooltip("Use a stored Vector3 variable or specify each axis individually.")]
		public FsmVector3 vector;

		[Tooltip("Use local or world space.")]
		public Space space;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		[Tooltip("Perform in LateUpdate. This is useful if you want to override the position of objects that are animated or otherwise positioned in Update.")]
		public bool lateUpdate;

		private GameObject go;

		public override void Reset()
		{
			gameObject = new FsmGameObject[3];
			vector = Vector3.zero;
			space = Space.Self;
			everyFrame = false;
			lateUpdate = false;
		}

		public override void OnEnter()
		{
			if(!everyFrame && !lateUpdate)
			{
				DoSetPosition();
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if(!lateUpdate)
			{
				DoSetPosition();
			}
		}

		public override void OnPreprocess()
		{
#if PLAYMAKER_1_8_5_OR_NEWER
			//required since PlayMaker 1.8.5 if you want to use OnLateUpdate()
			Fsm.HandleLateUpdate = true;
#endif
		}

		public override void OnLateUpdate()
		{
			if(lateUpdate)
			{
				DoSetPosition();
			}

			if(!everyFrame)
			{
				Finish();
			}
		}

		void DoSetPosition()
		{
			for(int i = 0; i < gameObject.Length; i++)
			{
				go = gameObject[i].Value;

				if(go == null)
				{
					continue;
				}

				Vector3 position;

				if(vector.IsNone)
				{
					position = space == Space.World ? go.transform.position : go.transform.localPosition;
				} else
				{
					position = vector.Value;
				}

				if(space == Space.World)
				{
					go.transform.position = position;
				} else
				{
					go.transform.localPosition = position;
				}
			}
		}
	}
}