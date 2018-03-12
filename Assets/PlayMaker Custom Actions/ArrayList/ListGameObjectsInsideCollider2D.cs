//License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Author: Deek

//Based on dudebxl's action "ArrayListFindGameObjectsInsideCollider2D", searches for Collider2D's instead of Sprites
//http://hutonggames.com/playmakerforum/index.php?topic=11754.0

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Store all active GameObjects with a Collider2D component that are inside the specified 2D collider with a specific tag and/or layer. Will filter first by tag, then by layer. Tags and/or layers must be declared in the tag/layer manager before using them.")]
	public class ListGameObjectsInsideCollider2D : ArrayListActions
	{
		[ActionSection("Set up")]

		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject).")]
		public FsmString reference;

		[RequiredField]
		[Tooltip("The collider to check against other intersecting colliders.")]
		[ObjectType(typeof(Collider2D))]
		public FsmObject colliderTarget;


		[ActionSection("Filter")]

		[UIHint(UIHint.Tag)]
		[Tooltip("by tag")]
		public FsmString tag;

		[Title("Incl Layer Filter")]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Also filter by layer?")]
		public FsmBool layerFilterOn;

		[UIHint(UIHint.Layer)]
		public int layer;

		[ActionSection("Optionally")]

		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject)]
		[Tooltip("Store the found GameObjects in a GameObject-Array.")]
		public FsmArray storeArray;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the amount of found GameObjects.")]
		public FsmInt storeAmount;

		[Tooltip("Wheter to update on every frame.")]
		public FsmBool everyFrame;


		private List<GameObject> tempList = new List<GameObject>();

		public override void Reset()
		{
			gameObject = null;
			colliderTarget = null;
			reference = null;
			tag = "Untagged";
			layerFilterOn = false;
			layer = 0;
			storeArray = null;
			storeAmount = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			if(SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
				FindGOByTag();

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			if(SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
				FindGOByTag();
		}

		public void FindGOByTag()
		{
			if(!isProxyValid()) return;

			proxy.arrayList.Clear();

			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);

			if(objtag.Length == 0) return;

			tempList.Clear();

			Collider2D temp = colliderTarget.Value as Collider2D;
			Bounds colliderBounds = temp.bounds;

			if(layerFilterOn.Value == false)
			{
				for(int i = 0; i < objtag.Length; i++)
				{
					Collider2D tempRen = objtag[i].GetComponent<Collider2D>();
					Bounds myObj = tempRen.bounds;

					bool insideCollider = colliderBounds.Intersects(myObj);

					if(insideCollider == true)
					{
						if(layerFilterOn.Value == true
						&& objtag[i].gameObject.layer != layer) continue;

						tempList.Add(objtag[i]);
					}
				}
			}

			proxy.arrayList.InsertRange(0, tempList);
			storeArray.Values = tempList.ToArray();
			storeAmount.Value = tempList.Count;
		}
	}
}
