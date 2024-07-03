using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    private static System.Random rng = new System.Random();
    public static Vector2 RotateVector(Vector2 vector, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radian);
        float sin = Mathf.Sin(radian);

        float newX = vector.x * cos - vector.y * sin;
        float newY = vector.x * sin + vector.y * cos;

        return new Vector2(newX, newY);
    }

    public static Vector3 ConvertV2ToV3(Vector2 vector)
    {
        // Calculate the angle in radians
        float angleInRadians = Mathf.Atan2(vector.y, vector.x);

        // Optionally convert the angle to degrees
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        // Create the Vector3 with the angle as the z component
        Vector3 result = new Vector3(0f, 0f, angleInDegrees + 90f);

        return result;
    }

    public static List<UpgradeSO> ShuffleUpgrades(List<UpgradeSO> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            UpgradeSO temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }
    public static List<List<UpgradeSO>> ShuffleUpgradeLists(List<List<UpgradeSO>> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            List<UpgradeSO> temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }

    

    public static T RandomItem<T>(this IList<T> list)
    {
        return list[rng.Next(list.Count)];
    }

    public static T RandomItem<T>(this T[] array)
    {
        return array[rng.Next(array.Length)];
    }

}
