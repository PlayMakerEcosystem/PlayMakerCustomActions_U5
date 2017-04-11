// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10096

using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends an Event to GameObject filtered by (1) Tag then by (2) layer (if layer option selected). NOTE: To send events between FSMs they must be marked as Global in the Events Browser.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10096")]
    public class SendEventByTag : FsmStateAction
	{

		[ActionSection("Filter by")]
		[RequiredField]
		[UIHint(UIHint.Tag)]
		public FsmString tag;
		[UIHint(UIHint.Layer)]
		public int layer;
		[TitleAttribute("incl Layer Filter")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool layerFilterOn;

		[ActionSection("Event Setup")]
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs. Or send by name")]
		public FsmEvent sendEvent;
		[TitleAttribute("or send by Event Name")]
		public FsmString sendEventName;

		[ActionSection("FSM Name")]
		[UIHint(UIHint.FsmString)]
		public string fsmName;

		[ActionSection("")]
		[Tooltip("Repeat every frame. Rarely needed.")]
		public bool everyFrame;

		GameObject[] gos;
	

		public override void Reset()
		{

			sendEvent = null;
			layerFilterOn = false;
			everyFrame = false;
			layer = 0;
			sendEventName = null;
			fsmName = null;
			tag = null;
			gos = null;
		
		}

		public override void OnEnter()
		{
		
			gos = null;
			gos = GameObject.FindGameObjectsWithTag(tag.Value);


			SendEventag();

			if (!everyFrame)
			{

				Finish();
			}


		}

		public override void OnUpdate()
		{
			SendEventag();

			if (!everyFrame)
			{
				
				Finish();
			}
		}

		public void SendEventag()
		{


			for(int i = 0; i<gos.Length;i++){

				if (layerFilterOn.Value == true) {

					if (gos[i].layer == layer)
					
					{

					if (sendEvent == null)	{
						
						Fsm.SendEventToFsmOnGameObject(gos[i],fsmName,FsmEvent.GetFsmEvent(sendEventName.Value));
					}
				
						
						Fsm.SendEventToFsmOnGameObject(gos[i],fsmName,sendEvent);


				}
				}

				if (layerFilterOn.Value == false){

						if (sendEvent == null)	{
							
							Fsm.SendEventToFsmOnGameObject(gos[i],fsmName,FsmEvent.GetFsmEvent(sendEventName.Value));
						}

							
							Fsm.SendEventToFsmOnGameObject(gos[i],fsmName,sendEvent);


					}

				}

		

			}
		}
	

}

