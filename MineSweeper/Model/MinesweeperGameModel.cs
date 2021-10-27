using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweeper.Persistence;

namespace MineSweeper.Model
{
    public enum GameDifficulty { Easy, Medium, Hard }
    public class MinesweeperGameModel
    {
        #region Difficulty constans

        private const Int32 TableSizeEasy = 6;
        private const Int32 TableSizeMedium = 10;
        private const Int32 TableSizeHard = 16;
        private const Int32 MineCountEasy = 9;
        private const Int32 MineCountMedium = 20;
        private const Int32 MineCountHard = 40;

        #endregion

        #region Fields

        private IMinesweeperDataAccess _dataAccess;
        private MinesweeperTable _table;
        private GameDifficulty _gameDifficulty;
        private Int32 gameTimePlayer1;
        private Int32 gameTimePlayer2;

        #endregion

        #region Properties
        public MinesweeperTable Table { get { return _table; } }

        public Boolean IsGameOver { get { return (_table.MineStep || _table.IsFilled); } }

        public Int32 GameTimePlayer1 { get { return gameTimePlayer1; } }

        public Int32 GameTimePlayer2 { get { return gameTimePlayer2; } }

        public GameDifficulty GameDifficulty { get { return _gameDifficulty; } set { _gameDifficulty = value; } }

        public Int32 NumberOfMines { 
            get 
            {
                if(GameDifficulty == GameDifficulty.Easy)
                {
                    return MineCountEasy;
                }
                else if (GameDifficulty == GameDifficulty.Medium)
                {
                    return MineCountMedium;
                }
                else
                {
                    return MineCountHard;
                }
            } 
        }

        #endregion

        #region Events

        public event EventHandler<MinesweeperEventArgs> GameAdvanced;

        public event EventHandler<MinesweeperEventArgs> GameOver;

        #endregion

        #region Constructor
        public MinesweeperGameModel(IMinesweeperDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _table = new MinesweeperTable(TableSizeMedium);
            GenerateFields(MineCountMedium);
            _gameDifficulty = GameDifficulty.Medium;
            _table.SetPlayer(Player.Player1);
        }

        #endregion

        #region Public game methods

        public void NewGame()
        {
            _table.SetPlayer(Player.Player1);
            gameTimePlayer1 = 180;
            gameTimePlayer2 = 180;
            switch (_gameDifficulty)
            {
                case GameDifficulty.Easy:
                    _table = new MinesweeperTable(TableSizeEasy);
                    GenerateFields(MineCountEasy);
                    break;
                case GameDifficulty.Medium:
                    _table = new MinesweeperTable(TableSizeMedium);
                    GenerateFields(MineCountMedium);
                    break;
                case GameDifficulty.Hard:
                    _table = new MinesweeperTable(TableSizeHard);
                    GenerateFields(MineCountHard);
                    break;
            }
        }

        public void AdvanceTime()
        {
            if (IsGameOver)
                return;

            if(_table.GetPlayer() == Player.Player1)
            {
                gameTimePlayer1--;
            }
            else
            {
                gameTimePlayer2--;
            }
            
            OnGameAdvanced();

            if (gameTimePlayer1 == 0 || gameTimePlayer2 == 0)
            {
                if (_table.GetPlayer() == Player.Player1)
                {
                    _table.SetPlayer(Player.Player2);
                }
                else
                {
                    _table.SetPlayer(Player.Player1);
                }
                OnGameOver(true);
            }
        }

        public void Step(Int32 x, Int32 y)
        {
            if(_table.GetPlayer() == Player.Player1)
            {
                _table.SetPlayer(Player.Player2);
            }
            else
            {
                _table.SetPlayer(Player.Player1);
            }

            if (Table.IsMine(x, y))
            {
                LockField(x, y);
            }
            else if (Table[x, y] == 0)
            {
                LockNeighbor(x, y);
            }
            else
            {
                LockField(x, y);
            }

            OnGameAdvanced();

            if (_table.MineStep)
            {
                OnGameOver(true);
            }
            else if (_table.IsFilled)
            {
                OnGameOver(false);
            }
        }

        public async Task LoadGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            _table = await _dataAccess.LoadAsync(path);
            if(_table.Size == TableSizeEasy)
            {
                GameDifficulty = GameDifficulty.Easy;
            }
            else if (_table.Size == TableSizeMedium)
            {
                GameDifficulty = GameDifficulty.Medium;
            }
            else
            {
                GameDifficulty = GameDifficulty.Hard;
            }
        }

        public async Task SaveGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await _dataAccess.SaveAsync(path, _table);
        }

        public void LockField(Int32 x, Int32 y)
        {
            _table.SetLock(x, y);
        }

        #endregion

        #region Private game methods

        private void GenerateFields(Int32 count)
        {
            Random random = new Random();

            for (Int32 i = 0; i < count; i++)
            {
                Int32 x, y;

                do
                {
                    x = random.Next(_table.Size);
                    y = random.Next(_table.Size);
                }
                while (_table.IsMine(x, y));

                _table.SetValue(x, y, 9, false);

                for (int l = x - 1; l <= x + 1; l++)
                {
                    for (int c = y - 1; c <= y + 1; c++)
                    {
                        if (!((l < 0 || l > _table.Size - 1) || (c < 0 || c > _table.Size - 1) || _table.IsMine(l, c)))
                        {
                            _table.SetValue(l, c, _table.GetValue(l, c) + 1, false);
                        }
                    }
                }
            }
        }

        private void LockNeighbor(Int32 x, Int32 y)
        {
            LockField(x, y);
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < Table.Size && j >= 0 && j < Table.Size)
                    {
                        if (!Table.IsLocked(i,j))
                        {
                            if (Table[i, j] == 0)
                            {
                                LockNeighbor(i, j);
                            }
                            else
                            {
                                LockField(i, j);
                            }
                        }

                    }
                }
            }
        }

        #endregion

        #region Private event methods

        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
                GameAdvanced(this, new MinesweeperEventArgs(false, _table));
        }

        private void OnGameOver(Boolean isWon)
        {
            if (GameOver != null)
                GameOver(this, new MinesweeperEventArgs(isWon, _table));
        }

        #endregion  
    }
}
