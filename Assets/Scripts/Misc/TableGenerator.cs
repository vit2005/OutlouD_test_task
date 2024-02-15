using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableGenerator
{
    private const float horizontalPadding = 1f;
    private const float verticalPadding = 1f;

    public List<Vector2> GenerateSpawnPoints(int count)
    {
        List<Vector2> points = new List<Vector2>();

        int rows = (int)Mathf.Sqrt(count);
        int columns = count / rows;
        if (rows * columns < count)
        {
            rows -= rows % 2;
            columns = count / rows;
        }

        float totalWidth = (float)columns * horizontalPadding;
        float totalHeight = (float)rows * verticalPadding;

        float startX = -totalWidth / 2f + horizontalPadding / 2f;
        float startY = -totalHeight / 2f + verticalPadding / 2f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float x = startX + col * horizontalPadding;
                float y = startY + row * verticalPadding;
                points.Add(new Vector2(x, y));
            }
        }

        return points;
    }
}
