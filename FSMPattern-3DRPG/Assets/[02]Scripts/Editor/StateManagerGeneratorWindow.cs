using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

public class StateManagerGeneratorWindow : EditorWindow
{
    [MenuItem("Tools/State Manager Generator")]
    public static void ShowWindow()
    {
        GetWindow<StateManagerGeneratorWindow>("State Manager Generator");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Generate"))
        {
            Dictionary<string, List<Type>> enums = new();
        
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(StateAttribute), true).Length > 0);

                foreach (var type in types)
                {
                    var attr = type.GetCustomAttribute<StateAttribute>();
                    if (attr != null && !string.IsNullOrEmpty(attr.StateName))
                    {
                        if (!enums.ContainsKey(attr.StateName))
                        {
                            enums.Add(attr.StateName, new List<Type>());
                        }
                        enums[attr.StateName].Add(type);
                    }
                }
            }

            var List = enums.Keys.OrderBy(s => s).ToList();
            var savePath = "Assets/Scripts/Generated";
            StringBuilder sb = new StringBuilder();
            
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            
            sb.AppendLine($"public class StateTypesClasses");
            sb.AppendLine("{");
            GenerateFillEnumText(sb, List);

            sb.AppendLine("\tprivate static readonly Dictionary<Type, StateTypes> TypeToState = new()");
            sb.AppendLine("\t{");
            
            foreach (var se in List)
            {
                var orderedList = enums[se].OrderBy(t => t.Name).ToList();
                foreach (var type in orderedList)
                {
                    sb.AppendLine($"\t\t[typeof({type})] = StateTypes.{se},");
                }
            }
            
            sb.AppendLine("\t};");
            
            sb.AppendLine("\tpublic static StateTypes GetState<T>() => GetState(typeof(T));");
            sb.AppendLine("\tpublic static StateTypes GetState(Type type )");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\treturn TypeToState.GetValueOrDefault(type, StateTypes.None);");
            sb.AppendLine("\t}");
            
            sb.AppendLine("}");
            
            string filePath = Path.Combine(savePath, "StateTypesClasses.cs");
            File.WriteAllText(filePath, sb.ToString());
        }
    }

    private static void GenerateFillEnumText(StringBuilder sb, List<string> List)
    {
        sb.AppendLine($"\tpublic enum StateTypes");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tNone,");
        foreach (var se in List)
        {
            sb.AppendLine($"\t\t{se},");
        }
        sb.AppendLine("\t\tMax");
        sb.AppendLine("\t}");
    }
}