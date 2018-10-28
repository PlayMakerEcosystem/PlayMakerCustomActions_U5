// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the GameObject and name of a higher level parent or lower level child specified by an index.")]
	public class GetMultilevelGameObject : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to start from.")]
		public FsmOwnerDefault startingFrom;

		[Tooltip("How many \"Directories\" to go up or down. For example '2' would be the grandparent of the GameObject this FSM is attached to, '3' the parent of that one, and so on. 0 returns the Owner. Anything below 0 goes in the other direction (-2 = first child of the first child if 'Recursive' is set). You can also set a very high number to get the root GameObject and a very low one to return the last child element.")]
		public FsmInt index;

		[UIHint(UIHint.Variable)]
		[Tooltip("The GameObject at the specified level.")]
		public FsmGameObject storeResult;

		[Tooltip("Returns the name of the found GameObject.")]
		public FsmString storeResultName;
		
		[Tooltip("When searching through the children, defines whether is should go through the first child recursively, or return the sibling of the first child depending on the level depth.")]
		public FsmBool recursive;

		[Tooltip("Follow the path it takes (throws Debug.Log's for every GameObject it passes). If there are several Log entries with the same GameObject, it means that the given index is higher than the GameObject has parents or lower than it has children.")]
		public FsmBool debug;

		private GameObject go;

		public override void Reset()
		{
			startingFrom = null;
			index = 1;
			storeResult = null;
			storeResultName = null;
			recursive = true;
			debug = false;
		}

		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(startingFrom);

			if (!go)
			{
				//If startingFrom wasn't set
				storeResult.Value = null;
				storeResultName = "None";
				if(debug.Value)
					Debug.Log("GetMultilevelGameObject - NullReferenceException: 'Start From' is null");
				Finish();
				return;
			}
			
			Transform trans = go.transform, prevTrans = null, initTrans = trans;
			
			//get owner
			if (index.Value == 0) DebugLevel(0, go.name);

			//get parent (ascending)
			if(index.Value > 0)
			{
				for(int i = 0; i < index.Value; i++)
				{
					if (trans.parent == null) break;
					trans = trans.parent;
					if (prevTrans == trans) break;
					prevTrans = trans;
					
					DebugLevel(i + 1, trans.name);
				}
			}

			//get child (descending)
			if(index.Value < 0)
			{
				for(int i = 0; i < System.Math.Abs(index.Value); i++)
				{
					if (recursive.Value)
					{
						if (trans.childCount > 0) trans = trans.GetChild(0);
					}
					else
					{
						if (initTrans.childCount > i) trans = initTrans.GetChild(i) ?? initTrans;
					}

					if (prevTrans == trans) break;
					
					prevTrans = trans;
					DebugLevel(-(i + 1), trans.name);
				}	
			}
			
			storeResult.Value = trans.gameObject;
			storeResultName.Value = storeResult.Value.name;
			Finish();
		}

		void DebugLevel(int index, string goName)
		{
			if (debug.Value)
				Debug.Log(go.name + " - Level " + index + ": " + goName);
		}
	}
}
