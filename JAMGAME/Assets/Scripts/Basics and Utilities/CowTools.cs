using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CowTools
{
    public static T[] LoadAssets<T>(string path) where T : Object
	{
		Object[] assetObjects = Resources.LoadAll(path, typeof(T));

		T[] assets = new T[assetObjects.Length];

		for(int i = 0; i < assetObjects.Length; i++)
		{
			assets[i] = (T)assetObjects[i];
		}

		return assets;
	}

    public static int GetEnumLength<TEnum>()
    {
        return System.Enum.GetNames(typeof(TEnum)).Length;
    }

    public static TEnum StringToEnum<TEnum>(string s)
    {
        return (TEnum)System.Enum.Parse(typeof(TEnum), s, true);
    }

    public static TEnum[] EnumArray<TEnum>()
    {
        return (TEnum[])System.Enum.GetValues(typeof(TEnum));
    }

    public static Quaternion Vec2Rot(Vector2 v, float offset=0)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + offset);
    }

    public static Vector3 ScaleXY(float scale)
    {
        return new Vector3(scale, scale, 1);
    }

    public static Vector3 TakeXY(Vector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }

    public static T PickRandom<T>(T[] array)
    {
        int index = Random.Range(0, array.Length);
        return array[index];
    }

    public static float Blink(float t)
    {
        return 0.5f * (Mathf.Sin(2 * Mathf.PI * t) + 1);
    }

    public static float Throb(float t, float a)
    {
        return (a-1) * Blink(t) + a;
    }

    public static T GetDefault<T>() where T : Object
    {
        return Resources.Load<T>("default");
    }

    public static Dictionary<string, T> MapResources<T>(string path) where T : Object
    {
        Dictionary<string, T> dict = new Dictionary<string, T>();

        object[] asset_objects = Resources.LoadAll(path, typeof(T));
        T[] assets = (T[]) asset_objects;

        foreach(T asset in assets)
        {
            dict.Add(asset.name, asset);
        }

        return dict;
    }

    public static Dictionary<string, T> MapResources<T>(string[] names, string path) where T : Object
    {
        Dictionary<string, T> dict = new Dictionary<string, T>();

        T[] assets = Resources.LoadAll<T>(path);
        T default_asset = Resources.Load<T>("default");

        foreach(T asset in assets)
        {
            if(asset.name.Equals("default"))
            {
                default_asset = asset;
                break;
            }
        }

        foreach(string name in names)
        {
            T match = default_asset;

            foreach(T asset in assets)
            {
                if(asset.name.Equals(name))
                {
                    match = asset;
                    break;
                }
            }

            dict.Add(name, match);
        }

        return dict;
    }

    public static T[] CopyArray<T>(T[] arr)
    {
        T[] copy = new T[arr.Length];

        for(int i = 0; i < arr.Length; i++)
        {
            copy[i] = arr[i];
        }

        return copy;
    }

    public static void Swap<T>(T[] arr, int i, int j)
    {
        T temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    public static T[] ShuffleArray<T>(T[] arr)
	{
		T[] copy = CopyArray(arr);

		for(int i = copy.Length; i >= 1; i--)
		{
			int j = Random.Range(0, i);
			Swap(copy, i, j);
		}

        return copy;
	}
}
