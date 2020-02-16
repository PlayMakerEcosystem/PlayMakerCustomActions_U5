// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Folder New


using System;
#if !UNITY_WINRT
using System.IO;
#else
using UnityEngine.Windows.File;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Files")]
	[Tooltip("Create a directory")]
	public class CreateDirectory : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString directoryPath;
		
		[ActionSection("Results")]
		public FsmEvent successEvent;
		public FsmEvent alreadyExistsEvent;
		public FsmEvent failureEvent;
	
		public override void Reset()
		{
			directoryPath = null;
			failureEvent = null;
			alreadyExistsEvent = null;
			successEvent = null;
		}

		private DirectoryInfo _info;
		
		public override void OnEnter()
		{
			try
			{
				if (Directory.Exists(directoryPath.Value))
				{
					Fsm.Event(alreadyExistsEvent);
				}
				else
				{
					_info = Directory.CreateDirectory(directoryPath.Value);
				
					if (_info == null)	{
						Fsm.Event(failureEvent);
					} else
					{
						if (!_info.Exists)
						{
							Fsm.EventData.StringData = "Does not exists";
							Fsm.Event(failureEvent);
						}
						else
						{
							Fsm.Event(successEvent);
						}
					}
				}
				
			}
			catch (Exception e)
			{
				Fsm.EventData.StringData = e.Message;
				Fsm.Event(failureEvent);
			}
			
			Finish();
		}
	}
}
