﻿using System;
using System.Windows.Forms;

namespace MyScout
{
    public partial class PrescoutFrm : Form
    {
        private Team selectedTeam;

        public PrescoutFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.Events.Count > 0)
            {
                TeamFrm teamform = new TeamFrm(false);
                teamform.ShowDialog();

                if (teamform.DialogResult == DialogResult.OK)
                {
                    selectedTeam = Program.Events[Program.CurrentEventIndex].teams[teamform.GetSelectedTeamIndex()];
                    button1.Text = selectedTeam.id.ToString() + "\n" + selectedTeam.name;
                    LoadStats(selectedTeam);
                    button2.Enabled = true;
                    button2.Select();
                    AcceptButton = button2;
                }
            }
            else
            {
                Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveStats(selectedTeam);
            Program.Saved = false;
            button2.Enabled = false;
            button1.Select();
            AcceptButton = button1;
        }

        public void LoadStats(Team team)
        {
            var ds = team.GetTeamSpecificDataset();
            canLowGoalCB.Checked = (bool)ds[0].GetValue();
            canHighGoalCB.Checked = (bool)ds[1].GetValue();
            maxCarriedUpDown.Value = Convert.ToInt16(ds[2].GetValue());
            checkBox1.Checked = (bool)ds[3].GetValue();
            checkBox2.Checked = (bool)ds[4].GetValue();
        }

        public void SaveStats(Team team)
        {
            if (team != null)
            {
                team.GetTeamSpecificDataset()[0].SetValue(canLowGoalCB.Checked);
                team.GetTeamSpecificDataset()[1].SetValue(canHighGoalCB.Checked);
                team.GetTeamSpecificDataset()[2].SetValue(maxCarriedUpDown.Value);
                team.GetTeamSpecificDataset()[3].SetValue(checkBox1.Checked);
                team.GetTeamSpecificDataset()[4].SetValue(checkBox2.Checked);
            }
        }

        /// <summary>
        /// Occurs when any checkbox in the form is checked/unchecked.
        /// </summary>
        private void chkbx_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedTeam != null)
            {
                button2.Enabled = true;
                button2.Select();
                AcceptButton = button2;
            }
        }
    }
}
