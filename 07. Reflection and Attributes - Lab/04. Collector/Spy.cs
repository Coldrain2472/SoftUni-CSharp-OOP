﻿using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;

namespace Stealer;

public class Spy
{
    public string StealFieldInfo(string investigatedClass, params string[] fields)
    {
        Type classType = Type.GetType(investigatedClass);
        FieldInfo[] classFields = classType.GetFields((BindingFlags)60);
        StringBuilder sb = new StringBuilder();
        Object classInstance = Activator.CreateInstance(classType, new object[] { });

        sb.AppendLine($"Class under investigation: {investigatedClass}");

        foreach (FieldInfo field in classFields.Where(f => fields.Contains(f.Name)))
        {
            sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
        }

        return sb.ToString().Trim();
    }

    public string AnalyzeAccessModifiers(string className)
    {
        Type classType = Type.GetType(className);
        FieldInfo[] fields = classType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
        MethodInfo[] publicMethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
        MethodInfo[] nonPublicMethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

        StringBuilder sb = new StringBuilder();

        foreach (FieldInfo field in fields)
        {
            sb.AppendLine($"{field.Name} must be private!");
        }
        foreach (MethodInfo method in nonPublicMethods.Where(m => m.Name.StartsWith("get")))
        {
            sb.AppendLine($"{method.Name} have to be public!");
        }
        foreach (MethodInfo method in publicMethods.Where(m => m.Name.StartsWith("set")))
        {
            sb.AppendLine($"{method.Name} have to be private!");
        }

        return sb.ToString().Trim();
    }

    public string RevealPrivateMethods(string className)
    {
        Type classType = Type.GetType(className);
        MethodInfo[] nonPublicMethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"All Private Methods of Class: {className}");
        sb.AppendLine($"Base Class: {classType.BaseType.Name}");
        foreach (MethodInfo method in nonPublicMethods)
        {
            sb.AppendLine(method.Name);
        }

        return sb.ToString().TrimEnd();
    }

    public string CollectGettersAndSetters(string className)
    {
        Type classType = Type.GetType(className);
        MethodInfo[] methods = classType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        StringBuilder sb = new StringBuilder();
        foreach (MethodInfo method in methods.Where(m => m.Name.StartsWith("get")))
        {
            sb.AppendLine($"{method.Name} will return {method.ReturnType}");
        }
        foreach (MethodInfo method in methods.Where(m => m.Name.StartsWith("set")))
        {
            sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
        }

        return sb.ToString().TrimEnd();
    }
}