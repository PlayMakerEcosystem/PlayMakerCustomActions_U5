//License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Author: Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Returns the length of the specified Array List Proxy Component.")]
	public class ArrayListGetLength : ArrayListActions
	{
		[ActionSection("Set up")]

		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;

		[ActionSection("Result")]

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt length;

		[Tooltip("Subtract 1 from the length to get the index of the last element. Otherwise you get the actual length of the ArrayList wich can lead to IndexOutOfRange Exceptions.")]
		public bool subtract1;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			length = null;
			subtract1 = true;
		}

		public override void OnEnter()
		{

			if(!SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				Debug.LogWarning("Couldn't find the Array List Proxy Component!");
				Finish();
			}

			length.Value = proxy.arrayList.Count;

			if(subtract1) length.Value -= 1;

			Finish();
		}
	}
}
