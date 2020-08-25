// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Author: Romi Fauzi
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Load Save Binary Disk

using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Files")]
    [Tooltip("Load Data saved using Binary Save, for Array only type of float, integer, string that are currently working, feel free to test other data types")]
    public class BinaryLoad : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("load your float variable here if there are any saved, can be more than one")]
        public FsmFloat[] floatVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("load your int variable here if there are any saved, can be more than one")]
        public FsmInt[] intVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("load your string variable here if there are any saved, can be more than one")]
        public FsmString[] stringVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("load your bool variable here if there are any saved, can be more than one")]
        public FsmBool[] boolVar;

        [UIHint(UIHint.Variable)]
        [Tooltip("load your array variable here, can be more than one and can be a different type (only float, integer and string are currently tested)")]
        public FsmArray[] arrayVar;

        [Tooltip("name of the save file that will be loaded, make sure the name its the same with the one on Binary Save")]
        public FsmString filename;

        public FsmEvent notFoundEvent;

        private SavedData loadData = new SavedData();

        public override void Reset()
        {
            floatVar = null;
            intVar = null;
            stringVar = null;
            arrayVar = null;
        }

        public override void OnEnter()
        {
            DoLoad();

            Finish();
        }

        private void DoLoad()
        {
            if (File.Exists(Application.persistentDataPath + "/" + filename.Value))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + filename.Value, FileMode.Open);
                loadData = (SavedData)bf.Deserialize(file);
                file.Close();

                if (loadData.savedFloat.Length > 0)
                {
                    for (int i = 0; i < floatVar.Length; i++)
                        floatVar[i].Value = loadData.savedFloat[i].Value;
                }

                if (loadData.savedInt.Length > 0)
                {
                    for (int i = 0; i < intVar.Length; i++)
                        intVar[i].Value = loadData.savedInt[i].Value;
                }


                if (loadData.savedString.Length > 0)
                {
                    for (int i = 0; i < stringVar.Length; i++)
                        stringVar[i].Value = loadData.savedString[i].Value;
                }

                if (loadData.savedBool.Length > 0)
                {
                    for (int i = 0; i < stringVar.Length; i++)
                        boolVar[i].Value = loadData.savedBool[i].Value;
                }

                if (loadData.savedArray.Length > 0)
                {
                    for (int i = 0; i < loadData.savedArray.Length; i++)
                    {
                        arrayVar[i].Values = loadData.savedArray[i].Values.Clone() as object[];
                        arrayVar[i].Resize(arrayVar[i].Length - 1);
                    }
                }
            }
            else
                Fsm.Event(notFoundEvent);
        }
    }
}

