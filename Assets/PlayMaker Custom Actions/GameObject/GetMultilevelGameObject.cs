// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the Game Object and name of a higher parent or lower child specified by an index.")]
	public class GetMultilevelGameObject : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to start from.")]
		public FsmOwnerDefault startingFrom;

		[Tooltip("How many 'Directories' to go up or down. For example '2' would be the grandparent of the GameObject this FSM is attached to, '3' the parent of that one ... and so on. 0 returns the Owner. Anything below 0 goes in the other direction (-2 = first Child of the first Child). You can also set a very high number to definitely get the root.")]
		public FsmInt index;

		[UIHint(UIHint.Variable)]
		[Tooltip("The final GameObject it reached.")]
		public FsmGameObject storeResult;

		[Tooltip("The Name of the final GameObject it reached.")]
		public FsmString storeResultName;

		[Tooltip("Follow the path it takes (throws Debug.Log's for every GameObject it passes). If there are several Log entries with the same GameObject, it means that the given index is higher than the GameObject has parents or lower than it has children.")]
		public FsmBool debug;

		public override void Reset()
		{
			startingFrom = null;
			index = 1;
			storeResult = null;
			storeResultName = null;
			debug = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(startingFrom);

			//if startingFrom is null
			if (go == null)
			{
				storeResult.Value = null;
				storeResultName.Value = "None";
				
				if(debug.Value)
					Debug.Log("GetMultilevelGameObject: 'Starting From' is null");
			}
			
			//get owner
			if(index.Value == 0)
			{
				storeResult.Value = Owner;
				storeResultName.Value = storeResult.Value.name;
					
				if(debug.Value)
					Debug.Log("GetMultilevelGameObject - Owner: " + Owner);
			}

			//Get ascending parent
			if(index.Value > 0)
			{
				for(int i = 1; i < index.Value; ++i)
				{
					if (go.transform.parent != null) go = go.transform.parent.gameObject;
					
					if(debug.Value)
					{
						Debug.Log("GetMultilevelGameObject - Parent " + (i + 1) + ": " + go.transform.gameObject.name);
					}

					storeResult.Value = go;
					storeResultName.Value = storeResult.Value.name;
				}
			}

			//Get descending Child
			if(index.Value < 0)
			{
				for(int i = -1; i > index.Value; i--)
				{
					go = go.transform.GetChild(0) == null ? go.transform.gameObject : go.transform.GetChild(0).gameObject;

					if(debug.Value)
					{
						//Invert and add 1 to current Index
						var j = i * -1 + 1;
						Debug.Log("GetMultilevelGameObject - Child " + j + ": " + go.transform.gameObject.name);
					}

					storeResult.Value = go;
					storeResultName.Value = storeResult.Value.name;
				}
			}
			
			Finish();
		}
	}
}
