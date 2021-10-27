using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MineSweeper.Model;

namespace MineSweeper.Persistence
{
    class MinesweeperFileDataAccess : IMinesweeperDataAccess
    {
        public async Task<MinesweeperTable> LoadAsync(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line = await reader.ReadLineAsync();
                    String[] numbers = line.Split(' ');
                    Int32 tableSize = Int32.Parse(numbers[0]);
  
                    MinesweeperTable table = new MinesweeperTable(tableSize);
                    if (numbers[1] == "Player1")
                    {
                        table.SetPlayer(Player.Player1);
                    }
                    else
                    {
                        table.SetPlayer(Player.Player2);
                    }

                    for (Int32 i = 0; i < tableSize; i++)
                    {
                        line = await reader.ReadLineAsync();
                        numbers = line.Split(' ');

                        for (Int32 j = 0; j < tableSize; j++)
                        {
                            table.SetValue(i, j, Int32.Parse(numbers[j]), false);
                        }
                    }

                    for (Int32 i = 0; i < tableSize; i++)
                    {
                        line = await reader.ReadLineAsync();
                        String[] locks = line.Split(' ');

                        for (Int32 j = 0; j < tableSize; j++)
                        {
                            if (locks[j] == "1")
                            {
                                table.SetLock(i, j);
                            }
                        }
                    }

                    return table;
                }
            }
            catch
            {
                throw new MinesweeperDataException();
            }
        }

        public async Task SaveAsync(String path, MinesweeperTable table)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    await writer.WriteLineAsync(table.Size.ToString() + " " + table.GetPlayer().ToString());
                    for (Int32 i = 0; i < table.Size; i++)
                    {
                        for (Int32 j = 0; j < table.Size; j++)
                        {
                            await writer.WriteAsync(table[i, j] + " ");
                        }
                        await writer.WriteLineAsync();
                    }

                    for (Int32 i = 0; i < table.Size; i++)
                    {
                        for (Int32 j = 0; j < table.Size; j++)
                        {
                            await writer.WriteAsync((table.IsLocked(i, j) ? "1" : "0") + " ");
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new MinesweeperDataException();
            }
        }
    }
}
