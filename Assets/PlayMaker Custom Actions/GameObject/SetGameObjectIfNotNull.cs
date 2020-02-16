// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets the value of a Game Object variable if it's not null.")]
	public class SetGameObjectIfNotNull : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject variable;

		[Tooltip("The GameObject to overwrite 'Variable' with if it's not null.")]
		public FsmGameObject gameObject;

		[Tooltip("Optional event to send if the GameObject is null.")]
		public FsmEvent isNull;

		[Tooltip("Optional event to send if the GameObject isn't null.")]
		public FsmEvent isNotNull;

		[Tooltip("Wheter to repeat this action on every frame or only once.")]
		public FsmBool everyFrame;

		public override void Reset()
		{
			variable = null;
			gameObject = null;
			isNull = null;
			isNotNull = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			SetGameObject();

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			SetGameObject();
		}

		private void SetGameObject()
		{
			if(gameObject.Value)
			{
				variable.Value = gameObject.Value;
				Fsm.Event(isNotNull);
			} else
			{
				Fsm.Event(isNull);
			}
		}
	}
}