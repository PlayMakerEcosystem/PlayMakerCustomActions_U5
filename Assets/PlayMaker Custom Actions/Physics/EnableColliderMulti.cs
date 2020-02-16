// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Enable or disable a Collider or Collider2D on several GamObject. Optionally set all colliders found on each GameObject.")]
	public class EnableColliderMulti : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObjects with the Colliders attached")]
		public FsmGameObject[] gameObjects;

		[RequiredField]
		[Tooltip("The flag value")]
		public FsmBool enable;

		[Tooltip("Set all Colliders on the GameObject target.")]
		public FsmBool applyToAllColliders;

		public override void Reset()
		{
			gameObjects = new FsmGameObject[3];
			enable = true;
			applyToAllColliders = false;
		}

		public override void OnEnter()
		{
			DoEnableCollider();

			Finish();
		}

		void DoEnableCollider()
		{
			foreach(var go in gameObjects)
			{
				if(go.Value == null) return;

				if(applyToAllColliders.Value)
				{
					// Find all of the colliders on the gameobject and set them all to be enabled.
					Collider[] cols = go.Value.GetComponents<Collider>();
					foreach(Collider c in cols)
					{
						c.enabled = enable.Value;
					}

					// Find all of the 2D colliders on the gameobject and set them all to be enabled.
					Collider2D[] cols2D = go.Value.GetComponents<Collider2D>();
					foreach(Collider2D c in cols2D)
					{
						c.enabled = enable.Value;
					}
				} else
				{
					if(go.Value.GetComponent<Collider>() != null) go.Value.GetComponent<Collider>().enabled = enable.Value;
					if(go.Value.GetComponent<Collider2D>() != null) go.Value.GetComponent<Collider2D>().enabled = enable.Value;
				}
			}
		}
	}
}
