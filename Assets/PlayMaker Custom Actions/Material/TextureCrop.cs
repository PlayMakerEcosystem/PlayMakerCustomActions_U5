// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Square crop Texture - NOTE: Read/Write has to be enabled if texture not created in-game")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12347.0")]
    public class TextureCrop : FsmStateAction
	{
		[ActionSection("Input")]
		public FsmTexture texture;

		[ActionSection("Options")]
		public FsmInt cropSize;
		public FsmInt offsetX;
		public FsmInt offsetY;

		[ActionSection("Output")]
		public FsmTexture newTexture;

		private Texture2D cropTex;
		private Texture2D inportTex;
		private RectTransform _rt;

		public override void Reset()
		{
			texture = null;
			newTexture = null;
			cropSize = 0;
			offsetX = 0;
			offsetY = 0;
		}

		public override void OnEnter()
		{
			Texture2D inportTex = texture.Value as Texture2D;
			cropTex = new Texture2D (cropSize.Value,cropSize.Value);
			Color[] data = inportTex.GetPixels(((inportTex.width - cropSize.Value) / 2)+offsetY.Value,((inportTex.height - cropSize.Value) / 2)+offsetX.Value,cropSize.Value, cropSize.Value);
			cropTex.SetPixels(0,0,cropSize.Value, cropSize.Value,data,0);
			cropTex.Apply();
			newTexture.Value = cropTex;

			inportTex = null;
			cropTex = null;

			Finish();
		}
	}
}
