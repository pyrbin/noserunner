

using UnityEngine;

namespace Unity.Mathematics
{
    public static unsafe class Int2Extensions
    {
        public static Vector2Int AsVector2Int(this int2 me)
        {
            return *(Vector2Int*)&me;
        }

        public static Vector3Int AsVector3Int(this int2 me, int z = 0)
        {
            return new Vector3Int(me.x, me.y, z);
        }

        public static int2 AsInt2(this Vector2Int me)
        {
            return *(int2*)&me;
        }

        public static Vector3Int AsVector3Int(this int3 me)
        {
            return *(Vector3Int*)&me;
        }

        public static int3 AsInt3(this Vector3Int me)
        {
            return *(int3*)&me;
        }
    }
}
