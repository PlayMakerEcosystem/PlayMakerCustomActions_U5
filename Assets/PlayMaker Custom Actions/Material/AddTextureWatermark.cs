// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Add watermark on image")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12473.0")]
	public class AddTextureWatermark : FsmStateAction
	{

		[ActionSection("Input")]
		public FsmTexture texture;
		public FsmTexture watermark;

		[ActionSection("Position")]
		public FsmInt xOffset;
		public FsmInt yOffset;

		[ActionSection("Output")]
		public FsmTexture newTexture;

		public override void Reset()
		{
		
			newTexture = null;
			texture = null;
			watermark = null;
			xOffset = 0;
			yOffset = 0;
		}

		public override void OnEnter()
		{
			newTexture.Value = AddWatermark(texture.Value as Texture2D, watermark.Value as Texture2D);
			Finish();
		}

		public Texture2D AddWatermark(Texture2D background, Texture2D watermark)
		{
			
			int startX = xOffset.Value;
			int startY = (background.height - watermark.height) + yOffset.Value;
			
			for (int x = startX; x < background.width; x++)
			{
				
				for (int y = startY; y < background.height; y++)
				{
					Color bgColor = background.GetPixel(x, y);
					Color wmColor = watermark.GetPixel(x - startX, y - startY);
					
					Color final_color = Color.Lerp(bgColor, wmColor, wmColor.a / 1.0f);
					
					background.SetPixel(x, y, final_color);
				}
			}
			
			background.Apply();
			return background;
		}

	}
}
