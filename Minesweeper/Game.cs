using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Minesweeper
{
    internal class Game
    {
        bool firstClick = true;
        int numberOfMines;
        string difLevel;
        List<Tile> listOfTiles = new List<Tile>();
        List<Tile> listOfMines = new List<Tile>();
        Directions directions = new Directions();
        Stopwatch timer = new Stopwatch();
        public void StartGame(List<Tile> list, string level)
        {
            difLevel = level;
            listOfTiles = list;
            foreach (var item in listOfTiles)
            {
                item.Panel.Enabled = true;
            }
            timer.Start();
        }

        public void ShowMines()
        {
            foreach (Tile tile in listOfTiles)
            {
                tile.ShowMine();
            }
        }

        private void WinCheck()
        {
            int minesFound = 0;
            if (listOfMines.Count(i => i.IsFlag) == numberOfMines)
            {
                timer.Stop();
                TimeSpan timeTaken = timer.Elapsed;
                System.Windows.Forms.MessageBox.Show("You Win! Your Time is : " + timeTaken.ToString(@"m\:ss\.fff"));

                ShowMines();
            }
            Debug.WriteLine(listOfMines.Count(i => i.IsFlag) + " : " + numberOfMines);
        }

        public void CheckNumberOfMinesAround(Tile tile)
        {
            Debug.WriteLine(listOfMines.Count);
            foreach (var item in listOfMines)
            {
                Debug.WriteLine(item.Id);
            }
            if (firstClick)
            {
                SetMines();
                firstClick = false;
                List<Tile> list = listOfTiles;
                list = FindEmpty();
                while(!list.Contains(tile))
                {
                    foreach (var t in listOfTiles)
                    {
                        t.IsMine = false;
                    }
                    SetMines();
                    list = FindEmpty();
                }
            }

            if (tile.IsMine)
            {
                timer.Stop();
                TimeSpan timeTaken = timer.Elapsed;
                System.Windows.Forms.MessageBox.Show("You Lost! Your Time is : " + timeTaken.ToString(@"m\:ss\.fff"));
                ShowMines();
                return;
            }
            CheckMinesAround(tile);
            if (tile.IsEmpty)
            {
                ShowAllEmpty(tile);
            }
        }

        private void ShowAllEmpty(Tile tile)
        {
            List<Tile> tilesToCheck = new List<Tile>();
            tilesToCheck.Clear();
            tilesToCheck = directions.CheckIfEmptyTilesAround(tile, listOfTiles);
            foreach (var item in tilesToCheck)
            {
                List<Tile> aroundTileList = new List<Tile>();
                aroundTileList = DirectionsToCheck(item);
                item.SetNumberOnTile(aroundTileList.Count);
            }
            Debug.Write(tilesToCheck.Count());
        }

        public void SetFlag(Tile tile)
        {
            tile.SetFlag();
            WinCheck();
        }

        private List<Tile> FindEmpty()
        {
            List<Tile> list = new List<Tile>();
            foreach (var item in listOfTiles)
            {
                if (CheckMinesAroundTile(item) == 0 && !item.IsMine)
                {
                    list.Add(item);
                    item.IsEmpty = true;
                }
                else
                {
                    item.IsEmpty = false;
                }
            }
            return list; 
        }

        private void CheckMinesAround(Tile tile)
        {
            List<Tile> aroundTileList = new List<Tile>();
            aroundTileList = DirectionsToCheck(tile);
            tile.SetNumberOnTile(aroundTileList.Count);
        }

        private int CheckMinesAroundTile(Tile tile)
        {
            List<Tile> aroundTileList = new List<Tile>();
            aroundTileList = DirectionsToCheck(tile);
            return aroundTileList.Count;
        }

        private void SetMines()
        {
            listOfMines.Clear();
            if (difLevel == "easy")
            {
                numberOfMines = 6;
            }
            else if (difLevel == "normal")
            {
                numberOfMines = 20;
            }
            else
            {
                numberOfMines = 60;
            }
            for (int i = 0; i < numberOfMines; i++)
            {
                AddMine();
            }
        }
        private void AddMine()
        {
            Random random = new Random();
            int r = random.Next(listOfTiles.Count);

            if (!listOfTiles[r].IsMine)
            {
                listOfTiles[r].IsMine = true;
                listOfMines.Add(listOfTiles[r]);
            }
            else
            {
                AddMine();
            }
        }

        private List<Tile> DirectionsToCheck(Tile tile)
        {
            List<Tile> listToReturn = new List<Tile>();
            listToReturn.Add(UpDir(tile));
            listToReturn.Add(UpLeftDir(tile));
            listToReturn.Add(LeftDir(tile));
            listToReturn.Add(DownLeftDir(tile));
            listToReturn.Add(DownDir(tile));
            listToReturn.Add(DownRightDir(tile));
            listToReturn.Add(RightDir(tile));
            listToReturn.Add(UpRightDir(tile));
            listToReturn.RemoveAll(item => item == null);
            listToReturn.RemoveAll(i => i.IsMine == false);
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
        private Tile UpRightDir(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y + 1 == tile.Y && i.X - 1 == tile.X))
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
        private Tile DownRightDir(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y - 1 == tile.Y && i.X - 1 == tile.X))
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
        private Tile DownLeftDir(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y - 1 == tile.Y && i.X + 1 == tile.X))
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
        private Tile UpLeftDir(Tile tile)
        {
            foreach (var item in listOfTiles.Where(i => i.Y + 1 == tile.Y && i.X + 1 == tile.X))
            {
                return DirReturn(item);
            }
            return null;
        }

        private static Tile DirReturn(Tile item)
        {
            return item;
        }
    }
}
