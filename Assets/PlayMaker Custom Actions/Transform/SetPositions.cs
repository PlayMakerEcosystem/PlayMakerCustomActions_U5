// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs"
					  ]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Sets the Position of multiple Game Object to one Vector3 or each one individually.")]
	public class SetPositions : FsmStateActionAdvanced
	{
		[CompoundArray("Amount", "Game Object", "Position")]
		
		[Tooltip("The GameObject to position.")]
		public FsmGameObject[] gameObjects;

		[Tooltip("The individual position to apply to the current GameObject if 'Apply To All' isn't 'None'.")]
		public FsmVector3[] positions;


		[Tooltip("Use a stored Vector3 variable or specify each axis individually. Gets ignores if 'None'.")]
		public FsmVector3 applyToAll;

		[Tooltip("Use local or world space.")]
		public Space space;

		[Tooltip("Ignores the Z-Axis on each Vector3 to effectively use them as Vector2's. Useful if you only want to change the position in a 2D manner.")]
		public FsmBool ignoreZAxis;

		private GameObject go = null;
		private Vector3 pos = new Vector3();
		private int prevAmount = 0;

		public override void Reset()
		{
			base.Reset();

			gameObjects = new FsmGameObject[0];
			positions = new FsmVector3[0];
			applyToAll = new FsmVector3() { UseVariable = true };
			space = Space.Self;
			ignoreZAxis = false;
		}

		public override void OnEnter()
		{
			Default();
			DoSetPosition();

			if(!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			Default();
			DoSetPosition();
		}

		void DoSetPosition()
		{
			for(int i = 0; i < gameObjects.Length; i++)
			{
				go = gameObjects[i].Value;

				if(gameObjects[i].IsNone)
				{
					continue;
				}

				//due to safety-precautions, this can only occur when a GameObject gets destroyed at runtime
				if(go == null)
				{
					UnityEngine.Debug.LogError("Action \"Set Position Multi\" in " + Fsm.Name + " on "
											   + Owner.name + " contains an empty GameObject on position #" + i);
					return;
				}

				if(applyToAll.IsNone)
				{
					if(positions[i].IsNone)
					{
						//if neither position was set, ignore the current GameObject
						continue;
					} else
					{
						//use individual position
						pos = positions[i].Value;
					}
				} else
				{
					//use global position
					pos = applyToAll.Value;
				}

				//use the GameObject's own z value for the new position if Ignore Z Axis is set to true
				if(ignoreZAxis.Value)
				{
					pos.z = space == Space.World ? go.transform.position.z : go.transform.localPosition.z;
				}

				//apply position to current GameObject
				if(space == Space.World)
				{
					go.transform.position = pos;
				} else
				{
					go.transform.localPosition = pos;
				}
			}
		}

		//explicitly declare using OnGUI
		public override void OnPreprocess()
		{
			Fsm.HandleOnGUI = true;
		}

		public override void OnGUI()
		{
			Default();
		}

		void Default()
		{
			//if the array-size changes, set each empty GameObject to 'None'
			if(prevAmount != gameObjects.Length)
			{
				for(int i = 0; i < gameObjects.Length; i++)
				{
					if(gameObjects[i].Value == null)
					{
						gameObjects[i].UseVariable = true;
						if(positions[i].Value == Vector3.zero)
						{
							positions[i].UseVariable = true;
						}
					}
				}
				prevAmount = gameObjects.Length;
			}
		}
	}
}