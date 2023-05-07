#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SirenixPowered.ExtensionMethods
{
    public static class EditorExtension
    {
        public static Texture2D Icon(string iconPath) => Resources.Load<Texture2D>(iconPath);

        public static Font Font(string fontPath) => Resources.Load<Font>(fontPath);

        public static void Line(Color color, int height = 2, params GUILayoutOption[] options)
        {
            GUIStyle newStyle = new GUIStyle();
            newStyle.normal.background = EditorExtension.MakeTex(2, height, color);

            GUILayout.BeginVertical(newStyle);
            {
                GUILayout.Space(height);
            }
            GUILayout.EndVertical();
        }

        public static void Button(string icon, GUIStyle style, Tones color, System.Action OnClick, params GUILayoutOption[] options)
        {
            GUI.backgroundColor = ColorExtension.Color(color);

            if (GUILayout.Button(Icon(icon), style, options))
            {
                OnClick?.Invoke();
                GUIUtility.ExitGUI();
            }

            GUI.backgroundColor = Color.white;
        }

        public static void Button(string Icon, GUIStyle style, Color color, System.Action OnClick, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(EditorExtension.Icon(Icon), style, options))
            {
                OnClick?.Invoke();
                GUIUtility.ExitGUI();
            }
        }

        public static void DropdownButton(GUIContent guiContent, GUIStyle style, Texture2D arrowDown, Texture2D arrowUp, Tones color, System.Action OnClick, bool dropdownOpened, params GUILayoutOption[] options)
        {
            GUI.backgroundColor = ColorExtension.Color(color);

            GUILayout.BeginHorizontal("Toolbar");
            {
                Texture2D dropdownIcon = dropdownOpened == true ? arrowDown : arrowUp;
                GUILayout.Label(dropdownIcon, style, GUILayout.Width(20));

                if (GUILayout.Button(guiContent, style, options))
                {
                    OnClick?.Invoke();
                    GUIUtility.ExitGUI();
                }
            }

            GUILayout.EndHorizontal();

            GUI.backgroundColor = Color.white;
        }

        public static void DropdownButton(GUIContent guiContent, GUIStyle style, Texture2D arrowDown, Texture2D arrowUp, Color color, System.Action OnClick, bool dropdownOpened, params GUILayoutOption[] options)
        {
            GUI.backgroundColor = color;

            GUILayout.BeginHorizontal("Toolbar");
            {
                Texture2D dropdownIcon = dropdownOpened == true ? arrowDown : arrowUp;
                GUILayout.Label(dropdownIcon, style, GUILayout.Width(20));

                if (GUILayout.Button(guiContent, style, options))
                {
                    OnClick?.Invoke();
                    GUIUtility.ExitGUI();
                }
            }
            GUILayout.EndHorizontal();

            GUI.backgroundColor = Color.white;
        }

        public static Texture2D MakeTex(int width, int height, Color color)
        {
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = color;
            }

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }

        public static Texture2D MakeTex(int width, int height, Color textureColor, RectOffset border, Color bordercolor)
        {
            int widthInner = width;
            width += border.left;
            width += border.right;

            Color[] pix = new Color[width * (height + border.top + border.bottom)];

            for (int i = 0; i < pix.Length; i++)
            {
                if (i < (border.bottom * width))
                {
                    pix[i] = bordercolor;
                }
                   
                else if (i >= ((border.bottom * width) + (height * width)))
                {
                    pix[i] = bordercolor;
                }             
                else
                {
                    if ((i % width) < border.left)
                    {
                        pix[i] = bordercolor;
                    }                      
                    else if ((i % width) >= (border.left + widthInner))
                    {
                        pix[i] = bordercolor;
                    }
                    else
                    {
                        pix[i] = textureColor;
                    } 
                }
            }

            Texture2D result = new Texture2D(width, height + border.top + border.bottom);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }

        public static void ObjectField(ref Object refValue, Color color, string label)
        {
            GUI.backgroundColor = color;
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            {
                GUI.backgroundColor = Color.white;

                GUIStyle _labelStyle = new GUIStyle(GUI.skin.label);
                _labelStyle.fontStyle = FontStyle.Bold;
                GUILayout.Label(label, _labelStyle, GUILayout.Width(120));

                refValue = (Object)EditorGUILayout.ObjectField(refValue, typeof(Object), false);

                //refValue = EditorGUILayout.Slider(refValue, minMaxValue.x, minMaxValue.y);
                //refValue = EditorGUILayout.FloatField(refValue, GUILayout.Width(50));

            }
            GUILayout.EndHorizontal();
        }

        public static void CurveField(ref AnimationCurve refValue, Color color, string Label)
        {
            GUI.backgroundColor = color;
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            {
                GUI.backgroundColor = Color.white;

                GUIStyle _labelStyle = new GUIStyle(GUI.skin.label);
                _labelStyle.fontStyle = FontStyle.Bold;
                GUILayout.Label(Label, _labelStyle, GUILayout.Width(120));

                refValue = EditorGUILayout.CurveField(refValue);
            }
            GUILayout.EndHorizontal();
        }

        public static void TitleWithIcon(string title, Color background, GUIStyle titleStyle, Texture icon, byte iconDistance = 0, Vector4? offset = null, params GUILayoutOption[] options)
        {
            GUIStyle newStyle = new GUIStyle();
            newStyle.normal.background = EditorExtension.MakeTex(2, 2, background);

            Vector4 newOffse = offset != null ? offset.Value : Vector4.zero;

            string distance = string.Empty;

            for (int i = 0; i < iconDistance; i++)
            {
                distance += " ";
            }

            GUILayout.BeginVertical(newStyle);
            {
                GUILayout.Space(newOffse.x);
                
                GUILayout.BeginHorizontal(newStyle);
                {
                    GUILayout.Space(newOffse.z);

                    GUIContent gUIContent = new GUIContent();
                    gUIContent.text = $"{distance}{title}";
                    gUIContent.image = icon;

                    GUILayout.Label(gUIContent, titleStyle, options);

                    GUILayout.Space(newOffse.w);
                    GUILayout.EndHorizontal();
                }

                GUILayout.Space(newOffse.y);
            }
            GUILayout.EndVertical();
        }
    } 
}
#endif