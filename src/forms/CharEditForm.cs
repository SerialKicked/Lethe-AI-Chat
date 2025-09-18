﻿using LetheAISharp;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LetheAIChat.Files;

namespace LetheAIChat.src.forms
{
    public partial class CharEditForm : Form
    {
        private Character SelectedCharacter = new();

        public CharEditForm()
        {
            InitializeComponent();
        }

        public void SetupCharacterEditor(string Forceid = "", bool addEvents = true)
        {
            cb_charlist.Items.Clear();
            foreach (var item in DataFiles.Characters)
            {
                cb_charlist.Items.Add(item.Value.UniqueName);
            }
            cb_pointsystems.Items.Clear();
            foreach (var item in DataFiles.Points)
            {
                cb_pointsystems.Items.Add(item.Key);
            }

            var idwant = 0;
            if (Forceid != "")
                idwant = cb_charlist.Items.IndexOf(Forceid);
            if (cb_charlist.Items.Count > 0)
            {
                cb_charlist.SelectedIndex = idwant;
                SelectedCharacter = DataFiles.Characters[cb_charlist.SelectedItem!.ToString()!].Copy<Character>()!;
            }
            if (addEvents)
            {
                cb_charlist.SelectedIndexChanged += (sender, e) =>
                {
                    SelectedCharacter = DataFiles.Characters[cb_charlist.SelectedItem!.ToString()!].Copy<Character>()!;
                    LoadCharacterSettings(SelectedCharacter);
                };

                cb_icon.Items.Clear();
                // load all the png and jpg files in the data/img folder
                var files = Directory.GetFiles("data/img", "*.png");
                files = [.. files, .. Directory.GetFiles("data/img", "*.jpg")];
                foreach (var item in files)
                {
                    cb_icon.Items.Add(Path.GetFileName(item));
                }
            }
            LoadCharacterSettings(SelectedCharacter);
        }

        private Character SaveUIToCharacter()
        {
            var mychar = SelectedCharacter.Copy<Character>()!;
            mychar.Name = ed_name.Text;
            mychar.Bio = ed_bio.Text.ToLinuxFormat();
            mychar.Scenario = ed_scenario.Text.ToLinuxFormat();
            mychar.FirstMessage = [.. ed_firstmessage.Lines];
            mychar.IsUser = ck_isuser.Checked;
            mychar.SystemPrompt = ed_sysprompt.Text.ToLinuxFormat();
            mychar.TTSVoice = ed_outetts.Text;
            mychar.ExampleDialogs = [.. ed_writingstyle.Lines];
            mychar.CanInitiateChat = ck_caninitchat.Checked;
            mychar.SenseOfTime = ck_senseoftime.Checked;
            mychar.SelfEditTokens = (int)num_selfedittokens.Value;
            mychar.SelfEditField = ed_selfedit.Text.ToLinuxFormat();
            mychar.Icon = cb_icon.Text;
            mychar.Plugins = [.. ckl_plugins.CheckedItems.Cast<string>()];
            mychar.Worlds = [.. ckl_worldinfo.CheckedItems.Cast<string>()];
            mychar.AllowedSamplers = [.. ckl_samplers.CheckedItems.Cast<string>()];
            mychar.PointSystem = (cb_pointsystems.SelectedIndex != -1) ? cb_pointsystems.Text : string.Empty;
            mychar.PointValue = (int)num_ptvalue.Value;
            mychar.DatesInSessionSummaries = ck_irldates.Checked;
            return mychar;
        }

