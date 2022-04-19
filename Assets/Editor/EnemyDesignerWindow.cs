using System.Collections;
using UnityEngine;
using UnityEditor;
using Types;
using System;

public class EnemyDesignerWindow : EditorWindow
{
    Texture2D headerSectionTexture;
    Texture2D mageSectionTexture;
    Texture2D warriorSectionTexture;
    Texture2D rogueSectionTexture;

    Color headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    Rect headerSection;
    Rect mageSection;
    Rect warriorSection;
    Rect rogueSection;

    GUISkin skin;

    static MageData mageData;
    static WarriorData warriorData;
    static RogueData rogueData;

    public static MageData MageInfo { get { return mageData; } }
    public static WarriorData WarriorInfo { get { return warriorData; } }
    public static RogueData RogueInfo { get { return rogueData; } }

    [MenuItem("Window/Enemy Designer")]
    static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    void OnEnable()
    {
        InitTextures();
        InitData();

        skin = Resources.Load<GUISkin>("GuiStyles/EnemyDesingerSkin");
    }

    public static void InitData()
    {
        mageData = (MageData)ScriptableObject.CreateInstance(typeof(MageData));
        warriorData = (WarriorData)ScriptableObject.CreateInstance(typeof(WarriorData));
        rogueData = (RogueData)ScriptableObject.CreateInstance(typeof(RogueData));
    }

    ///<summary>
    ///Initialize Texture2D values
    ///<summary>
    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        mageSectionTexture = new Texture2D(1, 1);
        mageSectionTexture.SetPixel(0, 0, Color.cyan);
        mageSectionTexture.Apply();

        warriorSectionTexture = new Texture2D(1, 1);
        warriorSectionTexture.SetPixel(0, 0, Color.green);
        warriorSectionTexture.Apply();

        rogueSectionTexture = new Texture2D(1, 1);
        rogueSectionTexture.SetPixel(0, 0, Color.yellow);
        rogueSectionTexture.Apply();

