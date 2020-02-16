// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Resize the texture. Example: ratio of 0,5 would mean current texture devided by half")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12349.0")]
    public class ResizeTexture : FsmStateAction
	{
		[ActionSection("Input")]
		public FsmTexture texture;

		[ActionSection("Setup")]
		[Tooltip("Leave to 0 to change only the FilterMode")]
		[HasFloatSlider(0f, 1.5f)]
		public FsmFloat ratio;

		[ActionSection("Output")]
		public FsmTexture newTexture;

		public enum FilterMode{
			Nearest,
			Biliner,
			Average,
		}
		
		public FilterMode filterMode;

		private int filterModeFinal;
		private Texture2D texItem;
		private TextureFormat modeSelect;
		private bool simpleMode;

		public override void Reset()
		{
			newTexture = null;
			texture = null;
			ratio = 0;
			filterModeFinal = 1;
		}

		public override void OnEnter()
		{

			if (texture.IsNone || texture.Value == null)
			{
				Debug.LogWarning("<b>[ResizeTexture]</b><color=#FF9900ff> Empty Input - Please review!</color>", this.Owner);
				Finish ();
			}

	
			switch (filterMode)
			{
			case FilterMode.Average:
				filterModeFinal = 0;
				break;
			case FilterMode.Biliner:
				filterModeFinal = 1;
				break;
			case FilterMode.Nearest:
				filterModeFinal = 2;
				break;
			}


			texItem = new Texture2D(Mathf.RoundToInt((float)texture.Value.width * ratio.Value),Mathf.RoundToInt((float)texture.Value.height * ratio.Value));
			texItem = ResizeTextureAction(texture.Value as Texture2D, filterModeFinal, ratio.Value);
			newTexture.Value = texItem;
			texItem = null;

			Finish();
		}

		public Texture2D ResizeTextureAction(Texture2D pSource, int pFilterMode, float pScale){

			int i;

			Color[] aSourceColor = pSource.GetPixels(0);
			Vector2 vSourceSize = new Vector2(pSource.width, pSource.height);

			float xWidth = Mathf.RoundToInt((float)pSource.width * pScale);                     
			float xHeight = Mathf.RoundToInt((float)pSource.height * pScale);

			Texture2D oNewTex = new Texture2D((int)xWidth, (int)xHeight, TextureFormat.RGBA32, false);

			int xLength = (int)xWidth * (int)xHeight;
			Color[] aColor = new Color[xLength];
			
			Vector2 vPixelSize = new Vector2(vSourceSize.x / xWidth, vSourceSize.y / xHeight);

			Vector2 vCenter = new Vector2();
			for(i=0; i<xLength; i++){

				float xX = (float)i % xWidth;
				float xY = Mathf.Floor((float)i / xWidth);
				vCenter.x = (xX / xWidth) * vSourceSize.x;
				vCenter.y = (xY / xHeight) * vSourceSize.y;

				if(pFilterMode == 0){
					

					vCenter.x = Mathf.Round(vCenter.x);
					vCenter.y = Mathf.Round(vCenter.y);

					int xSourceIndex = (int)((vCenter.y * vSourceSize.x) + vCenter.x);

					aColor[i] = aSourceColor[xSourceIndex];
				}
				

				else if(pFilterMode == 1){

					float xRatioX = vCenter.x - Mathf.Floor(vCenter.x);
					float xRatioY = vCenter.y - Mathf.Floor(vCenter.y);

					int xIndexTL = (int)((Mathf.Floor(vCenter.y) * vSourceSize.x) + Mathf.Floor(vCenter.x));
					int xIndexTR = (int)((Mathf.Floor(vCenter.y) * vSourceSize.x) + Mathf.Ceil(vCenter.x));
					int xIndexBL = (int)((Mathf.Ceil(vCenter.y) * vSourceSize.x) + Mathf.Floor(vCenter.x));
					int xIndexBR = (int)((Mathf.Ceil(vCenter.y) * vSourceSize.x) + Mathf.Ceil(vCenter.x));

					aColor[i] = Color.Lerp(
						Color.Lerp(aSourceColor[xIndexTL], aSourceColor[xIndexTR], xRatioX),
						Color.Lerp(aSourceColor[xIndexBL], aSourceColor[xIndexBR], xRatioX),
						xRatioY
						);
				}

				else if(pFilterMode == 2){

					int xXFrom = (int)Mathf.Max(Mathf.Floor(vCenter.x - (vPixelSize.x * 0.5f)), 0);
					int xXTo = (int)Mathf.Min(Mathf.Ceil(vCenter.x + (vPixelSize.x * 0.5f)), vSourceSize.x);
					int xYFrom = (int)Mathf.Max(Mathf.Floor(vCenter.y - (vPixelSize.y * 0.5f)), 0);
					int xYTo = (int)Mathf.Min(Mathf.Ceil(vCenter.y + (vPixelSize.y * 0.5f)), vSourceSize.y);


					Color oColorTemp = new Color();
					float xGridCount = 0;
					for(int iy = xYFrom; iy < xYTo; iy++){
						for(int ix = xXFrom; ix < xXTo; ix++){

							oColorTemp += aSourceColor[(int)(((float)iy * vSourceSize.x) + ix)];

							xGridCount++;
						}
					}

					aColor[i] = oColorTemp / (float)xGridCount;
				}
			}

			oNewTex.SetPixels(aColor);
			oNewTex.Apply();

			return oNewTex;
		}

	}
}
