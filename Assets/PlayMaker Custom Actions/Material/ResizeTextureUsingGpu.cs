// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// require min. Unity5

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Resize the texture using GPU - WARNING: This script override the RTT Setup! (It sets a RTT!)")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12349.0")]
    public class ResizeTextureUsingGpu : FsmStateAction
	{
		[ActionSection("Input")]
		public FsmTexture texture;

		[ActionSection("Setup")]
		public FsmInt setWidth;
		public FsmInt setHeight;
	
		public enum Mode{
			Bilinear,
			Point,
			Trilinear,
		}
		
		public Mode filterMode;

		[ActionSection("Output")]
		public FsmTexture newTexture;
	
		private FilterMode modeSelect;

		public override void Reset()
		{
			newTexture = null;
			texture = null;
			filterMode = Mode.Trilinear;
			setWidth = 0;
			setHeight = 0;
		}

		public override void OnEnter()
		{

			if (texture.IsNone || texture.Value == null)
			{
				Debug.LogWarning("<b>[ResizeTextureUsingGpu]</b><color=#FF9900ff> Empty Input - Please review!</color>", this.Owner);
				Finish ();
			}

			if (setWidth.Value == 0 || setHeight.Value == 0)
			{
				Debug.LogWarning("<b>[ResizeTextureUsingGpu]</b><color=#FF9900ff> Empty setWidth or setHeight - Nothing happened - Please review!</color>", this.Owner);
				Finish ();
			}

			switch (filterMode)
			{
			case Mode.Bilinear:
				modeSelect = FilterMode.Bilinear;
				break;
			case Mode.Point:
				modeSelect = FilterMode.Point;
				break;
			case Mode.Trilinear:
				modeSelect = FilterMode.Trilinear;
				break;
			}

		
			Texture2D texGpuItem = texture.Value as Texture2D;
			newTexture.Value = scaled(texGpuItem,setWidth.Value,setHeight.Value,modeSelect);
			texGpuItem = null;

			Finish();
		}

		public static Texture2D scaled(Texture2D src, int width, int height, FilterMode mode)
		{
			Rect texR = new Rect(0,0,width,height);
			_gpu_scale(src,width,height,mode);

			Texture2D result = new Texture2D(width, height, TextureFormat.ARGB32, true);
			result.Resize(width, height);
			result.ReadPixels(texR,0,0,true);
			result.Apply(true);
			return result;                 
		}


		static void _gpu_scale(Texture2D src, int width, int height, FilterMode fmode)
		{
		
			src.filterMode = fmode;
			src.Apply(true);       

			RenderTexture rtt = new RenderTexture(width, height, 32);

			Graphics.SetRenderTarget(rtt);

			GL.LoadPixelMatrix(0,1,1,0);

			GL.Clear(true,true,new Color(0,0,0,0));
			Graphics.DrawTexture(new Rect(0,0,1,1),src);
		}
	}
}
