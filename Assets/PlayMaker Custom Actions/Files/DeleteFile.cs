// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
#if !UNITY_WINRT
using System.IO;
#else
using UnityEngine.Windows.File;
#endif

using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Files")]
	[Tooltip("Delete a file")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12475.0")]
	public class DeleteFile : FsmStateAction
	{
		[ActionSection("Input")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString pathToFile;
		[ActionSection("Option")]
		public FsmBool secureDelete;
		public FsmInt loop;
		[ActionSection("Output")]
		public FsmEvent done;
		public FsmEvent failed;
	
		public override void Reset()
		{
			pathToFile = null;
			done = null;
			failed = null;
			secureDelete = false;
			loop = 2;
		}

		public override void OnEnter()
		{
			bool exist = File.Exists(pathToFile.Value);

			if (exist == false)	{
				Fsm.Event(failed);

			}

			else {

				if (secureDelete.Value == false){
				File.Delete(pathToFile.Value);
				Fsm.Event(done);
				}

				else {
					TwoPassFileErase(pathToFile.Value,128,new System.Random(), loop.Value);
					Fsm.Event(done);
				}
			}

			Finish();
		}

			public static void TwoPassFileErase(string path, int blockSize, System.Random random, int loop)
		{
			if (path == null) 
			if (blockSize <= 0) 
			if (random == null)
			using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Write))
			{
				Byte[] buffer = new byte[blockSize];
				long length = stream.Length;

				for (int i = 0; i < loop; i++){

				for (long index = 0; index < length; index += buffer.Length)
				{
					random.NextBytes(buffer);
					stream.Write(buffer, 0, buffer.Length);
				}

				stream.Flush();
				stream.Seek(0, SeekOrigin.Begin);
				Array.Clear(buffer, 0, buffer.Length);

				for (long index = 0; index < length; index += buffer.Length)
				{
					stream.Write(buffer, 0, buffer.Length);
				}

				stream.Flush();
				}
			}
			string newPath;
			do
			{
				newPath = Path.Combine(Path.GetDirectoryName(path), Path.GetRandomFileName());
			} while (File.Exists(newPath));
			File.Move(path, newPath);
			File.Delete(newPath);

			return;
		}


		
	}
}
