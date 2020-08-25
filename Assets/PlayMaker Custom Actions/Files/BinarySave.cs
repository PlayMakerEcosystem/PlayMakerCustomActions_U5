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
    [Tooltip("Save Data to be load later using Binary Load, for Array only type of float, integer, string that are currently working, feel free to test other data types")]
    public class BinarySave : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("Save your float variable here, can be more than one")]
        public FsmFloat[] floatVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("Save your integer variable here, can be more than one")]
        public FsmInt[] intVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("Save your string variable here, can be more than one")]
        public FsmString[] stringVar;
        [UIHint(UIHint.Variable)]
        [Tooltip("Save your bool variable here, can be more than one")]
        public FsmBool[] boolVar;

        [UIHint(UIHint.Variable)]
        [Tooltip("Save your array variable here, can be more than one and can be a different type (only float, integer and string are currently tested)")]
        public FsmArray[] arrayVar;

        [Tooltip("name of the save file that will be outputted")]
        public FsmString filename;

        private SavedData saveData = new SavedData();

        public override void Reset()
        {
            floatVar = null;
            intVar = null;
            stringVar = null;
            arrayVar = null;
        }

        public override void OnEnter()
        {
            DoSave();

            Finish();
        }

        private void DoSave()
        {
            saveData.savedFloat = floatVar;
            saveData.savedInt = intVar;
            saveData.savedString = stringVar;
            saveData.savedBool = boolVar;

            saveData.savedArray = new FsmArray[arrayVar.Length];

            for (int i = 0; i < saveData.savedArray.Length; i++)
            {
                saveData.savedArray[i] = arrayVar[i].Clone() as FsmArray;
                saveData.savedArray[i].Resize(saveData.savedArray[i].Length + 1);
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + filename.Value);
            bf.Serialize(file, saveData);
            file.Close();
        }
    }

    [System.Serializable]
    public class SavedData
    {
        public FsmFloat[] savedFloat;
        public FsmInt[] savedInt;
        public FsmString[] savedString;
        public FsmBool[] savedBool;
        public FsmArray[] savedArray;
    }
}

