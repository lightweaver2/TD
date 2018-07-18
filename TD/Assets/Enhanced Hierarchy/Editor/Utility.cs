﻿using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace EnhancedHierarchy {
    /// <summary>
    /// Misc utilities for Enhanced Hierarchy.
    /// </summary>
    internal static class Utility {

        private const string CTRL = "Ctrl";
        private const string CMD = "Cmd";
        private const string MENU_ITEM_PATH = "Edit/Enhanced Hierarchy %h";

        private static int errorCount;
        private static readonly GUIContent tempContent = new GUIContent();

        public static string CtrlKey { get { return Application.platform == RuntimePlatform.OSXEditor ? CMD : CTRL; } }

        [MenuItem(MENU_ITEM_PATH, false, int.MinValue)]
        private static void EnableDisableHierarchy() {
            Preferences.Enabled.Value = !Preferences.Enabled;
            EditorApplication.RepaintHierarchyWindow();
        }

        [MenuItem(MENU_ITEM_PATH, true)]
        private static bool CheckHierarchyEnabled() {
            Menu.SetChecked(MENU_ITEM_PATH, Preferences.Enabled);
            return true;
        }

        public static void EnableFPSCounter() {
            var frames = 0;
            var fps = 0d;
            var lastTime = 0d;
            var content = new GUIContent();
            var evt = EventType.Repaint;

            EditorApplication.hierarchyWindowItemOnGUI += (id, rect) => {
                using(ProfilerSample.Get("Enhanced Hierarchy"))
                using(ProfilerSample.Get("FPS Counter")) {
                    if(evt == Event.current.type)
                        return;

                    evt = Event.current.type;

                    if(evt == EventType.Repaint)
                        frames++;

                    if(EditorApplication.timeSinceStartup - lastTime < 0.5d)
                        return;

                    fps = frames / (EditorApplication.timeSinceStartup - lastTime);
                    lastTime = EditorApplication.timeSinceStartup;
                    frames = 0;

                    content.text = string.Format("{0:00.0} FPS", fps);
                    content.image = Styles.warningIcon;

                    SetHierarchyTitle(content);
                }
            };
        }

        public static bool ShouldCalculateTooltipAt(Rect area) {
            return area.Contains(Event.current.mousePosition);
        }

        public static void ForceUpdateHierarchyEveryFrame() {
            EditorApplication.update += () => {
                if(EditorWindow.mouseOverWindow)
                    EditorApplication.RepaintHierarchyWindow();
            };
        }

        public static void LogException(Exception e) {
            Debug.LogError("Unexpected exception in Enhanced Hierarchy: {0}");
            Debug.LogException(e);

            if(errorCount++ >= 10) {
                Debug.LogWarning("Automatically disabling Enhanced Hierarchy, if the error persists contact the developer");
                Preferences.Enabled.Value = false;
                errorCount = 0;
            }
        }

        public static void SetHierarchyTitle(string title) {
            try {
                ReflectionHelper.HierarchyWindowInstance.titleContent.text = title;
            }
            catch(Exception e) {
                Debug.LogWarning("Failed to set hierarchy title: " + e);
            }
        }

        public static void SetHierarchyTitle(GUIContent content) {
            try {
                ReflectionHelper.HierarchyWindowInstance.titleContent = content;
            }
            catch(Exception e) {
                Debug.LogWarning("Failed to set hierarchy title: " + e);
            }
        }

        public static GUIStyle CreateStyleFromTextures(Texture2D on, Texture2D off) {
            return CreateStyleFromTextures(null, on, off);
        }

        public static GUIStyle CreateStyleFromTextures(GUIStyle reference, Texture2D on, Texture2D off) {
            using(ProfilerSample.Get()) {
                var style = reference != null ? new GUIStyle(reference) : new GUIStyle();

                style.active.background = off;
                style.focused.background = off;
                style.hover.background = off;
                style.normal.background = off;
                style.onActive.background = on;
                style.onFocused.background = on;
                style.onHover.background = on;
                style.onNormal.background = on;
                style.imagePosition = ImagePosition.ImageOnly;
                style.fixedHeight = 15f;
                style.fixedWidth = 15f;

                return style;
            }
        }

        public static Texture2D FindOrLoad(byte[] bytes, string name) {
            if(Preferences.DebugEnabled)
                return LoadTexture(bytes, name);
            else
                return FindTextureFromName(name) ?? LoadTexture(bytes, name);
        }

        public static Texture2D LoadTexture(byte[] bytes, string name) {
            using(ProfilerSample.Get())
                try {
                    var texture = new Texture2D(0, 0, TextureFormat.ARGB32, false, false);

                    texture.name = name;
                    texture.hideFlags = HideFlags.HideAndDontSave;
                    texture.LoadImage(bytes);

                    return texture;
                }
                catch(Exception e) {
                    Debug.LogErrorFormat("Failed to load texture \"{0}\": {1}", name, e);
                    return null;
                }
        }

        public static Texture2D FindTextureFromName(string name) {
            using(ProfilerSample.Get())
                try {
                    var textures = Resources.FindObjectsOfTypeAll<Texture2D>();

                    for(var i = 0; i < textures.Length; i++)
                        if(textures[i].name == name)
                            return textures[i];

                    return null;
                }
                catch(Exception e) {
                    Debug.LogErrorFormat("Failed to find texture \"{0}\": {1}", name, e);
                    return null;
                }
        }

        public static Color GetHierarchyColor(Transform t) {
            if(!t)
                return Color.black;

            return GetHierarchyColor(t.gameObject);
        }

        public static Color GetHierarchyColor(GameObject go) {
            if(!go)
                return Color.black;

            return GetHierarchyLabelStyle(go).normal.textColor;
        }

        public static GUIStyle GetHierarchyLabelStyle(GameObject go) {
            using(ProfilerSample.Get()) {
                if(!go)
                    return EditorStyles.label;

                var prefabType = PrefabUtility.GetPrefabType(PrefabUtility.FindPrefabRoot(go));
                var active = go.activeInHierarchy;

                switch(prefabType) {
                    case PrefabType.PrefabInstance:
                    case PrefabType.ModelPrefabInstance:
                        return active ? Styles.labelPrefab : Styles.labelPrefabDisabled;

                    case PrefabType.MissingPrefabInstance:
                        return active ? Styles.labelPrefabBroken : Styles.labelPrefabBrokenDisabled;

                    default:
                        return active ? Styles.labelNormal : Styles.labelDisabled;
                }
            }
        }

        public static Color OverlayColors(Color src, Color dst) {
            using(ProfilerSample.Get()) {
                var alpha = dst.a + src.a * (1f - dst.a);
                var result = (dst * dst.a + src * src.a * (1f - dst.a)) / alpha;

                result.a = alpha;

                return result;
            }
        }

        public static bool LastInHierarchy(Transform t) {
            using(ProfilerSample.Get()) {
                if(!t)
                    return true;

                return t.parent.GetChild(t.parent.childCount - 1) == t;
            }
        }

        public static bool LastInHierarchy(GameObject go) {
            if(!go)
                return true;

            return LastInHierarchy(go.transform);
        }

        public static void LockObject(GameObject go) {
            using(ProfilerSample.Get()) {
                go.hideFlags |= HideFlags.NotEditable;

                if(!Preferences.AllowSelectingLockedSceneView)
                    foreach(var comp in go.GetComponents<Component>().Where(comp => comp && !(comp is Transform))) {
                        comp.hideFlags |= HideFlags.NotEditable;
                        comp.hideFlags |= HideFlags.HideInHierarchy;
                    }

                EditorUtility.SetDirty(go);
            }
        }

        public static void UnlockObject(GameObject go) {
            using(ProfilerSample.Get()) {
                go.hideFlags &= ~HideFlags.NotEditable;

                foreach(var comp in go.GetComponents<Component>().Where(comp => comp && !(comp is Transform))) {
                    comp.hideFlags &= ~HideFlags.NotEditable;
                    comp.hideFlags &= ~HideFlags.HideInHierarchy;
                }

                EditorUtility.SetDirty(go);
            }
        }

        public static void RelockAllObjects() {
            using(ProfilerSample.Get())
                foreach(var obj in Resources.FindObjectsOfTypeAll<GameObject>())
                    if(obj && (obj.hideFlags & HideFlags.HideInHierarchy) == 0 && !EditorUtility.IsPersistent(obj)) {
                        var locked = (obj.hideFlags & HideFlags.NotEditable) != 0;

                        UnlockObject(obj);

                        if(locked)
                            LockObject(obj);
                    }
        }

        public static void UnlockAllObjects() {
            using(ProfilerSample.Get())
                foreach(var obj in Resources.FindObjectsOfTypeAll<GameObject>())
                    if(obj && (obj.hideFlags & HideFlags.HideInHierarchy) == 0 && !EditorUtility.IsPersistent(obj))
                        UnlockObject(obj);
        }

        public static void ApplyPrefabModifications(GameObject go, bool allowCreatingNew) {
            var isPrefab = PrefabUtility.GetPrefabType(go) == PrefabType.PrefabInstance;

            if(isPrefab) {
                var selection = Selection.instanceIDs;
                var prefab = PrefabUtility.GetCorrespondingObjectFromSource(go);

                Selection.activeGameObject = go;
                EditorApplication.ExecuteMenuItem("GameObject/Apply Changes To Prefab");
                EditorUtility.SetDirty(prefab);
                Selection.instanceIDs = selection;
            }
            else if(allowCreatingNew) {
                var path = EditorUtility.SaveFilePanelInProject("Save prefab", "New Prefab", "prefab", "Save the selected prefab");

                if(!string.IsNullOrEmpty(path))
                    PrefabUtility.CreatePrefab(path, go, ReplacePrefabOptions.ConnectToPrefab);
            }
        }

        public static string EnumFlagsToString(Enum value) {
            using(ProfilerSample.Get())
                try {
                    if((int)(object)value == -1)
                        return "Everything";

                    var str = new StringBuilder();
                    var separator = ", ";

                    foreach(var enumValue in Enum.GetValues(value.GetType())) {
                        var i = (int)enumValue;
                        if(i != 0 && (i & (i - 1)) == 0 && Enum.IsDefined(value.GetType(), i) && (Convert.ToInt32(value) & i) != 0) {
                            str.Append(ObjectNames.NicifyVariableName(enumValue.ToString()));
                            str.Append(separator);
                        }
                    }

                    if(str.Length > 0)
                        str.Length -= separator.Length;

                    return str.ToString();
                }
                catch(Exception e) {
                    if(Preferences.DebugEnabled)
                        Debug.LogException(e);
                    return string.Empty;
                }
        }

        public static GUIContent GetTempGUIContent(string text, string tooltip = null, Texture2D image = null) {
            tempContent.text = text;
            tempContent.tooltip = tooltip;
            tempContent.image = image;
            return tempContent;
        }

        public static string SafeGetName(this IconBase icon) {
            try {
                return icon.Name;
            }
            catch(Exception e) {
                Debug.LogException(e);
                Preferences.ForceDisableButton(icon);
                return string.Empty;
            }
        }

        public static float SafeGetWidth(this IconBase icon) {
            try {
                return icon.Width;
            }
            catch(Exception e) {
                Debug.LogException(e);
                Preferences.ForceDisableButton(icon);
                return 0f;
            }
        }

        public static void SafeInit(this IconBase icon) {
            try {
                icon.Init();
            }
            catch(Exception e) {
                Debug.LogException(e);
                Preferences.ForceDisableButton(icon);
            }
        }

        public static void SafeDoGUI(this IconBase icon, Rect rect) {
            try {
                icon.DoGUI(rect);
            }
            catch(Exception e) {
                Debug.LogException(e);
                Preferences.ForceDisableButton(icon);
            }
        }

    }
}