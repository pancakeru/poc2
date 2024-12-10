using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    Tree[] trees;
    [SerializeField] Sprite[] treeSprites;
    [SerializeField] Color maxFor;
    [SerializeField] Color minFor;
    [SerializeField] Color minGro;
    [SerializeField] Color maxGro;
    [SerializeField] Color minBac;
    [SerializeField] Color maxBac;

    public Vector2 sizeVariationRange = new Vector2(0.8f, 1.2f);

    [SerializeField] int forOrder, bacOrder, groOrder;
    // Start is called before the first frame update
    void Start()
    {
        trees = FindObjectsOfType<Tree>();
        AssignTreeProperties();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AssignTreeProperties()
    {

        foreach (Tree tree in trees)
        {
            // Assign random sprite

            if (treeSprites.Length > 0)
            {
                tree.spr.sprite = treeSprites[Random.Range(0, treeSprites.Length)];
            }
            Color colorRangeMin = Color.black;
            Color colorRangeMax = Color.black;

            int sOrder = 0;
            switch (tree.layerG)
            {
                case Tree.layerGround.foreground:
                    colorRangeMax = maxFor;
                    colorRangeMin = minFor;
                    sOrder = forOrder;
                    break;
                case Tree.layerGround.background:
                    colorRangeMax = maxBac;
                    colorRangeMin = minBac;
                    sOrder = bacOrder;
                    break;
                case Tree.layerGround.ground:
                    colorRangeMax = maxGro;
                    colorRangeMin = minGro;
                    sOrder = groOrder;
                    break;
            }

            tree.spr.color = new Color(
                Random.Range(colorRangeMin.r, colorRangeMax.r),
                Random.Range(colorRangeMin.g, colorRangeMax.g),
                Random.Range(colorRangeMin.b, colorRangeMax.b)
            );

            // Set sorting order
            tree.spr.sortingOrder = sOrder;

            // Apply size variation
            float randomScale = Random.Range(sizeVariationRange.x, sizeVariationRange.y);
            tree.transform.localScale = Vector3.one * randomScale;
            if (Random.value > 0.5f) // 50% chance
            {
                // Flip the sprite on the X axis
                tree.spr.flipX = true;
            }
        }
    }
}
