// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12716.0")]
	[Tooltip("Drag uGui element in game")]
	public class uGuiDrag : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.RawImage))]
		[ActionSection("Setup")]
		[Tooltip("use touch")]
		public FsmBool isMobile;

		public FsmString tag;

		public enum updateType
		{
			Update,
			Late,
			Fixed,
		};

		public updateType updateTypeSelect;

		public FsmBool pause;
		public FsmBool forceQuit;

		private bool isUpdate;
		private bool isFUpdate;
		private bool isLUpdate;

		public override void Reset()
		{
			isMobile = null;
			tag = null;
			updateTypeSelect = updateType.Update;
			forceQuit = null;
			pause = null;
			isUpdate = false;
			isFUpdate = false;
			isLUpdate = false;
		}

		public override void OnPreprocess()
		{
			if (updateTypeSelect == updateType.Fixed)
			{
				Fsm.HandleFixedUpdate = true;
			}

			#if PLAYMAKER_1_8_5_OR_NEWER
			if (updateTypeSelect == updateType.Late)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}

		public override void OnEnter()
		{

			switch(updateTypeSelect){

			case updateType.Update:
				isUpdate = true;
				isFUpdate = false;
				isLUpdate = false;
				break;

			case updateType.Fixed:
				isUpdate = false;
				isFUpdate = true;
				isLUpdate = false;
				break;

			case updateType.Late:
				isUpdate = false;
				isFUpdate = false;
				isLUpdate = true;
				break;

			}
		}

		public override void OnUpdate(){

			if (isUpdate == true){
				Action();
			}
		}

		public override void OnLateUpdate(){

			if (isLUpdate == true){
				Action();
			}
		}

		public override void OnFixedUpdate(){
			if (isFUpdate == true){
				Action();
			}

		}

		void Action(){

			if(Input.GetMouseButton(0) && pause.Value == false && isMobile.Value == false)
			{
			PointerEventData pointer = new PointerEventData(EventSystem.current);
			pointer.position = Input.mousePosition;

			List<RaycastResult> raycastResults = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointer, raycastResults);

			if(raycastResults.Count > 0)
			{
					if(raycastResults[0].gameObject.tag == tag.Value)
					raycastResults[0].gameObject.transform.position = Input.mousePosition;
			}

			}

			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && pause.Value == false && isMobile.Value == true)
			{
				PointerEventData pointer = new PointerEventData(EventSystem.current);
				pointer.position = Input.mousePosition;

				List<RaycastResult> raycastResults = new List<RaycastResult>();
				EventSystem.current.RaycastAll(pointer, raycastResults);

				if(raycastResults.Count > 0)
				{
					if(raycastResults[0].gameObject.tag == tag.Value)
						raycastResults[0].gameObject.transform.position = new Vector3 (Input.GetTouch(0).position.x, Input.GetTouch(0).position.y,0);
				}

			}


			if (forceQuit.Value == true){
				Finish();
			}
		}
			
	}
}
