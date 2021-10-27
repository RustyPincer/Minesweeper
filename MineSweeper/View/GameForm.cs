using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineSweeper.Model;
using MineSweeper.Persistence;

namespace MineSweeper
{
    public partial class GameForm : Form
    {
        #region Fields

        private IMinesweeperDataAccess _dataAccess;
        private MinesweeperGameModel _model;
        private Button[,] _buttonGrid;

        #endregion

        #region Constructors

        public GameForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Form event handlers

        private void GameForm_Load(Object sender, EventArgs e)
        {
            _dataAccess = new MinesweeperFileDataAccess();

            _model = new MinesweeperGameModel(_dataAccess);
            _model.GameAdvanced += new EventHandler<MinesweeperEventArgs>(Game_GameAdvanced);
            _model.GameOver += new EventHandler<MinesweeperEventArgs>(Game_GameOver);

            GenerateTable();
            SetupMenus();


            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();

            _model.NewGame();
            SetupTable();
            toolLabelMines.Text = "Aknák száma: " + _model.NumberOfMines.ToString();
        }

        #endregion

        #region Game event handlers

        private void Game_GameAdvanced(Object sender, MinesweeperEventArgs e)
        {

        }

        private void Game_GameOver(Object sender, MinesweeperEventArgs e)
        {
            timer.Stop();

            foreach (Button button in _buttonGrid)
                button.Enabled = false;

            Reveal();

            menuFileSaveGame.Enabled = false;

            if (e.IsWon)
            {
                MessageBox.Show("Gratulálok, " + e.CurrentTable.GetPlayer().ToString() + " győzött!",
                                "Aknakereső",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Döntetlen.",
                                "Aknakereső",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
            }
        }

        #endregion

        #region Grid event handlers

        private void ButtonGrid_MouseClick(Object sender, MouseEventArgs e)
        {
            Int32 x = ((sender as Button).TabIndex - 100) / _model.Table.Size;
            Int32 y = ((sender as Button).TabIndex - 100) % _model.Table.Size;

            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (_buttonGrid[x, y].Text == "?")
                {
                    _buttonGrid[x, y].Text = "";
                }
                else
                {
                    _buttonGrid[x, y].Text = "?";
                }
            }
            else
            {
                _model.Step(x, y);
                SetupTable();
            }
        }

        #endregion

        #region Menu event handlers

        private void menuFileNewGame_Click(Object sender, EventArgs e)
        {
            menuFileSaveGame.Enabled = true;

            if (!timer.Enabled)
            {
                timer.Start();
            }

            RemoveTable();
            _model.NewGame();
            GenerateTable();
            SetupTable();
            SetupMenus();
            toolLabelMines.Text = "Aknák száma: " + _model.NumberOfMines.ToString();
        }

        private async void MenuFileLoadGame_Click(Object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.LoadGameAsync(openFileDialog.FileName);
                    menuFileSaveGame.Enabled = true;
                }
                catch (MinesweeperDataException)
                {
                    MessageBox.Show("Játék betöltése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a fájlformátum.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _model.NewGame();
                    menuFileSaveGame.Enabled = true;
                }
                RemoveTable();
                GenerateTable();
                SetupTable();
                SetupMenus();
            }
        }

        private async void MenuFileSaveGame_Click(Object sender, EventArgs e)
        {

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.SaveGameAsync(saveFileDialog.FileName);
                }
                catch (MinesweeperDataException)
                {
                    MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Biztosan ki szeretne lépni?", "Aknakereső", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
            else
            {
            }
        }

        private void menuGameEasy_Click(object sender, EventArgs e)
        {
            _model.GameDifficulty = GameDifficulty.Easy;
            SetupMenus();
        }

        private void menuGameMedium_Click(object sender, EventArgs e)
        {
            _model.GameDifficulty = GameDifficulty.Medium;
            SetupMenus();
        }

        private void menuGameHard_Click(object sender, EventArgs e)
        {
            _model.GameDifficulty = GameDifficulty.Hard;
            SetupMenus();
        }

        #endregion

        #region Timer event handlers

