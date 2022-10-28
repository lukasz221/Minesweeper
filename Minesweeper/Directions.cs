using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Minesweeper
{
    internal class Directions
    {
        List<Tile> listOfTiles;
        static List<Tile> visitedTiles;
        static List<Tile> activeTiles;
        static List<Tile> toBack;
 
        public List<Tile> CheckIfEmptyTilesAround(Tile tile, List<Tile> listOfTiles)
        {
            listOfTiles = listOfTiles.ToList();
            visitedTiles = new List<Tile>();
            activeTiles = new List<Tile>();
            toBack = new List<Tile>();

            this.listOfTiles = listOfTiles;
            activeTiles.Add(tile);
            while(activeTiles.Any())
            {
                Tile current = activeTiles.First();
                activeTiles.AddRange(DirectionsCheckForEmptyTiles(current));
                activeTiles.Remove(current);
                visitedTiles.Add(current);
                if (current.IsEmpty)
                {
                    bool alreadyExist = toBack.Contains(current);
                    if (!alreadyExist)
                    {
                        toBack.Add(current);
                    }
                }
            }
            toBack.AddRange(ddd());
            toBack.Distinct().ToList();
            return toBack;
            Debug.WriteLine("toBack: " + toBack.Count());
        }

        private List<Tile> ddd()
        {
            List<Tile> temp = new List<Tile>();
            foreach (var item in toBack)
            {
                temp.AddRange( DirectionsCheckForBorder(item));
            }
            temp = temp.Distinct().ToList();
            return temp;
        }

        public List<Tile> DirectionsCheckForEmptyTiles(Tile tile)
        {
            List<Tile> listToReturn = new List<Tile>();
            listToReturn.Add(UpDir(tile));
            listToReturn.Add(LeftDir(tile));
            listToReturn.Add(DownDir(tile));
            listToReturn.Add(RightDir(tile));
            listToReturn.RemoveAll(item => item == null);
            return listToReturn;
        }

        public List<Tile> DirectionsCheckForBorder(Tile tile)
        {
            List<Tile> listToReturn = new List<Tile>();
            listToReturn.Add(UpDirBorder(tile));
            listToReturn.Add(UpLeftDirBorder(tile));
            listToReturn.Add(LeftDirBorder(tile));
            listToReturn.Add(DownLeftDirBorder(tile));
            listToReturn.Add(DownDirBorder(tile));
            listToReturn.Add(DownRightDirBorder(tile));
            listToReturn.Add(RightDirBorder(tile));
            listToReturn.Add(UpRightDirBorder(tile));
            listToReturn.RemoveAll(item => item == null);
            return listToReturn;
        }

        private Tile UpDir(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y + 1 == tile.Y && i.X == tile.X))
            {
                return DirReturn(item);
            }
            return null;
        }
        private Tile RightDir(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y == tile.Y && i.X - 1 == tile.X))
            {
                return DirReturn(item);
            }
            return null;
        }
        private Tile DownDir(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y - 1 == tile.Y && i.X == tile.X))
            {
                return DirReturn(item);
            }
            return null;
        }
        private Tile LeftDir(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y == tile.Y && i.X + 1 == tile.X))
            {
                return DirReturn(item);
            }
            return null;
        }
        private static Tile DirReturn(Tile item)
        {
            if (item.IsEmpty && !visitedTiles.Contains(item) && !item.IsMine)
            {
                return item;
            }
            else
            {
                return null;
            }
        }

        private Tile UpDirBorder(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y + 1 == tile.Y && i.X == tile.X))
            {
                return DirReturnBorder(item);
            }
            return null;
        }
        private Tile UpRightDirBorder(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y + 1 == tile.Y && i.X - 1 == tile.X))
            {
                return DirReturnBorder(item);
            }
            return null;
        }
        private Tile RightDirBorder(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y == tile.Y && i.X - 1 == tile.X))
            {
                return DirReturnBorder(item);
            }
            return null;
        }
        private Tile DownRightDirBorder(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y - 1 == tile.Y && i.X - 1 == tile.X))
            {
                return DirReturnBorder(item);
            }
            return null;
        }
        private Tile DownDirBorder(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y - 1 == tile.Y && i.X == tile.X))
            {
                return DirReturnBorder(item);
            }
            return null;
        }
        private Tile DownLeftDirBorder(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y - 1 == tile.Y && i.X + 1 == tile.X))
            {
                return DirReturnBorder(item);
            }
            return null;
        }
        private Tile LeftDirBorder(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y == tile.Y && i.X + 1 == tile.X))
            {
                return DirReturnBorder(item);
            }
            return null;
        }
        private Tile UpLeftDirBorder(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y + 1 == tile.Y && i.X + 1 == tile.X))
            {
                return DirReturnBorder(item);
            }
            return null;
        }
        private static Tile DirReturnBorder(Tile item)
        {
            if (!toBack.Contains(item))
            {
                return item;
            }
            else
            {
                return null;
            }
        }
    }
}