using System.Runtime.CompilerServices;

public static class ByteExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int AddFlag(byte flag, byte mask)
    {
        return (mask | flag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RemoveFlag(byte flag, byte mask)
    {
        return mask & (~flag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FlipFlag(byte flag, byte mask)
    {
        return (mask ^ flag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFlagSet(byte flag, byte mask)
    {
        return (flag & mask) != 0;
    }
}
