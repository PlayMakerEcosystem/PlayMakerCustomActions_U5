// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Plays a series of sprites at a given framerate on a GameObject with an image sprite of a UGui Image component.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11937.0")]
	public class PlayUGuiImageAnimation : FsmStateAction
	{
		
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Image))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The framerate to animate the sprites")]
		public FsmFloat framesPerSecond;
		
		[Tooltip("-1 or 0 for infinite loop, animation done will never be called. More than 0 to define the number of times to play.")]
		public FsmInt loop;
		
		public FsmEvent animationDoneEvent;
		
		public Sprite[] _image;

		private UnityEngine.UI.Image imgRenderer;
		
		private int lastSpriteIndex;
		
		private int loopCounter;
		
		public override void Reset()
		{
			gameObject = null;
			loop = -1;
			animationDoneEvent = null;
			
			framesPerSecond = 12f;
			_image = new Sprite[3];
			
			loopCounter = 0;
		}
		
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			imgRenderer = go.GetComponent<UnityEngine.UI.Image>();
			
			if (imgRenderer == null)
			{
				LogError("PlayUGuiImageAnimation: Missing  UGui Image Renderer!");
				return;
			}
			
			lastSpriteIndex = 0;
			loopCounter = 0;
			
		}
		
		// Update is called once per frame
		public override void OnUpdate()
		{
			if (framesPerSecond.Value>0)
			{
				int index = (int)(Time.timeSinceLevelLoad * framesPerSecond.Value);
				int spriteIndex = index % _image.Length;
				
				
				imgRenderer.sprite = _image[ spriteIndex ];
				
				if (spriteIndex!=lastSpriteIndex && spriteIndex==0 )
				{
					loopCounter++;
					
					if (loop.Value>0 && loopCounter>=loop.Value)
					{
						Fsm.Event(animationDoneEvent);
						Finish ();
					}
				}
				
				lastSpriteIndex = spriteIndex;
				
			}
		}
		
	}
}
