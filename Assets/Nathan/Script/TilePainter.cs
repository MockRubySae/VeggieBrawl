using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    public Tile grass1, grass2, grass3, grass4;
    public Tilemap tilemap;
    public Vector3Int position;
    public int choose;
    public Tile paint;

    public Tile farmlandTopLeft, farmlandTopRight, farmlandBottomLeft, farmlandBottomRight, farmlandMiddle, farmlandLeft, farmlandRight, farmlandTop, farmlandBottom;
    void Start()
    {
        for (int x = -250; x < 250; x++)
        {
            for (int y = -250; y < 250; y++)
            {
                position = new Vector3Int(x, y, 0);
                choose = Random.Range(0, 4);
                if (choose == 1)
                {
                    paint = grass1;
                } 
                else if (choose == 2)
                {
                    paint = grass2;
                } 
                else if (choose == 3)
                {
                    paint = grass3;
                }
                else
                {
                    paint = grass4;
                }
                tilemap.SetTile(position, paint);
            }
            
        }

        for (int x = -250; x < 250; x++)
        {
            for (int y = -250; y < 250; y++)
            {
                position = new Vector3Int(x, y, 0);
                if (Random.Range(0,2501) == 2500)
                {
                    PaintFarmland();
                }
            }
        }
    }

    public void PaintFarmland()
    {
        tilemap.SetTile(position, farmlandTopLeft);
        tilemap.SetTile(new Vector3Int(position.x, position.y - 3, 0), farmlandBottomLeft);
        tilemap.SetTile(new Vector3Int(position.x + 6, position.y, 0), farmlandTopRight);
        tilemap.SetTile(new Vector3Int(position.x + 6, position.y - 3, 0), farmlandBottomRight);

        for (int i = 1; i < 3; i++)
        {
            tilemap.SetTile(new Vector3Int(position.x, position.y - i, 0), farmlandLeft);
            tilemap.SetTile(new Vector3Int(position.x + 6, position.y - i, 0), farmlandRight);
        }

        for (int i = 1; i < 6; i++)
        {
            tilemap.SetTile(new Vector3Int(position.x + i, position.y, 0), farmlandTop);
            tilemap.SetTile(new Vector3Int(position.x + i, position.y - 3, 0), farmlandBottom);
        }

        for (int i = 1; i < 6; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                tilemap.SetTile(new Vector3Int(position.x + i, position.y - j, 0), farmlandMiddle);
            }
        }
    }
}
