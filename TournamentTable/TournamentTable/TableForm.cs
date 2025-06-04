using Model.Core;
using Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TournamentTable
{
    public partial class TableForm : Form
    {
        private DataGridView dataGridView;
        private string _selectedSport;
        private string _selectedSeason;
        private FootballTeam[] _footballCurrentTeams;
        private BasketballTeam[] _basketballCurrentTeams;
        private VoleyballTeam[] _voleyballCurrentTeams;
        public TableForm(string selectedSport, string selectedSeason)
        {
            InitializeComponent();
            InitializeDataGridView();
            _selectedSeason = selectedSeason;
            _selectedSport = selectedSport;

            dataGridView.DataBindingComplete += OnDataBindingComplete;

            LoadData(selectedSport, selectedSeason);
        }

        private void InitializeDataGridView()
        {
            dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false
            };

            this.Controls.Add(dataGridView);
        }
        private void LoadData(string selectedSport, string selectedSeason, string sort = "default")
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();

            dataGridView.Columns.Clear();

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Place",
                HeaderText = "Место",
                DataPropertyName = "Place"
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "Название"
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Points",
                HeaderText = "Очки",
                DataPropertyName = "Points"
            });
            int season = 2026-int.Parse(selectedSeason);
            if (selectedSport == "Футбол")
            {
                var FootballJSONSerializer = new FootballJSONSerializer();
                bool isData = true;
                using (var reader = new StreamReader(FootballJSONSerializer.FilePath))
                    if (reader.ReadLine() == null) isData = false;
                if (isData == false)
                {
                    Football football = new Football(new FootballTournamentGenerator(new FootballTeamsGenerator().Teams).Tournaments);

                    FootballJSONSerializer.SerializeObject(football);
                }

                List<ITournamentStats> lines = new List<ITournamentStats>();
                Football sport = FootballJSONSerializer.DeserializeSport(FootballJSONSerializer.DeserializeData());
                if (sort == "default") sport.Tournaments[season - 1].SortDefault();
                else if (sort == "score") sport.Tournaments[season - 1].SortByScore();
                FootballTeam[] teams = sport.Tournaments[season - 1].Teams;
                _footballCurrentTeams = teams;
                for (int i = 0; i < teams.Length; i++)
                {
                    var team = teams[i];
                    if (team.TournamentsData.ContainsKey(season))
                        lines.Add(team.TournamentsData[season]);
                }
                dataGridView.DataSource = lines;

            }
            else if (selectedSport == "Баскетбол")
            {
                var BasketballJSONSerializer = new BasketballJSONSerializer();
                bool isData = true;
                using (var reader = new StreamReader(BasketballJSONSerializer.FilePath))
                    if (reader.ReadLine() == null) isData = false;
                if (isData == false)
                {
                    Basketball Basketball = new Basketball(new BasketballTournamentGenerator(new BasketballTeamsGenerator().Teams).Tournaments);

                    BasketballJSONSerializer.SerializeObject(Basketball);
                }

                List<ITournamentStats> lines = new List<ITournamentStats>();
                Basketball sport = BasketballJSONSerializer.DeserializeSport(BasketballJSONSerializer.DeserializeData());
                if (sort == "default") sport.Tournaments[season - 1].SortDefault();
                else if (sort == "score") sport.Tournaments[season - 1].SortByScore();
                BasketballTeam[] teams = sport.Tournaments[season - 1].Teams;
                _basketballCurrentTeams = teams;
                for (int i = 0; i < teams.Length; i++)
                {
                    var team = teams[i];
                    if (team.TournamentsData.ContainsKey(season))
                        lines.Add(team.TournamentsData[season]);
                }
                dataGridView.DataSource = lines;

            }
            else if (selectedSport == "Волейбол")
            {
                var VoleyballJSONSerializer = new VoleyballJSONSerializer();
                bool isData = true;
                using (var reader = new StreamReader(VoleyballJSONSerializer.FilePath))
                    if (reader.ReadLine() == null) isData = false;
                if (isData == false)
                {
                    Voleyball Voleyball = new Voleyball(new VoleyballTournamentGenerator(new VoleyballTeamsGenerator().Teams).Tournaments);

                    VoleyballJSONSerializer.SerializeObject(Voleyball);
                }

                List<ITournamentStats> lines = new List<ITournamentStats>();
                Voleyball sport = VoleyballJSONSerializer.DeserializeSport(VoleyballJSONSerializer.DeserializeData());
                if (sort == "default") sport.Tournaments[season - 1].SortDefault();
                else if (sort == "score") sport.Tournaments[season - 1].SortByScore();
                VoleyballTeam[] teams = sport.Tournaments[season - 1].Teams;
                _voleyballCurrentTeams = teams;
                for (int i = 0; i < teams.Length; i++)
                {
                    var team = teams[i];
                    if (team.TournamentsData.ContainsKey(season))
                        lines.Add(team.TournamentsData[season]);
                }
                dataGridView.DataSource = lines;

            }
        }
        private void OnDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (_selectedSport == "Футбол")
            {
                if (_footballCurrentTeams == null) return;

                for (int i = 0; i < _footballCurrentTeams.Length; i++)
                {
                    if (i < dataGridView.Rows.Count)
                    {
                        dataGridView.Rows[i].Cells["Name"].Value = _footballCurrentTeams[i].Name;
                    }
                }
            }
            else if (_selectedSport == "Баскетбол")
            {
                if (_basketballCurrentTeams == null) return;

                for (int i = 0; i < _basketballCurrentTeams.Length; i++)
                {
                    if (i < dataGridView.Rows.Count)
                    {
                        dataGridView.Rows[i].Cells["Name"].Value = _basketballCurrentTeams[i].Name;
                    }
                }
            }
            else if (_selectedSport == "Волейбол")
            {
                if (_voleyballCurrentTeams == null) return;

                for (int i = 0; i < _voleyballCurrentTeams.Length; i++)
                {
                    if (i < dataGridView.Rows.Count)
                    {
                        dataGridView.Rows[i].Cells["Name"].Value = _voleyballCurrentTeams[i].Name;
                    }
                }
            }
        }

        private void dataGridViewTeams_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData(_selectedSport, _selectedSeason, "default");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadData(_selectedSport, _selectedSeason, "score");
        }
    }
}
