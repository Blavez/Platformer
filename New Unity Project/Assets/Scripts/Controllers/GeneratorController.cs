using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlatformerMVC
{

    public class GeneratorController
    {
        private Tilemap _tilemap;
        private Tile _groundTile;
        private int _mapWidth;
        private int _mapHeight;
        private int _factorSmooth;
        private int _fillPercewnt;
        private bool _borders;

        private const int CountWall = 4;

        private int[,] _map;
        public GeneratorController(GeneratorLevelView levelView)
        {
            _tilemap = levelView.Tilemap;
            _groundTile = levelView.GroundTile;
            _mapHeight = levelView.MapHeight;
            _mapWidth = levelView.MapWidth;
            _factorSmooth = levelView.FactorSmooth;
            _fillPercewnt = levelView.FillPercewnt;
            _borders = levelView.Borders;

            _map =new int[_mapWidth, _mapHeight];

        }
        public void Init()
        {
            RandomFillMap();
            for (int i = 0; i < _factorSmooth; i++)
            {
                SmoothMap();
            }


            DrawTiles();
        }

        private void RandomFillMap()
        {
            System.Random rand = new System.Random(Time.deltaTime.ToString().GetHashCode());
            for(int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if(x==0 || x== _mapWidth - 1 || y == 0 || y == _mapHeight - 1)
                    {
                        if (_borders)
                        {
                            _map[x, y] = 1;
                        }
                    }
                    else
                    {
                        _map[x, y] = (rand.Next(0, 100) < _fillPercewnt) ? 1 : 0;
                    }

                }
            }
            //Debug.Log("RandomFillMap")
        }
        private void SmoothMap()
        {
            for (int i = 0; i < _mapWidth; i++)
            {
                for (int j = 0; j < _mapHeight; j++)
                {
                    int neighbourWall = GetWallCount(i, j);

                    if (neighbourWall> CountWall)
                    {
                        _map[i, j] = 1;
                    }
                    else if (neighbourWall < CountWall)
                    {
                        _map[i, j] = 0;
                    }
                }
            }
        }

        private int GetWallCount (int x, int y)
        {
            int wallCount = 0;
            for (int GridX = x-1; GridX <= x+1; GridX++)
            {
                for (int GridY = y-1; GridY <= y+1; GridY++)
                {
                    if (GridX>=0 && GridX<_mapWidth && GridY>=0 && GridY < _mapHeight)
                    {
                        if (GridX!=x || GridY != y)
                        {
                            wallCount += _map[GridX, GridY];
                        }
                    }
                    else
                    {
                        wallCount++;
                    }
                        
                }
            }
            return wallCount;
        }

        private void DrawTiles()
        {
            if (_map == null)
                return;
            for (int i = 0; i < _mapWidth; i++)
            {
                for (int j = 0; j < _mapHeight; j++)
                {
                    var positionTile = new Vector3Int(-_mapWidth / 2 + i, -_mapHeight / 2 + j, 0);
                    if (_map[i, j] == 1)
                    {
                        _tilemap.SetTile(positionTile, _groundTile);
                    }
                }

            }
        }
    }
}