        private void LoadCharacterSettings(Character selectedCharacter)
        {
            ed_name.Text = selectedCharacter.Name;
            ed_bio.Text = selectedCharacter.Bio.ToWinFormat();
            ed_scenario.Text = selectedCharacter.Scenario.ToWinFormat();
            ed_firstmessage.Clear();
            ed_firstmessage.Lines = [.. selectedCharacter.FirstMessage];
            ck_isuser.Checked = selectedCharacter.IsUser;
            ed_sysprompt.Text = selectedCharacter.SystemPrompt.ToWinFormat();
            ed_outetts.Text = selectedCharacter.TTSVoice;
            ed_writingstyle.Clear();
            ed_writingstyle.Lines = [.. selectedCharacter.ExampleDialogs];
            ck_caninitchat.Checked = selectedCharacter.CanInitiateChat;
            ck_senseoftime.Checked = selectedCharacter.SenseOfTime;
            num_selfedittokens.Value = selectedCharacter.SelfEditTokens;
            ed_selfedit.Text = selectedCharacter.SelfEditField.ToWinFormat();
            num_ptvalue.Value = selectedCharacter.PointValue;
            ck_irldates.Checked = selectedCharacter.DatesInSessionSummaries;
            ckl_plugins.Items.Clear();
            foreach (var item in LLMEngine.ContextPlugins)
            {
                ckl_plugins.Items.Add(item.PluginID, selectedCharacter.Plugins.Contains(item.PluginID));
            }
            ckl_worldinfo.Items.Clear();
            foreach (var item in DataFiles.WorldInfos)
            {
                ckl_worldinfo.Items.Add(item.Value.UniqueName, selectedCharacter.Worlds.Contains(item.Value.UniqueName));
            }
            ckl_samplers.Items.Clear();
            foreach (var item in DataFiles.Inference)
            {
                ckl_samplers.Items.Add(item.Value.UniqueName, selectedCharacter.AllowedSamplers.Contains(item.Value.UniqueName));
            }
            // set cb_icon item index to the one that matches the selected character icon
            cb_icon.SelectedIndex = cb_icon.Items.IndexOf(selectedCharacter.Icon);

            cb_pointsystems.SelectedIndex = !string.IsNullOrWhiteSpace(selectedCharacter.PointSystem) && DataFiles.Points.ContainsKey(selectedCharacter.PointSystem) ? cb_pointsystems.Items.IndexOf(selectedCharacter.PointSystem) : -1;
        }

        private void bt_worldsave_Click(object sender, EventArgs e)
        {
            var NewName = cb_charlist.Text;
            if (string.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("Please select a valide name for the character");
                return;
            }
            // If name already exists ask for confirmation
            if (DataFiles.Characters.ContainsKey(NewName) && (MessageBox.Show("This character already exists, do you want to overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No))
                return;
            SelectedCharacter = SaveUIToCharacter();
            SelectedCharacter.UniqueName = NewName;
            DataFiles.Characters[NewName] = SelectedCharacter;
            (SelectedCharacter as IFile).SaveToFile("data/chars/" + NewName + ".json");
            SetupCharacterEditor(NewName, false);

            // Update the sampler list in the chat menu
            var currselection = cb_charlist.SelectedItem?.ToString() ?? "";
            cb_charlist.Items.Clear();
            foreach (var item in DataFiles.Characters)
            {
                cb_charlist.Items.Add(item.Value.UniqueName);
            }
            var newidx = cb_charlist.Items.IndexOf(currselection);
            cb_charlist.SelectedIndex = newidx == -1 ? 0 : newidx;
        }

        private void cb_icon_SelectedIndexChanged(object sender, EventArgs e)
        {
            // load the image from the data/img folder into the picture
            if (!File.Exists("data/img/" + cb_icon.Text))
                return;
            var img = Image.FromFile("data/img/" + cb_icon.Text);
            if (img != null)
            {
                pic.Image = img;
            }
        }

        private async void btRegenBio_Click(object sender, EventArgs e)
        {
            if (SelectedCharacter == null)
                return;
            SelectedCharacter.LoadChatHistory();
            if (SelectedCharacter.History.Sessions.Count < 2)
                return;
            if (num_selfedittokens.Value == 0)
            {
                ed_selfedit.Text = string.Empty;
                SelectedCharacter.SelfEditField = string.Empty;
                SelectedCharacter.SelfEditTokens = 0;
            }
            else
            {
                SelectedCharacter.SelfEditTokens = (int)num_selfedittokens.Value;
                await SelectedCharacter.UpdateSelfEditSection();
                ed_selfedit.Text = SelectedCharacter.SelfEditField.ToWinFormat();
            }
        }
    }
}
