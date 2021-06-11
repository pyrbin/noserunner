using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class GameObjectExtensions
{
    public static void SetLayerRecursively(this GameObject root, int layer)
    {
        if (root == null)
        {
            throw new ArgumentNullException(nameof(root), "Root transform can't be null.");
        }

        foreach (var child in root.transform.EnumerateHierarchy())
        {
            child.gameObject.layer = layer;
        }
    }

    public static bool IsInLayerMask(this GameObject gameObject, LayerMask layerMask)
    {
        LayerMask gameObjectMask = 1 << gameObject.layer;
        return (gameObjectMask & layerMask) == gameObjectMask;
    }

    public static void ForEachComponent<T>(this GameObject gameObject, Action<T> action)
    {
        foreach (T i in gameObject.GetComponents<T>()) action(i);
    }

    public static bool TryGetComponentSelfOrParent<T>(this Behaviour self, out T component) where T : Component
    {
        try
        {
            return self.TryGetComponent(out component)
                || self.transform.parent.TryGetComponent(out component);
        }
        catch
        {
            component = null;
            return false;
        }
    }

    public static T GetComponentSelfOrParent<T>(this Behaviour self) where T : Component
    {
        self.TryGetComponentSelfOrParent<T>(out var component);
        return component;
    }
}

public static class LayerMaskExtensions
{
    public static int MaskValue(this LayerMask me)
    {
        return (int)Mathf.Log(me.value, 2);
    }
}

