using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Persistence
{
    public enum Player { Player1, Player2 }
    public class MinesweeperTable
    {
        #region Fields

        private Int32[,] _fieldValues;
        private Boolean[,] _fieldLocks;
        private Player _player;

        #endregion

        #region Properties

        public Boolean IsFilled
        {
            get
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (_fieldValues[i, j] != 9)
                        {
                            if (_fieldLocks[i, j] == false)
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }

        public Boolean MineStep
        {
            get
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (_fieldValues[i, j] == 9)
                        {
                            if (_fieldLocks[i, j] == true)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
        }

        public Int32 this[Int32 x, Int32 y] { get { return GetValue(x, y); } }

        public Int32 Size { get { return _fieldValues.GetLength(0); } }

        #endregion

        #region Constructor

        public MinesweeperTable(Int32 tableSize)
        {

            if (tableSize < 0)
                throw new ArgumentOutOfRangeException("The table size is less than 0.", "tableSize");

            _fieldValues = new Int32[tableSize, tableSize];
            _fieldLocks = new Boolean[tableSize, tableSize];

            for (int i = 0; i < tableSize; i++)
            {
                for (int j = 0; j < tableSize; j++)
                {
                    _fieldValues[i, j] = 0;
                }
            }
        }

        #endregion

        #region Public methods

        public Player GetPlayer()
        {
            return _player;
        }

        public void SetPlayer(Player player)
        {
            _player = player;
        }

        public Boolean IsMine(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y] == 9;
        }

        public Boolean IsLocked(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldLocks[x, y];
        }

        public Int32 GetValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y];
        }

        public void SetLock(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            _fieldLocks[x, y] = true;

        }

        public void SetValue(Int32 x, Int32 y, Int32 value, Boolean lockField)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            if (_fieldLocks[x, y])
                return;

            _fieldValues[x, y] = value;
            _fieldLocks[x, y] = lockField;
        }

        #endregion
    }
}
