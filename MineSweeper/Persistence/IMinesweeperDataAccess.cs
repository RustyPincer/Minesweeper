using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweeper.Model;

namespace MineSweeper.Persistence
{
    public interface IMinesweeperDataAccess
    {

        Task<MinesweeperTable> LoadAsync(String path);

        Task SaveAsync(String path, MinesweeperTable table);
    }
}
