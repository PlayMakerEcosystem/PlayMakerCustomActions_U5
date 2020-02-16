// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;
using System.Text;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.String)]
    [Tooltip("Get the filename from a path string")]
    public class GetFilenameFromPath : FsmStateAction
    {
        [RequiredField]
        public FsmString path;

        [UIHint(UIHint.Variable)]
        public FsmString filename;

        [UIHint(UIHint.Variable)]
        public FsmString name;
        [UIHint(UIHint.Variable)]
        public FsmString extension;

        public override void Reset()
        {
            path = null;
            filename = null;

        }

        public override void OnEnter()
        {

            string _path = Path.Combine("", path.Value);

            if (!filename.IsNone)
            {
                filename.Value = Path.GetFileName(_path);
            }

            if (!name.IsNone)
            {
                name.Value = Path.GetFileNameWithoutExtension(_path);
            }

            if (!extension.IsNone)
            {
                extension.Value = Path.GetExtension(_path);
            }

            Finish();
        }

      
    }
}
