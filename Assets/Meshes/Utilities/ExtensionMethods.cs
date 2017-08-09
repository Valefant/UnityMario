using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static Vector3 Vector3(float x, float y, float size)
        {
            return new Vector3(x * size, y * size);
        }

        public static Vector3 Vector3(float x, float y, float z, float size)
        {
            return new Vector3(x * size, y * size, z * size);
        }

        public static Vector3 Vector3(float x, float y, float z, float width, float height, float depth)
        {
            return new Vector3(x * width, y * height, z * depth);
        }

        public static Vector2 Vector2(float x, float y, float size)
        {
            return new Vector2(x * size, y * size);
        }
    }
}
