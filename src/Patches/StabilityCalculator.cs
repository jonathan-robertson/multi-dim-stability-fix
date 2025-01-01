using HarmonyLib;
using MultiDimStabilityFix.Utilities;

namespace MultiDimStabilityFix.Patches
{
    [HarmonyPatch(typeof(StabilityCalculator), nameof(StabilityCalculator.GetBlockStabilityIfPlaced))]
    internal class StabilityCalculator_GetBlockStabilityIfPlaced_Patches
    {
        private static readonly ModLog<StabilityCalculator_GetBlockStabilityIfPlaced_Patches> _log = new ModLog<StabilityCalculator_GetBlockStabilityIfPlaced_Patches>();

        public static bool Prefix(Vector3i _pos, BlockValue _bv, ref float __result)
        {
            _log.Info("Prefix");

            var microStopwatch = new MicroStopwatch();
            var block = _bv.Block;
            var num = block.StabilitySupport ? 1 : 0;
            float num2;
            if (block.isMultiBlock)
            {
                var type = _bv.type;
                var rotation = (int)_bv.rotation;
                var vector3i = Vector3i.max;
                var vector3i2 = Vector3i.min;
                for (var i = block.multiBlockPos.Length - 1; i >= 0; i--)
                {
                    var vector3i3 = block.multiBlockPos.Get(i, type, rotation) + _pos;
                    if (!StabilityCalculator.posPlaced.ContainsKey(vector3i3))
                    {
                        StabilityCalculator.posPlaced.Add(vector3i3, num);
                        vector3i = Vector3i.Min(vector3i, vector3i3);
                        vector3i2 = Vector3i.Max(vector3i2, vector3i3);
                    }
                }
                IChunk chunk = null;
                for (var j = vector3i.z; j <= vector3i2.z; j++)
                {
                    Vector3i vector3i4;
                    vector3i4.z = j;
                    for (var k = vector3i.x; k <= vector3i2.x; k++)
                    {
                        vector3i4.x = k;
                        StabilityCalculator.world.GetChunkFromWorldPos(k, j, ref chunk);
                        if (chunk != null && chunk.GetStability(k & 15, vector3i.y - 1, j & 15) == 15)
                        {
                            for (var l = vector3i.y; l <= vector3i2.y; l++)
                            {
                                vector3i4.y = l;
                                StabilityCalculator.posPlaced[vector3i4] = 15;
                            }
                        }
                    }
                }
                num2 = 1f;
                using (var enumerator = StabilityCalculator.posPlaced.Keys.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var vector3i5 = enumerator.Current;
                        if (vector3i5.x == vector3i.x || vector3i5.x == vector3i2.x || vector3i5.y == vector3i.y || vector3i5.y == vector3i2.y || vector3i5.z == vector3i.z || vector3i5.z == vector3i2.z)
                        {
                            var blockStability = StabilityCalculator.GetBlockStability(vector3i5, _bv);
                            if (blockStability < num2)
                            {
                                num2 = blockStability;
                                if (blockStability == 0f)
                                {
                                    break;
                                }
                            }
                            if (microStopwatch.ElapsedMicroseconds > 25000L)
                            {
                                break;
                            }
                        }
                    }
                    goto IL_246;
                }
            }
            StabilityCalculator.posPlaced.Add(_pos, num);
            num2 = StabilityCalculator.GetBlockStability(_pos, _bv);
        IL_246:
            StabilityCalculator.posPlaced.Clear();

            __result = num2;
            return false;
        }
    }
}
