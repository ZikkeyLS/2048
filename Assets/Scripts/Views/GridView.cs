using UnityEngine;
using UnityEngine.UI;

public class GridView : MonoBehaviour
{
    [SerializeField] private float _moveTime = 0.2f;
    [SerializeField] private float _scaleTime = 0.3f;

    public void UpdateUI(BlockModel[,] blocks, Image blockPrefab)
    {
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
            {
                BlockModel block = blocks[x, y];

                if (block.ContainsBlock)
                {
                    if (block.Slot.childCount == 0)
                        Instantiate(blockPrefab, block.Slot);

                    Text text = block.Slot.GetChild(0).GetComponentInChildren<Text>();
                    text.text = block.Value.ToString();

                    if (block.Previous != null)
                    {
                        Transform uiElement = block.Slot.GetChild(0).transform;

                        LeanTween.move(uiElement.gameObject, block.Previous.Slot.position, 0);
                        LeanTween.move(uiElement.gameObject, block.Slot.position, _moveTime);

                        block.SetPreviousBlock(null);
                    }
                }
                else
                {
                    if (block.Slot.childCount == 1)
                        Destroy(block.Slot.GetChild(0).gameObject);
                }
            }
    }

    public void SmoothScale(BlockModel block)
    {
        if (block.Slot.childCount == 0)
            return;

        Transform uiElement = block.Slot.GetChild(0);
        Vector3 oldScale = uiElement.localScale;

        LeanTween.scale(uiElement.gameObject, Vector3.one / 2, 0);
        LeanTween.scale(uiElement.gameObject, oldScale, _scaleTime);
    }
}
