// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// original action http://hutonggames.com/playmakerforum/index.php?topic=10092.msg47749#msg47749

///v2.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets the visibility of GameObject's by (1) Tag then by (2) layer (if layer option is On). Note: This action sets the GameObject Renderer's enabled state or Sprite renderer enabled state.")]
	public class SetVisibilityByTag : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[UIHint(UIHint.Tag)]
		public FsmString tag;
		
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Should the object visibility be On or Off?")]
		public FsmBool visible;

		[ActionSection("Options")]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Should the object Collider be included?")]
		public FsmBool inclCollider;

		[UIHint(UIHint.FsmBool)]
		[Tooltip("Should the object Children be included?")]
		public FsmBool inclChildren;

		[ActionSection("Layer Options")]
		[TitleAttribute("incl Layer Filter")]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Also filter by layer?")]
		public FsmBool layerFilterOn;
		[Tooltip("Filter layer on child?")]
		public FsmBool inclLayerFilterOnChild;
		[UIHint(UIHint.Layer)]
		public int layer;


		GameObject[] gos;
		bool children;

		public override void Reset()
		{

			tag = null;
			visible = false;
			inclCollider = true;
			inclChildren = true;
			layerFilterOn = false;
			layer = 0;
			inclLayerFilterOnChild = false;
		}


		public override void OnEnter()
		{
			children = inclChildren.Value;

			if (layerFilterOn.Value == false){

			DoSetVisibility();

			}

			else if (layerFilterOn.Value == true){
				DoSetVisibilitywithLayer();
			}

			Finish();

		}

		void DoSetVisibility()
		{
			gos = GameObject.FindGameObjectsWithTag(tag.Value);

			if (gos.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				Finish();
			}

			for(int i = 0; i<gos.Length;i++){


				if (gos[i].GetComponent<Renderer>() != null){

					gos[i].GetComponent<Renderer>().enabled = visible.Value;

				}

				else {

					Debug.LogWarning ("Object is missing a renderer:  "+gos[i].name+"  ***  tag: "+tag.Value);
				}

					if (inclCollider.Value){

						if (gos[i].GetComponent<Collider>() != null)
						{   
							gos[i].GetComponent<Collider>().enabled = visible.Value;
						}

						if (gos[i].GetComponent<Collider2D>() != null)
						{   
							gos[i].GetComponent<Collider2D>().enabled = visible.Value;
						}

					}

				if (children)
				{
					foreach (Transform trans in gos[i].GetComponentsInChildren<Transform>(true))
					{
				
						if (trans.gameObject.GetComponent<Renderer>() != null){
						trans.gameObject.GetComponent<Renderer>().enabled = visible.Value;
						}

						if (inclCollider.Value){
							
							if (trans.gameObject.GetComponent<Collider>() != null)
							{   
								trans.gameObject.GetComponent<Collider>().enabled = visible.Value;
							}
							
							if (trans.gameObject.GetComponent<Collider2D>() != null)
							{   
								trans.gameObject.GetComponent<Collider2D>().enabled = visible.Value;
							}
							
						}
						
					}
					
				}
			}

			Finish();
		}

		void DoSetVisibilitywithLayer()
		{
			gos = GameObject.FindGameObjectsWithTag(tag.Value);
			
			
			for(int i = 0; i<gos.Length;i++){

				if (gos[i].layer == layer){

					if (gos[i].GetComponent<Renderer>() != null){
						
						gos[i].GetComponent<Renderer>().enabled = visible.Value;
						
					}
					
					else {
						
						Debug.LogWarning ("Object is missing a renderer:  "+gos[i].name+"  ***  tag: "+tag.Value+" layer: "+LayerMask.LayerToName(layer));
					}
				
				if (inclCollider.Value){
					
					if (gos[i].GetComponent<Collider>() != null)
					{   
						gos[i].GetComponent<Collider>().enabled = visible.Value;
					}
					
					if (gos[i].GetComponent<Collider2D>() != null)
					{   
						gos[i].GetComponent<Collider2D>().enabled = visible.Value;
					}
					
				}
				
				if (children)
				{
					foreach (Transform trans in gos[i].GetComponentsInChildren<Transform>(true))
					{
						
							if (inclLayerFilterOnChild.Value == true){

						if (trans.gameObject.layer == layer){

						if (trans.gameObject.GetComponent<Renderer>() != null){
							trans.gameObject.GetComponent<Renderer>().enabled = visible.Value;
						}
						
						if (inclCollider.Value){
							
							if (trans.gameObject.GetComponent<Collider>() != null)
							{   
								trans.gameObject.GetComponent<Collider>().enabled = visible.Value;
							}
							
							if (trans.gameObject.GetComponent<Collider2D>() != null)
							{   
								trans.gameObject.GetComponent<Collider2D>().enabled = visible.Value;
							}
							
						}
						}
						}

							else if (inclLayerFilterOnChild.Value == false){

								if (trans.gameObject.GetComponent<Renderer>() != null){
									trans.gameObject.GetComponent<Renderer>().enabled = visible.Value;
								}
								
								if (inclCollider.Value){
									
									if (trans.gameObject.GetComponent<Collider>() != null)
									{   
										trans.gameObject.GetComponent<Collider>().enabled = visible.Value;
									}
									
									if (trans.gameObject.GetComponent<Collider2D>() != null)
									{   
										trans.gameObject.GetComponent<Collider2D>().enabled = visible.Value;
									}
								}
							}
					}
					
				}
			}
			}

			Finish();
		}



	}
}