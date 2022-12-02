using System;
using UnityEngine;

[Serializable]
public class BlockModel
{
    public Transform Slot { get; private set; } = null;
    public BlockModel Previous { get; private set; } = null;

    public int Value { get; private set; } = 0;
    public bool ContainsBlock { get; private set; } = false;

    public BlockModel(Transform slot)
    {
        Slot = slot;
    }

    public void SetContainsBlock(bool contains) => ContainsBlock = contains;
    public void SetPreviousBlock(BlockModel previous) => Previous = previous;

    public void SetValue(int value) => Value = value;
    public void MultiplyValue(int by) => Value *= by;
    public void DivideValue(int by) => Value /= by;
}
