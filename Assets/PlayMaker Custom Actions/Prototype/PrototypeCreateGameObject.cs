// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

using Com.InkleStudios;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Prototype")]
    [ActionTarget(typeof(GameObject), "gameObject", true)]
	[Tooltip("Creates a Game Object out of a Prototype")]
	public class PrototypeCreateGameObject : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Prototype))]
		[Tooltip("GameObject to create. Must be a Prototype")]
		public FsmGameObject gameObject;

		[Tooltip("Optional Spawn Point.")]
		public FsmGameObject spawnPoint;
		
		[Tooltip("Position. If a Spawn Point is defined, this is used as a local offset from the Spawn Point position.")]
		public FsmVector3 position;
		
		[Tooltip("Rotation. NOTE: Overrides the rotation of the Spawn Point.")]
		public FsmVector3 rotation;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally store the created object.")]
		public FsmGameObject storeObject;


		GameObject _go;
		Prototype _proto;
		Transform _t;

		public override void Reset()
		{
			gameObject = null;
			spawnPoint = null;
			position = new FsmVector3 { UseVariable = true };
			rotation = new FsmVector3 { UseVariable = true };
			storeObject = null;		
		}

		public override void OnEnter()
		{
			_go = gameObject.Value;

			if (_go != null) {
				_proto = _go.GetComponent<Prototype> ();
			}

			if (_proto != null)
			{
				var spawnPosition = Vector3.zero;
				var spawnRotation = Vector3.zero;
				
				if (spawnPoint.Value != null)
				{
					spawnPosition = spawnPoint.Value.transform.position;
					
					if (!position.IsNone)
					{
						spawnPosition += position.Value;
					}
					
					spawnRotation = !rotation.IsNone ? rotation.Value : spawnPoint.Value.transform.eulerAngles;
				}
				else
				{
					if (!position.IsNone)
					{
						spawnPosition = position.Value;
					}
					
					if (!rotation.IsNone)
					{
						spawnRotation = rotation.Value;
					}
                }

				_t = _proto.Instantiate<Transform>();
				_t.position = spawnPosition;
				_t.rotation = Quaternion.Euler (spawnRotation);

				storeObject.Value = _t.gameObject;


			}
			
			Finish();
		}

	}
}