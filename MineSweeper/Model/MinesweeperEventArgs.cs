using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweeper.Persistence;

namespace MineSweeper.Model
{
    public class MinesweeperEventArgs : EventArgs
    {
        private Boolean _isWon;
        private MinesweeperTable _currentTable;
        public Boolean IsWon { get { return _isWon; } }

        public MinesweeperTable CurrentTable { get { return _currentTable; } }

        public MinesweeperEventArgs(Boolean isWon, MinesweeperTable currentTable)
        {
            _isWon = isWon;
            _currentTable = currentTable;
        }
    }
}
