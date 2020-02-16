// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//v1.0

using UnityEngine;
using System.Collections; 
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
		
		[ActionCategory(ActionCategory.GameObject)]
		[Tooltip("Rewind a Gameobject. Default elements: Position + Rotation + Scale + Velocity. You can add more variables or component by code to the action (marked in code).")]
		[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11831.0")]

		public class GameObjectTimeRewind : FsmStateAction
		{	

		[ActionSection("Setup")]
		[Tooltip("The GameObject that owns the Behaviour.")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Rewind seconds")]
		[Tooltip("The rewind time in seconds")]
		public FsmInt timeLimitInSeconds;

		[ActionSection("General Option")]
		[Tooltip("Is gameobject render 2d?")]
		public FsmBool is2D;
		[Tooltip("Use SmoothDeltaTime?")]
		public FsmBool useSmoothDeltaTime;
		[Tooltip("Set to true if you use gravity")]
		public FsmBool rewindIsKinematic;


		[ActionSection("Update Setup")]
		[Tooltip("Which update to use")]
		public updateType updateTypeSelect;
		public enum updateType
		{
			Update,
			FixedUpdate,
			LateUpdate
		};

		[ActionSection("Time Option")]
		public FsmBool useStopMotion;
		[HasFloatSlider(0.1f, 2f)]
		public FsmFloat stopMotion; 

		[ActionSection("Buffer Option")]
		[Tooltip("Limit the size of list")]
		public FsmInt listLimit;

		[ActionSection("Control")]
		public FsmBool rewind;
		public FsmBool forceQuit;

		[ActionSection("Output")]
		public FsmBool isRewinding;
		public FsmEvent forceQuitEvent;

		private List <Vector3> positionVal = new List <Vector3>(); 
		private List <Vector3> rotationVal = new List <Vector3>();
		private List <Vector3> sizeVal = new List <Vector3>();
		private List <Vector3> velocityVal = new List <Vector3>();

// --> add new List component 


// <-- end

		private GameObject go;
		private int indexVal; 
		private float counter;


		private Rigidbody rb; 
		private Rigidbody2D rb2d;

		private float newTimeScale; 


		public override void Reset()
		{
			gameObject = null;
			is2D = false;
			useSmoothDeltaTime = false;
			positionVal = new List <Vector3>(); 
			rotationVal = new List <Vector3>(); 
			sizeVal = new List <Vector3>(); 
			velocityVal = new List <Vector3>(); 
			updateTypeSelect = updateType.FixedUpdate;

// --> add new List component 


			
// <-- end

			listLimit = 500;
			timeLimitInSeconds = 10;
			isRewinding = false;
			rewind = false;
			forceQuit = false;
			forceQuitEvent= null;
			rewindIsKinematic = true;
			stopMotion =0.1f;
			useStopMotion =false;
		
		}

	
		public override void OnPreprocess()
		{
			if (updateTypeSelect == updateType.FixedUpdate )
			{
				Fsm.HandleFixedUpdate = true;
			}

			#if PLAYMAKER_1_8_5_OR_NEWER
			if (updateTypeSelect == updateType.LateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}

		public override void OnEnter()
		{
			newTimeScale += Time.deltaTime;

			go = Fsm.GetOwnerDefaultTarget(gameObject);

			if (is2D.Value == true)
			{

				rb2d = go.GetComponent<Rigidbody2D>();

			}

			else
			{

				rb = go.GetComponent<Rigidbody>();

			}

		}

		public override void OnUpdate(){


			if (updateTypeSelect == updateType.Update )
			{
				
				doAction();
			}

		}

		public override void OnLateUpdate(){

			if (updateTypeSelect == updateType.LateUpdate )
			{

				doAction();
			}
			
			
		}

		public override void OnFixedUpdate(){
			
			if (updateTypeSelect == updateType.FixedUpdate )
			{

				doAction();
			}
			
		}


		void doAction()
		{
	

			if(counter < 0) 
			{ 
				counter = 0;
				isRewinding.Value = false;
				rewind.Value = false;
			}
	
			if (rewind.Value == true){

				if (newTimeScale >= (stopMotion.Value*0.1f) || useStopMotion.Value == false) {

				if(counter > 0) 
				{ 



					if (useSmoothDeltaTime.Value == false){

						counter-=Time.deltaTime/stopMotion.Value; 
					} 

					else {

						counter-=Time.smoothDeltaTime; 
					
					}

					newTimeScale = 0f;

					isRewinding.Value = true;


					Rewind();

				}
				
				}
				newTimeScale += Time.deltaTime;  
				
			}

				else { 

					if(counter<timeLimitInSeconds.Value) 
					{ 

					if (useSmoothDeltaTime.Value == false){
						
						counter+=Time.deltaTime/stopMotion.Value; 
					} 
					
					else {
						
						counter+=Time.smoothDeltaTime; 
						
					}

					
					} 

					isRewinding.Value = false;

				if (is2D.Value == true & rewindIsKinematic.Value == true)
				{
					rb2d.isKinematic = false;
				}
				
				else if (rewindIsKinematic.Value == true){
					
					rb.isKinematic = false;
					rb.useGravity = true;
				}

				}

				if(!isRewinding.Value) {

					positionVal.Add(go.transform.position); 
					rotationVal.Add(go.transform.eulerAngles); 
					sizeVal.Add (go.transform.localScale);

					if (is2D.Value == true)
					{
						velocityVal.Add(rb2d.velocity);
					}

					else
					{
						velocityVal.Add(rb.velocity);
					}
				

// --> add new component 
				
				
// <-- end

				if(indexVal<listLimit.Value) 
					{ 
						indexVal++; 
					}
				
				
				}

				if(indexVal>listLimit.Value && !isRewinding.Value) 
				{ 
					positionVal.RemoveAt(0); 
					rotationVal.RemoveAt(0); 
					sizeVal.RemoveAt(0); 
					velocityVal.RemoveAt(0); 
				
// --> add new component 
					
					
// <-- end	

				}
		
		

			if (forceQuit.Value == true)
			{
				isRewinding.Value = false;
				positionVal = new List <Vector3>(); 
				rotationVal = new List <Vector3>(); 
				sizeVal = new List <Vector3>(); 
				velocityVal = new List <Vector3>(); 

// --> add new component 
				
				
// <-- end	

				Fsm.Event(forceQuitEvent);
				Finish();
				
			} 

		}



		void Rewind()
		{
			if(indexVal>0) 
			{ 

			indexVal--; 

			go.transform.position = positionVal[indexVal]; 
			positionVal.RemoveAt(indexVal);

			go.transform.eulerAngles = rotationVal[indexVal]; 
			rotationVal.RemoveAt(indexVal); 

			go.transform.localScale = sizeVal[indexVal];
			sizeVal.RemoveAt(indexVal); 

			if (is2D.Value == true)
				{
					if (rewindIsKinematic.Value == true){
					rb2d.isKinematic = true;
					}

					rb2d.velocity = velocityVal[indexVal]; 
				}

				else {
					if (rewindIsKinematic.Value == true){
					rb.isKinematic = true;
					rb.useGravity = false;
					}

					rb.velocity = velocityVal[indexVal]; 
				}

			velocityVal.RemoveAt(indexVal); 

// --> add new component 
			
			
// <-- end	
			}

			if(indexVal==0) 
			{ 
				isRewinding.Value = false; 
				rewind.Value = false;

				if (is2D.Value == true & rewindIsKinematic.Value == true)
				{
					rb2d.isKinematic = false;
				}
				
				else if (rewindIsKinematic.Value == true){
					
					rb.isKinematic = false;
					rb.useGravity = true;
				}

			}
		}
	
	


	}



}

