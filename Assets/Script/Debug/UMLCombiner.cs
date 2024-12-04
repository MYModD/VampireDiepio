using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UMLCombiner : MonoBehaviour
{
    void Start()
    {
        StringBuilder uml = new StringBuilder();
        uml.AppendLine("@startuml");
        uml.AppendLine("hide empty members");
        uml.AppendLine("skinparam classAttributeIconSize 0");

        // カスタムスクリプトを取得
        var customTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => {
                try
                {
                    return assembly.GetTypes();
                }
                catch
                {
                    return Array.Empty<Type>();
                }
            })
            .Where(type => type != null &&
                   type.IsClass &&
                   !type.IsAbstract &&
                   !type.Name.Contains("MonoBehaviour") &&
                   !type.Name.Contains("TMP") &&
                   !type.Name.Contains("Camera") &&
                   !type.Name.Contains("UniTask"))
            .ToList();

        foreach (var type in customTypes)
        {
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                      BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(m => !m.IsSpecialName &&
                           !m.Name.StartsWith("get_") &&
                           !m.Name.StartsWith("set_"))
                .ToList();

            if (methods.Any())
            {
                uml.AppendLine($"class {type.Name} {{");
                foreach (var method in methods)
                {
                    string visibility = method.IsPrivate ? "-" : "+";
                    uml.AppendLine($"    {visibility} {method.Name}()");
                }
                uml.AppendLine("}");
            }
        }

        uml.AppendLine("@enduml");

        // デスクトップに保存
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        File.WriteAllText(Path.Combine(desktopPath, "custom_methods.puml"), uml.ToString());
        Debug.Log($"UML file saved to desktop: custom_methods.puml");
    }
}