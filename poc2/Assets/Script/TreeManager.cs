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

    [SerializeField]
    float minRotation = -10f;
    [SerializeField]
    float maxRotation = 10f;

    public Vector2 sizeVariationRangeF = new Vector2(0.8f, 1.2f);
    public Vector2 sizeVariationRangeG = new Vector2(0.8f, 1.2f);
    public Vector2 sizeVariationRangeB = new Vector2(0.8f, 1.2f);

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
            Vector2 sizeVariationRange = new Vector2(0.8f, 1.2f);
            int sOrder = 0;
            switch (tree.layerG)
            {
                case Tree.layerGround.foreground:
                    colorRangeMax = maxFor;
                    colorRangeMin = minFor;
                    sOrder = forOrder;
                    sizeVariationRange = sizeVariationRangeF;
                    break;
                case Tree.layerGround.background:
                    colorRangeMax = maxBac;
                    colorRangeMin = minBac;
                    sOrder = bacOrder;
                    sizeVariationRange = sizeVariationRangeB;
                    break;
                case Tree.layerGround.ground:
                    colorRangeMax = maxGro;
                    colorRangeMin = minGro;
                    sOrder = groOrder;
                    sizeVariationRange = sizeVariationRangeG;
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
            
            tree.transform.localScale  *= randomScale;

            // Generate a random rotation within the specified range
            float randomRotation = Random.Range(minRotation, maxRotation);

            // Apply the random rotation to the tree
            tree.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);

            if (Random.value > 0.5f) // 50% chance
            {
                // Flip the sprite on the X axis
                Vector2 newTransform = tree.transform.localScale;
                newTransform.x *= -1;
                tree.transform.localScale = newTransform;
            }
        }
    }
}
