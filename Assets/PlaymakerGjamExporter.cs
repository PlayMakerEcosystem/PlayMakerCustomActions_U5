using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Collections.Generic;


public class PlaymakerGjamExporter : EditorWindow
{
    private const float windowWidth = 500;
    private const float windowHeight = 100;
    private string directoryPath = "Assets";
    private string fileToAdd;
    private string packageName;
    private string targetDirectory;
    private string get_targetDirectory;
    private List<string> includeFileList = new List<string>();
    private bool fileExistsCheck;

    [MenuItem("PlayMaker/Addons/Game Jam Exporter", false, 500)]
    public static void OpenWelcomeWindow()
    {
        GetWindow<PlaymakerGjamExporter>(true);
    }



    private void OnEnable()
    {

#if UNITY_PRE_5_1
            title = "Welcome To Samples For PlayMaker";
#else
        titleContent = new GUIContent("Playmaker Game Jam Exporter");
#endif
        maxSize = new Vector2(windowWidth, windowHeight);
        minSize = maxSize;
    }

    public void OnGUI()
    {
        GUILayout.BeginVertical("box");
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUI.color = Color.yellow;
        if (GUILayout.Button("Create Package", GUILayout.Height(30)))
        {
            CreatePackage();
            GUIUtility.ExitGUI();
        }
        GUI.color = new Color(1, 1, 1, 1);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        // Set Package Name
        GUILayout.BeginVertical("box");
        GUILayout.BeginHorizontal();
        GUILayout.Label("Package Name", GUILayout.Width(120));
        packageName = EditorGUILayout.TextField(packageName, GUILayout.MinWidth(5));
        GUILayout.EndHorizontal();

        GUILayout.Space(3);
        // Target Directory
        GUILayout.BeginHorizontal();
        GUILayout.Label("Target Directory", GUILayout.Width(120));
        targetDirectory = EditorGUILayout.TextField(targetDirectory, GUILayout.MinWidth(1));
        if (GUILayout.Button("Select Directory", GUILayout.Height(16), GUILayout.Width(110)))
        {
            OnSetTargetDirectory();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(3);
        GUILayout.EndVertical();
    }

    private void OnSetTargetDirectory()
    {
        get_targetDirectory = EditorUtility.OpenFolderPanel(Application.dataPath, "", "*.*");
        targetDirectory = get_targetDirectory;
        Repaint();
    }

    private void CreatePackage()
    {
        if (packageName == "" || packageName == null)
        {
            EditorUtility.DisplayDialog("No Package Name", "There is no package Name defined\n\nPlease provide a package name", "Ok", "");
            return;
        }
        else if (targetDirectory == "" || targetDirectory == null)
        {
            bool option = EditorUtility.DisplayDialog("No Target Directory", "You need to set a target directory", "Ok", "");
            if (option)
            {
                return;
            }
        }
        else
            includeFileList.Clear();

        Debug.Log("Creating");
        DirectoryInfo dir = new DirectoryInfo(directoryPath);
        FileInfo[] info = dir.GetFiles("*.*", SearchOption.AllDirectories);
        foreach (FileInfo f in info)
        {
            string get_File = f.FullName;
            int index = get_File.IndexOf("Assets");
            fileToAdd = get_File.Substring(index);
            fileToAdd = fileToAdd.Replace('\\', '/');

            if (!fileToAdd.Contains("PlayMaker.dll"))
            {
                includeFileList.Add(fileToAdd);
            }
            else Debug.Log("dll found : " + fileToAdd);
        }


        DirectoryInfo dirInfo = new DirectoryInfo("Assets/PlayMaker/Editor/Install");
        FileInfo[] infoFile = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);
        foreach (FileInfo f in infoFile)
        {
            string get_Folder = f.FullName;
            int indexfolder = get_Folder.IndexOf("Assets");
            fileToAdd = get_Folder.Substring(indexfolder);
            fileToAdd = fileToAdd.Replace('\\', '/');
            if (includeFileList.Contains(fileToAdd))
            {

                int index = includeFileList.IndexOf(fileToAdd);
                if (index != -1)
                {
                    includeFileList.RemoveAt(index);
                }
            }
        }
        BuildPackage();
    }

    private void BuildPackage()
    {

        string exportdirectory = targetDirectory + "/" + packageName + ".unitypackage";
        if (!Directory.Exists(targetDirectory))
        {
            Directory.CreateDirectory(targetDirectory);
            EditorUtility.DisplayProgressBar("Exporting", "This might take a while depending on the size of your Project", 10f);
            AssetDatabase.ExportPackage(includeFileList.ToArray(), exportdirectory, ExportPackageOptions.Default);
            EditorUtility.ClearProgressBar();
            EditorUtility.RevealInFinder(exportdirectory);
        }
        else
        {
            if (File.Exists(exportdirectory))
            {

                bool overwrite = EditorUtility.DisplayDialog("This Package Exists already.", "Overwrite package?", "yes", "No");
                if (overwrite)
                {
                    File.Delete(exportdirectory);
                    EditorUtility.DisplayProgressBar("Exporting", "This might take a while depending on the size of your Project", 10f);
                    AssetDatabase.ExportPackage(includeFileList.ToArray(), exportdirectory, ExportPackageOptions.Default);
                    EditorUtility.ClearProgressBar();
                    EditorUtility.RevealInFinder(exportdirectory);
                }
                else
                {
                    EditorUtility.DisplayDialog("Build Canceled", "I saved your life!", "Thank You", "");
                }
            }
            else
            {
                if (File.Exists(exportdirectory))
                {
                    File.Delete(exportdirectory);
                }

                EditorUtility.DisplayProgressBar("Exporting", "this might take a while depending on the size of your included files/folders\n Please wait", 10f);
                AssetDatabase.ExportPackage(includeFileList.ToArray(), exportdirectory, ExportPackageOptions.Default);
                EditorUtility.ClearProgressBar();
                EditorUtility.RevealInFinder(exportdirectory);
            }
        }


    }


}