        /*mageSectionTexture = Resources.Load<Texture2D>("icons/icon_texture");
        warriorSectionTexture = Resources.Load<Texture2D>("icons/icon_texture");
        rogueSectionTexture = Resources.Load<Texture2D>("icons/icon_texture");*/

    }

    ///<summary>
    ///Similiar to any Update Function
    ///Not called once per frame. Called 1 or more times per interaction. Mouse click vb.
    ///<summary>
    void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
        DrawRogueSettings();
    }

    ///<summary>
    ///Defines Rect and points texture based on Rects
    ///<summary>
    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50f;

        mageSection.x = 0;
        mageSection.y = 50f;
        mageSection.width = Screen.width / 3.7f;
        mageSection.height = Screen.height;

        warriorSection.x = Screen.width / 3.7f;
        warriorSection.y = 50f;
        warriorSection.width = Screen.width / 3.7f;
        warriorSection.height = Screen.height;

        rogueSection.x = (Screen.width / 3.7f) * 2f;
        rogueSection.y = 50f;
        rogueSection.width = Screen.width / 3.8f;
        rogueSection.height = Screen.height;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mageSection, mageSectionTexture);
        GUI.DrawTexture(warriorSection, warriorSectionTexture);
        GUI.DrawTexture(rogueSection, rogueSectionTexture);
    }

    ///<summary>
    ///Draw contents of header
    ///<summary>
    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);

        GUILayout.Label("Enemy Designer                    ", skin.GetStyle("Header1"));

        GUILayout.EndArea();
    }

    ///<summary>
    ///Draw contents of mage region
    ///<summary>
    void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection);

        GUILayout.Label("Mage", skin.GetStyle("MageHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage Type", skin.GetStyle("MageField"));
        mageData.DmgType = (MageDmgType)EditorGUILayout.EnumPopup(mageData.DmgType);
        GUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon Type", skin.GetStyle("MageField"));
        mageData.WpnType = (MageWpnType)EditorGUILayout.EnumPopup(mageData.WpnType);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
        }

        GUILayout.EndArea();
    }

    ///<summary>
    ///Draw contents of warrior region
    ///<summary>
    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);

        GUILayout.Label("Warrior", skin.GetStyle("MageHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Class Type", skin.GetStyle("MageField"));
        warriorData.WarriorClassType = (WarriorClassType)EditorGUILayout.EnumPopup(warriorData.WarriorClassType);
        GUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon Type", skin.GetStyle("MageField"));
        warriorData.WarriorWpnType = (WarriorWpnType)EditorGUILayout.EnumPopup(warriorData.WarriorWpnType);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
        }

        GUILayout.EndArea();
    }

    ///<summary>
    ///Draw contents of rogue region
    ///<summary>
    void DrawRogueSettings()
    {
        GUILayout.BeginArea(rogueSection);

        GUILayout.Label("Rogue", skin.GetStyle("MageHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Strategy Type", skin.GetStyle("MageField"));
        rogueData.RogueStrategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(rogueData.RogueStrategyType);
        GUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon Type", skin.GetStyle("MageField"));
        rogueData.RogueWpnType = (RogueWpnType)EditorGUILayout.EnumPopup(rogueData.RogueWpnType);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROGUE);
        }

        GUILayout.EndArea();
    }

    public class GeneralSettings : EditorWindow
    {
        GUIStyle labelStyle = new GUIStyle();

        public enum SettingsType
        {
            MAGE,
            WARRIOR,
            ROGUE
        }
        static SettingsType dataSetting;
        static GeneralSettings window;

        public static void OpenWindow(SettingsType setting)
        {
            dataSetting = setting;
            window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
            window.minSize = new Vector2(250, 200);
            window.Show();
        }

        void OnEnable()
        {
            InitLabelStyle();
        }

        void InitLabelStyle()
        {
            labelStyle.border = new RectOffset(1, 1, 1, 1);
            //labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.normal.textColor = Color.white;
            labelStyle.padding.right = 20;
        }

        void OnGUI()
        {
            if (window != null)
            {
                switch (dataSetting)
                {
                    case SettingsType.MAGE:
                        DrawSettings((CharacterData)EnemyDesignerWindow.MageInfo);
                        break;
                    case SettingsType.WARRIOR:
                        DrawSettings((CharacterData)EnemyDesignerWindow.WarriorInfo);
                        break;
                    case SettingsType.ROGUE:
                        DrawSettings((CharacterData)EnemyDesignerWindow.RogueInfo);
                        break;
                }
            }
        }

        void DrawSettings(CharacterData characterData)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Prefab");
            characterData.Prefab = (GameObject)EditorGUILayout.ObjectField(characterData.Prefab, typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Name");
            characterData.Name = EditorGUILayout.TextField(characterData.Name);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Max Health");
            characterData.MaxHealth = EditorGUILayout.FloatField(characterData.MaxHealth);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Max Energy");
            characterData.MaxEnergy = EditorGUILayout.FloatField(characterData.MaxEnergy);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Power");
            characterData.Power = EditorGUILayout.Slider(characterData.Power, 0f, 100f);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("% Crit Chance");
            characterData.CritChance = EditorGUILayout.Slider(characterData.CritChance, 0f, characterData.Power);
            EditorGUILayout.EndHorizontal();

            if (characterData.Prefab == null)
            {
                EditorGUILayout.HelpBox("This enemy prefab a [Prefab] before it can be created.", MessageType.Warning);
            }
            else if (characterData.Name == null || characterData.Name.Length < 1)
            {
                EditorGUILayout.HelpBox("This enemy prefab a [Name] before it can be created.", MessageType.Warning);
            }
            else if (GUILayout.Button("Finish and Save", GUILayout.Height(30)))
            {
                SaveCharacterData();
                window.Close();
            }
        }

        void SaveCharacterData()
        {
            string prefabPath;
            string newPrefabPath = "Assets/Prefabs/Characters/";
            string dataPath = "Assets/Resources/CharacterData/Data/";

            switch (dataSetting)
            {
                case SettingsType.MAGE:

                    //create the .asset file
                    dataPath += "Mage/" + EnemyDesignerWindow.MageInfo.Name + ".asset";
                    AssetDatabase.CreateAsset(EnemyDesignerWindow.MageInfo, dataPath);

                    newPrefabPath += "Mage/" + EnemyDesignerWindow.MageInfo.Name + ".prefab";

                    prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.Prefab);
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject magePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                    if (!magePrefab.GetComponent<Mage>())
                        magePrefab.AddComponent<Mage>();
                    magePrefab.GetComponent<Mage>().MageData = EnemyDesignerWindow.MageInfo;

                    break;
                case SettingsType.WARRIOR:

                    //create the .asset file
                    dataPath += "Warrior/" + EnemyDesignerWindow.WarriorInfo.Name + ".asset";
                    AssetDatabase.CreateAsset(EnemyDesignerWindow.WarriorInfo, dataPath);

                    newPrefabPath += "Warrior/" + EnemyDesignerWindow.WarriorInfo.Name + ".prefab";

                    prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.WarriorInfo.Prefab);
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject warriorPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                    if (!warriorPrefab.GetComponent<Warrior>())
                        warriorPrefab.AddComponent<Warrior>();
                    warriorPrefab.GetComponent<Warrior>().WarriorData = EnemyDesignerWindow.WarriorInfo;

                    break;
                case SettingsType.ROGUE:

                    //create the .asset file
                    dataPath += "Rogue/" + EnemyDesignerWindow.RogueInfo.Name + ".asset";
                    AssetDatabase.CreateAsset(EnemyDesignerWindow.RogueInfo, dataPath);

                    newPrefabPath += "Rogue/" + EnemyDesignerWindow.RogueInfo.Name + ".prefab";

                    prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.RogueInfo.Prefab);
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject roguePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                    if (!roguePrefab.GetComponent<Rogue>())
                        roguePrefab.AddComponent<Rogue>();
                    roguePrefab.GetComponent<Rogue>().RogueData = EnemyDesignerWindow.RogueInfo;

                    break;
            }
        }
    }

}