        private void Timer_Tick(Object sender, EventArgs e)
        {
            _model.AdvanceTime();
            SetupLabel();
        }

        #endregion

        #region Private methods

        private void RemoveTable()
        {
            for (Int32 i = 0; i < _buttonGrid.GetLength(0); i++)
                for (Int32 j = 0; j < _buttonGrid.GetLength(0); j++)
                {
                    Controls.Remove(_buttonGrid[i, j]);
                }
        }

        private void GenerateTable()
        {
            _buttonGrid = new Button[_model.Table.Size, _model.Table.Size];
            for (Int32 i = 0; i < _model.Table.Size; i++)
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    _buttonGrid[i, j] = new Button();
                    if (_model.GameDifficulty == GameDifficulty.Hard)
                    {
                        _buttonGrid[i, j].Location = new Point(5 + 40 * j, 35 + 40 * i);
                        _buttonGrid[i, j].Size = new Size(40, 40);
                        _buttonGrid[i, j].Font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold);
                    }
                    else if (_model.GameDifficulty == GameDifficulty.Medium)
                    {
                        _buttonGrid[i, j].Location = new Point(5 + 64 * j, 35 + 64 * i);
                        _buttonGrid[i, j].Size = new Size(64, 64);
                        _buttonGrid[i, j].Font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold);
                    }
                    else
                    {
                        _buttonGrid[i, j].Location = new Point(5 + 106 * j, 35 + 106 * i);
                        _buttonGrid[i, j].Size = new Size(106, 106);
                        _buttonGrid[i, j].Font = new Font(FontFamily.GenericSansSerif, 45, FontStyle.Bold);
                    }
                    _buttonGrid[i, j].TabIndex = 100 + i * _model.Table.Size + j;
                    _buttonGrid[i, j].MouseDown += new MouseEventHandler(ButtonGrid_MouseClick);
                    _buttonGrid[i, j].Enabled = true;


                    Controls.Add(_buttonGrid[i, j]);
                }
        }

        private void SetupTable()
        {
            toolLabelPlayer.Text = _model.Table.GetPlayer().ToString();
            
            timerStatusLabel.Text = _model.Table.GetPlayer() == Player.Player1 ?
                                    TimeSpan.FromSeconds(_model.GameTimePlayer1).ToString("g") :
                                    TimeSpan.FromSeconds(_model.GameTimePlayer2).ToString("g");

            for (int i = 0; i < _model.Table.Size; i++)
            {
                for (int j = 0; j < _model.Table.Size; j++)
                {
                    if (_model.Table.IsLocked(i, j))
                    {
                        _buttonGrid[i, j].Enabled = false;
                        if (_model.Table.GetValue(i, j) != 9 && _model.Table.GetValue(i, j) > 0)
                        {
                            _buttonGrid[i, j].Text = _model.Table[i, j].ToString();
                        }
                        else if(_model.Table.GetValue(i, j) == 9)
                        {
                            _buttonGrid[i, j].Text = "X";
                        }
                        else
                        {
                            _buttonGrid[i, j].Text = "";
                        }
                    }
                }
            }
        }

        private void Reveal()
        {
            for (int i = 0; i < _model.Table.Size; i++)
            {
                for (int j = 0; j < _model.Table.Size; j++)
                {
                    if (_model.Table.GetValue(i, j) == 9)
                    {
                         _buttonGrid[i, j].Text = "X";
                    }
                }
            }
        }

        private void SetupLabel()
        {
            toolLabelPlayer.Text = _model.Table.GetPlayer().ToString();
            timerStatusLabel.Text = _model.Table.GetPlayer() == Player.Player1 ?
                                    TimeSpan.FromSeconds(_model.GameTimePlayer1).ToString("g") :
                                    TimeSpan.FromSeconds(_model.GameTimePlayer2).ToString("g");
        }

        private void SetupMenus()
        {
            menuGameEasy.Checked = (_model.GameDifficulty == GameDifficulty.Easy);
            menuGameMedium.Checked = (_model.GameDifficulty == GameDifficulty.Medium);
            menuGameHard.Checked = (_model.GameDifficulty == GameDifficulty.Hard);
        }

        #endregion
    }
}
