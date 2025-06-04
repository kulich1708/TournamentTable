using Model.Core;
using Model.Data;

namespace TournamentTable
{
    public partial class TournamentTable : Form
    {
        public TournamentTable()
        {
            InitializeComponent();
        }

        private void comboBoxSport_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckComboBoxes();

        }

        private void comboBoxSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckComboBoxes();

        }

        private void buttonShowTable_Click(object sender, EventArgs e)
        {
        }

        private void CheckComboBoxes()
        {
            buttonShowTable.Enabled = comboBoxSport.SelectedIndex != -1 && comboBoxSeason.SelectedIndex != -1;
        }

        private void buttonShowTable_Click_1(object sender, EventArgs e)
        {

            string selectedSport = comboBoxSport.SelectedItem?.ToString();
            string selectedSeason = comboBoxSeason.SelectedItem?.ToString();
            TableForm form2 = new TableForm(selectedSport, selectedSeason);
            form2.Show();
            //this.Hide();
        }
    }
}
