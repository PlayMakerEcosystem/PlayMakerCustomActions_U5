// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using System;
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Save Texture as png or jpg.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12361.0")]
	public class SaveTextureAsImage : FsmStateAction
	{
		[ActionSection("Input")]
		[RequiredField]
		public FsmTexture texture;
		
		[ActionSection("File Path Setup")]
		public FsmString filePath;
		[Tooltip("Use the default MyPictures Folder?")]
		public FsmBool useMypictures;
		[Tooltip("Use the default save Folder?")]
		public FsmBool useDefaultFolder;
		[ActionSection("File Name Setup")]
		public FsmString filename;
		
		[ActionSection("Option")]
		public FsmBool useJpg;
		[Tooltip("Jpg Quality - 0 to 100")]
		public FsmInt jpgQuality;
		
		public FsmBool debugOn;
		
		private int screenshotCount;
		private string saveTexPath;
		private string screenshotFullPath;
		private Texture2D temp;
		
		public override void Reset()
		{
			texture = null;
			filename = "";
			useMypictures = false;
			useDefaultFolder = true;
			useJpg = false;
			debugOn = false;
			jpgQuality = 100;
		}
		
		public override void OnEnter()
		{
			if (texture.Value == null){
				Debug.LogWarning("<b>[SaveTextureAsImage]</b><color=#FF9900ff> Input texture is empty - Please review!</color>", this.Owner);
				Finish ();
			}

			if (useJpg.Value == true && jpgQuality.Value > 100 || jpgQuality.Value < 0){
				Debug.Log("<b>[SaveTextureAsImage]</b><color=#FF9900ff> Jpg quality should be between 0 - 100 - set to default (100) - Please review!</color>", this.Owner);
				jpgQuality.Value = 100;
			}

			savePicture();
			Finish();
		}
		
		public void savePicture()
		{
			temp = texture.Value as Texture2D;

			if (useMypictures.Value == true && useDefaultFolder.Value  == true){
				Debug.Log("<b>[SaveTextureAsImage]</b><color=#FF9900ff> useMypictures & useDefaultFolder cannot both be true - Please review!</color>", this.Owner);
			}

			if (useMypictures.Value == true)saveTexPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)+"/";
			else if (!useMypictures.Value && !useDefaultFolder.Value) saveTexPath = filePath.Value;
			else if (!useMypictures.Value && useDefaultFolder.Value == true) saveTexPath = Application.persistentDataPath;
			
			if (!useJpg.Value) screenshotFullPath = saveTexPath + filename.Value + ".png";
			else screenshotFullPath = saveTexPath + filename.Value + ".jpg";


			byte[] bytes;

			if (!useJpg.Value){
				if(temp.format == TextureFormat.ARGB32 || temp.format == TextureFormat.RGB24) {
					bytes = temp.EncodeToPNG();
				}

				else {

					Debug.LogWarning("<b>[SaveTextureAsImage]</b><color=#FF9900ff> Texture is not ARGB32 or RGB24 - Cannot save to PNG - Please review!</color>", this.Owner);
					return;
				}
			}
			else
			{ 
				bytes = temp.EncodeToJPG(jpgQuality.Value);
			}
			
			System.IO.File.WriteAllBytes(screenshotFullPath,bytes);
			
			if (debugOn.Value == true)Debug.Log("File path: "+screenshotFullPath+" File Name: "+filename.Value);

			temp = null;

			return;

		}


	}
}
