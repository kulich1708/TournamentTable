namespace TournamentTable
{
    partial class TournamentTable
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TournamentTable));
            buttonShowTable = new Button();
            comboBoxSport = new ComboBox();
            comboBoxSeason = new ComboBox();
            SuspendLayout();
            // 
            // buttonShowTable
            // 
            resources.ApplyResources(buttonShowTable, "buttonShowTable");
            buttonShowTable.Name = "buttonShowTable";
            buttonShowTable.UseVisualStyleBackColor = true;
            buttonShowTable.Click += buttonShowTable_Click_1;
            // 
            // comboBoxSport
            // 
            resources.ApplyResources(comboBoxSport, "comboBoxSport");
            comboBoxSport.FormattingEnabled = true;
            comboBoxSport.Items.AddRange(new object[] { resources.GetString("comboBoxSport.Items"), resources.GetString("comboBoxSport.Items1"), resources.GetString("comboBoxSport.Items2") });
            comboBoxSport.Name = "comboBoxSport";
            comboBoxSport.SelectedIndexChanged += comboBoxSport_SelectedIndexChanged;
            // 
            // comboBoxSeason
            // 
            resources.ApplyResources(comboBoxSeason, "comboBoxSeason");
            comboBoxSeason.FormattingEnabled = true;
            comboBoxSeason.Items.AddRange(new object[] { resources.GetString("comboBoxSeason.Items"), resources.GetString("comboBoxSeason.Items1"), resources.GetString("comboBoxSeason.Items2") });
            comboBoxSeason.Name = "comboBoxSeason";
            comboBoxSeason.SelectedIndexChanged += comboBoxSeason_SelectedIndexChanged;
            // 
            // TournamentTable
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 45, 45);
            Controls.Add(comboBoxSeason);
            Controls.Add(comboBoxSport);
            Controls.Add(buttonShowTable);
            Name = "TournamentTable";
            ResumeLayout(false);
        }

        #endregion
        private Button buttonShowTable;
        private ComboBox comboBoxSport;
        private ComboBox comboBoxSeason;
    }
}
