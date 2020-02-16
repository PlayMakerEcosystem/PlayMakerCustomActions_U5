// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Compress texture into DXT format. Use this to compress textures generated at runtime. Compressed textures use less graphics memory and are faster to render. After compression, texture will be in DXT1 format if the original texture had no alpha channel, and in DXT5 format if it had alpha channel.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12357.0")]
    public class DxtCompressTexture : FsmStateAction
	{
		[ActionSection("Input")]
		public FsmTexture texture;

		[ActionSection("Option")]
		public FsmBool highQuality;

		public override void Reset()
		{
		
			texture = null;
			highQuality = true;
		}

		public override void OnEnter()
		{
			Texture2D temp = texture.Value as Texture2D;
			temp.Compress(highQuality.Value);
			texture.Value = temp;
			temp = null;
			Finish();
		}
	}
}
