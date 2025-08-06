using System;
using System.Collections.Generic;
using Codice.CM.Client.Differences;
using UnityEngine;

namespace RG.Systems
{
    public class GridGenerator : MonoBehaviour
    {
        Tile[,] Tiles;
        Dictionary<(float, float), GameObject> TileObjects;
        public int Size;
        public GameObject TilePrefab;
        public LayerMask GroundLayerMask;
        public float offSetX;
        public float offSetY;
        public float HexRadius = 1;
        [Header("Gizmos")]
        public Color GizmosGridColor;
        private bool gridUpdated = false;

        void Start()
        {
            GenerateTiles();
        }

        private void GenerateTiles()
        {
            InitializeGrid();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    var position = (i, j);
                    Tiles[i, j] = new Tile(HexRadius * 1.5f * i, HexRadius * Mathf.Sqrt(3) * (j + i / 2f));
                    TileObjects[position] = Instantiate(TilePrefab, new Vector3(Tiles[i, j].X, 1, Tiles[i, j].Y), Quaternion.LookRotation(Vector3.up), transform);
                    TileObjects[position].transform.Rotate(new Vector3(0, 0, 30));
                }
            }
            gridUpdated = true;
        }

        private void OnDrawGizmos()
        {
            if (Tiles == null || !gridUpdated) return;
            Gizmos.color = GizmosGridColor;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    try
                    {
                        DrawHex(new Vector3(Tiles[i, j].X, 1, Tiles[i, j].Y));
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        gridUpdated = false;
                        Debug.LogError("Error: " +e+". Grid size has change. Reset to generate grid with new size");
                    }
                    
                }
            }
        }
        [ContextMenu("Reset Grid")]
        private void ResetGrid()
        {
            ClearGrid();
            GenerateTiles();
        }

        private void ClearGrid()
        {
            for (int i = 0; i < Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < Tiles.GetLength(1); j++)
                {
                    Destroy(TileObjects[(i, j)]);
                }
            }
        }

        private void InitializeGrid()
        {
            Tiles = new Tile[Size, Size];
            TileObjects = new Dictionary<(float, float), GameObject>();
        }
        private void DrawHex(Vector3 center)
        {
            Vector3[] corners = new Vector3[6];
            for (int i = 0; i < 6; i++)
            {
                float angleDeg = 60f * i;
                float angleRad = Mathf.Deg2Rad * angleDeg;
                corners[i] = new Vector3(
                    center.x + HexRadius * Mathf.Cos(angleRad),
                    center.y + .5f,
                    center.z + HexRadius * Mathf.Sin(angleRad)
                );
            }

            for (int i = 0; i < 6; i++)
            {
                Vector3 start = corners[i];
                Vector3 end = corners[(i + 1) % 6];
                Gizmos.DrawLine(start, end);
            }
        }
    }
}
