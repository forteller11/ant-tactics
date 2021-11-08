using System;
using Unity.Mathematics;
using UnityEngine;

namespace Ant.Grid
{
    [Serializable]
    public class Grid
    {
        public int2 Dimensions = new int2(256, 256);

        public int2 GetCellIndexFromScreenPosition(int2 screen, float3 position)
        {
            return GetCellIndexFromScreenPosition(screen, new float2(position.x, position.y));
        }
        ///<remarks>assumes grid covers entire screen</remarks>
        public int2 GetCellIndexFromScreenPosition(int2 screen, float2 position)
        {
            float2 normPoint = math.unlerp(float2.zero, screen, position);
            var index = (int2)(normPoint * Dimensions);
            return index;
        }
        
    }
}