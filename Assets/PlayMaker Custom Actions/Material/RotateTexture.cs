// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Rotate the Fsm texture")]
     [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12476.0")]
	public class RotateTexture : FsmStateAction
	{
		[ActionSection("Input")]
		public FsmTexture textureInput;
	
		[ActionSection("Options")]
		public AngleType angle;
		public enum AngleType{
			Clockwise90,
			Counterclockwise90,
			Rotate180,
		}


		[ActionSection("Output")]
		public FsmTexture textureOutput;



		public override void Reset()
		{
			textureInput = null;
			textureOutput = null;
	
			angle = AngleType.Clockwise90;
		}

		public override void OnEnter()
		{
	
			Texture2D texTemp = textureInput.Value as Texture2D;

			switch (angle)
			{
			case AngleType.Clockwise90:
				textureOutput.Value = RotateClockwise90(texTemp);
				break;
			case AngleType.Counterclockwise90:
				textureOutput.Value = RotateCounterclockwise90(texTemp);
				break;
			case AngleType.Rotate180:
				textureOutput.Value = Rotate180(texTemp);
				break;
			
			}

			texTemp = null;

			Finish();
		}

		public static Texture2D RotateClockwise90 (Texture2D texture2D) {
			int height = texture2D.height;
			int width = texture2D.width;
			Texture2D rotatedTexture2D = new Texture2D(height, width, texture2D.format, false);
			
			for (int j = 0; j < height; j++) {
				for (int i = 0; i < width; i++) {
					rotatedTexture2D.SetPixel(height - j - 1, width - i - 1, texture2D.GetPixel(i,j));
				}
			}
			
			rotatedTexture2D.Apply();
			return rotatedTexture2D;
		}


		public static Texture2D RotateCounterclockwise90 (Texture2D texture2D) {
			int height = texture2D.height;
			int width = texture2D.width;
			Texture2D rotatedTexture2D = new Texture2D(height, width, texture2D.format, false);
			
			for (int j = 0; j < height; j++) {
				for (int i = 0; i < width; i++) {
					rotatedTexture2D.SetPixel(j, i, texture2D.GetPixel(i,j));
				}
			}
			
			rotatedTexture2D.Apply();
			return rotatedTexture2D;
		}

		public static Texture2D Rotate180 (Texture2D texture2D) {
			int height = texture2D.height;
			int width = texture2D.width;
			Texture2D rotatedTexture2D = new Texture2D(width, height, texture2D.format, false);
			
			for (int j = 0; j < height; j++) {
				for (int i = 0; i < width; i++) {
					rotatedTexture2D.SetPixel(width - i - 1, height - j - 1, texture2D.GetPixel(i,j));
				}
			}
			
			rotatedTexture2D.Apply();
			return rotatedTexture2D;
		}


		
	}
}
