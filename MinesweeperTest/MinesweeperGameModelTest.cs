using System;
using System.Threading.Tasks;
using MineSweeper.Model;
using MineSweeper.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MinesweeperTest
{
    [TestClass]
    public class MinesweeperGameModelTest
    {
        private MinesweeperGameModel _model;
        private MinesweeperTable _mockedTable; 
        private Mock<IMinesweeperDataAccess> _mock;

        [TestInitialize]
        public void Initialize()
        {
            _mockedTable = new MinesweeperTable(10);
            _mockedTable.SetValue(1, 2, 3, false);
            _mockedTable.SetValue(4, 5, 6, true);
            _mockedTable.SetValue(7, 8, 9, false);

            _mock = new Mock<IMinesweeperDataAccess>();
            _mock.Setup(mock => mock.LoadAsync(It.IsAny<String>()))
                .Returns(() => Task.FromResult(_mockedTable));

            _model = new MinesweeperGameModel(_mock.Object);

            _model.GameAdvanced += new EventHandler<MinesweeperEventArgs>(Model_GameAdvanced);
            _model.GameOver += new EventHandler<MinesweeperEventArgs>(Model_GameOver);
        }

        [TestMethod]
        public void MinesweeperGameModelNewGameMediumTest()
        {
            _model.NewGame();

            Assert.AreEqual(Player.Player1, _model.Table.GetPlayer());
            Assert.AreEqual(GameDifficulty.Medium, _model.GameDifficulty);

            Int32 mineFields = 0;
            for (Int32 i = 0; i < 10; i++)
                for (Int32 j = 0; j < 10; j++)
                    if (_model.Table.IsMine(i, j))
                        mineFields++;

            Assert.AreEqual(15, mineFields);
        }

        [TestMethod]
        public void MinesweeperGameModelNewGameEasyTest()
        {
            _model.GameDifficulty = GameDifficulty.Easy;
            _model.NewGame();

            Assert.AreEqual(Player.Player1, _model.Table.GetPlayer());
            Assert.AreEqual(GameDifficulty.Easy, _model.GameDifficulty);

            Int32 mineFields = 0;
            for (Int32 i = 0; i < 6; i++)
                for (Int32 j = 0; j < 6; j++)
                    if (_model.Table.IsMine(i, j))
                        mineFields++;

            Assert.AreEqual(5, mineFields);
        }

        [TestMethod]
        public void MinesweeperGameModelNewGameHardTest()
        {
            _model.GameDifficulty = GameDifficulty.Hard;
            _model.NewGame();

            Assert.AreEqual(Player.Player1, _model.Table.GetPlayer());
            Assert.AreEqual(GameDifficulty.Hard, _model.GameDifficulty);

            Int32 mineFields = 0;
            for (Int32 i = 0; i < 16; i++)
                for (Int32 j = 0; j < 16; j++)
                    if (_model.Table.IsMine(i, j))
                        mineFields++;

            Assert.AreEqual(35, mineFields);
        }

        [TestMethod]
        public void MinesweeperGameModelStepTest()
        {
            _model.NewGame();

            Random random = new Random();
            Int32 x = 0, y = 0;
            do
            {
                x = random.Next(0, 10);
                y = random.Next(0, 10);
            }
            while (_model.Table.IsMine(x, y) || _model.Table[x, y] == 0);

            _model.Step(x, y);

            Assert.AreEqual(Player.Player2, _model.Table.GetPlayer());
            Assert.IsTrue(_model.Table.IsLocked(x,y));

            do
            {
                x = random.Next(1, 9);
                y = random.Next(1, 9);
            }
            while (!(_model.Table[x, y] == 0) );

            _model.Step(x, y);

            Assert.AreEqual(Player.Player1, _model.Table.GetPlayer());
            Assert.IsTrue(_model.Table.IsLocked(x - 1, y - 1));
            Assert.IsTrue(_model.Table.IsLocked(x - 1, y));
            Assert.IsTrue(_model.Table.IsLocked(x - 1, y + 1));
            Assert.IsTrue(_model.Table.IsLocked(x, y - 1));
            Assert.IsTrue(_model.Table.IsLocked(x, y));
            Assert.IsTrue(_model.Table.IsLocked(x, y + 1));
            Assert.IsTrue(_model.Table.IsLocked(x + 1, y - 1));
            Assert.IsTrue(_model.Table.IsLocked(x + 1, y));
            Assert.IsTrue(_model.Table.IsLocked(x + 1, y + 1));
        }

        [TestMethod]
        public async Task MinesweeperGameModelLoadTest()
        {
            _model.NewGame();

            await _model.LoadGameAsync(String.Empty);

            for (Int32 i = 0; i < 3; i++)
                for (Int32 j = 0; j < 3; j++)
                {
                    Assert.AreEqual(_mockedTable.GetValue(i, j), _model.Table.GetValue(i, j));
                    Assert.AreEqual(_mockedTable.IsLocked(i, j), _model.Table.IsLocked(i, j));
                }

            Assert.AreEqual(Player.Player1, _model.Table.GetPlayer());

            _mock.Verify(dataAccess => dataAccess.LoadAsync(String.Empty), Times.Once());
        }

        private void Model_GameAdvanced(Object sender, MinesweeperEventArgs e)
        {
            Assert.IsFalse(e.IsWon);
        }

        private void Model_GameOver(Object sender, MinesweeperEventArgs e)
        {
            Assert.IsTrue(_model.IsGameOver);
            Assert.IsFalse(e.IsWon);
        }
    }
}
