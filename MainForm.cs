/*  Windows 10 Privacy Utility
 *  Authors: Hailey Ferguson & Kyle Groleau
 *  Contact: kyle.groleau@outlook.com
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management.Automation;
using System.Text.RegularExpressions;
using System.Security.Principal;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Diagnostics;
using System.ServiceProcess;


namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        PowerShell powerShell = PowerShell.Create();
        List<string> hiddenAppsList = new List<string>();
        List<string> hiddenPAppsList = new List<string>();
        bool isAdmin = false;

        List<int> checkBoxList = new List<int>();
        List<string> removeAppsList = new List<string>();
        List<string> removeProvisionedList = new List<string>();
        Config xmlConfig = new Config();

        static WindowsIdentity ident = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new WindowsPrincipal(ident);

        bool runOncePrivCheck = true;

        public MainForm()
        {
            InitializeComponent();
            UpdateHiddenList();
            CheckAdmin();

            for (int i = 0; i < 32; i++) checkBoxList.Add(0);
        }

        private void CheckAdmin()
        {
            isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!isAdmin)
            {
                foreach (Control o in ProvisionedApps.Controls) { o.Enabled = false; o.Visible = false; }
                ProvAdminWarn.Visible = true;
            }
        }

        //----------------------------------------------NON-PROVISIONED APPS-----------------------------------------------------------
        private void RefreshAppList(object sender, EventArgs e)
        {
            ListOfAppsInstalled.Items.Clear();
            powerShell.Commands.Clear();

            powerShell.AddCommand("get-appxpackage");
            powerShell.AddCommand("Select").AddParameter("property", "name");

            foreach (PSObject result in powerShell.Invoke())
            {
                string current = result.ToString();
                if (hiddenAppsList != null) if ((hiddenAppsList.Any(current.Contains)) && !ShowHiddenApps.Checked) continue;
                if (ListOfAppsInstalled.Items.Contains(Regex.Replace(current, "(@{Name=)|(})", ""))) continue;
                ListOfAppsInstalled.Items.Add(Regex.Replace(current, "(@{Name=)|(})", ""));
            }

            string appInst = ListOfAppsInstalled.Items.ToString();
            foreach (string item in AppsToRemove.Items) if (item.Any(appInst.Contains)) ListOfAppsInstalled.Items.Remove(item);

            UpdInstallCount();
        }

        private void UninstallApps(object sender, EventArgs e)     //Uninstall apps in appsToRemove
        {
            if (AppsToRemove.Items.Count == 0) { MessageBox.Show("No apps to uninstall"); }
            else
            {
                Enabled = false;
                MessageBox.Show("Uninstall will be attempted after pressing OK.\nThe application will become active when uninstall is complete.\n(This can take a while)");
                MessageBox.Show(RunAppUninstaller());

                AppsToRemove.Items.Clear();
                RefreshAppList(null, null);
                Enabled = true;
            }
        }

        string RunAppUninstaller()
        {
            string success = "Successfully removed:\n";
            string failed = "Failed to remove:\n";

            // TODO: This definitely doesn't need to be a new powershell invocation for each package
            foreach (var item in AppsToRemove.Items)
            {
                powerShell.Commands.Clear();
                powerShell.AddCommand("Get-AppxPackage");
                powerShell.AddArgument(item.ToString());
                powerShell.AddCommand("remove-appxpackage");

                List<PSObject> psOut = new List<PSObject>(powerShell.Invoke());

                foreach (var p in powerShell.Streams.Progress)
                {
                    if (p.Activity.Contains(item.ToString()) && p.StatusDescription == "Completed")     //APP REMOVED
                    {
                        success += "\t" + item.ToString() + "\n";
                        break;
                    }
                    else if (p.Activity.Contains(item.ToString()) && p.StatusDescription == "Error")    //APP NOT REMOVED
                    {
                        if (!failed.Contains(item.ToString())) failed += "\t" + item.ToString() + "\n";
                    }
                }
                powerShell.Streams.Progress.Clear();
            }

            string outputPS = "";
            if (powerShell.HadErrors) { outputPS = success + "\n" + failed; powerShell.Streams.Error.Clear(); }
            else { outputPS = success; }

            return outputPS;

        }

        private void btnMvRight_Click(object sender, EventArgs e)
        {
            if (ListOfAppsInstalled.Items.Count != 0)
            {
                if (ListOfAppsInstalled.SelectedItem == null) ListOfAppsInstalled.SelectedIndex = 0;
                while (ListOfAppsInstalled.SelectedItem != null)
                {
                    AppsToRemove.Items.Add(ListOfAppsInstalled.SelectedItem);
                    ListOfAppsInstalled.Items.Remove(ListOfAppsInstalled.SelectedItem);
                }
                UpdInstallCount();
            }
        }

        private void btnMvLeft_Click(object sender, EventArgs e)
        {
            if (AppsToRemove.Items.Count != 0)
            {
                if (AppsToRemove.SelectedItem == null) AppsToRemove.SelectedIndex = 0;
                while (AppsToRemove.SelectedItem != null)
                {
                    ListOfAppsInstalled.Items.Add(AppsToRemove.SelectedItem);
                    AppsToRemove.Items.Remove(AppsToRemove.SelectedItem);
                }
                UpdInstallCount();
            }
        }

        private void btnMvAllRight_Click(object sender, EventArgs e)
        {
            foreach (var item in ListOfAppsInstalled.Items)
            {
                AppsToRemove.Items.Add(item);
            }
            ListOfAppsInstalled.Items.Clear();
            UpdInstallCount();
        }

        private void UpdInstallCount()
        {
            int appsinst = ListOfAppsInstalled.Items.Count;
            int appsrem = AppsToRemove.Items.Count;
            lblInstalledCount.Text = "Apps to keep [" + appsinst.ToString() + "]";
            lblRemoveCount.Text = "Apps to remove [" + appsrem.ToString() + "]";
            btnMvAllRight.Enabled = btnMvRight.Enabled = (appsinst != 0);
            btnMvLeft.Enabled = btnUninstall.Enabled = btnClearAppsRem.Enabled = (appsrem != 0);
        }

        private void ShowHiddenApps_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowHiddenApps.Checked) MessageBox.Show("USING THIS CAN CAUSE IRREPARABLE DAMAGE TO YOUR OS");
            RefreshAppList(sender, e);
        }

        private void UpdateHiddenList()
        {
            System.IO.StreamReader fileStream = null;

            try
            {
                fileStream = System.IO.File.OpenText("hiddenapps.txt");

                if (fileStream.Peek() > 0)
                {
                    string buff;
                    while ((buff = fileStream.ReadLine()) != null) hiddenAppsList.Add(buff);
                };
                fileStream.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                foreach (string line in EmbResources.hiddenapps.Split('\n')) hiddenAppsList.Add(line.TrimEnd(new[] { '\n', '\r', ' ' }));
            }

            try
            {
                fileStream = System.IO.File.OpenText("hiddenprovisionedapps.txt");

                if (fileStream.Peek() > 0)
                {
                    string buff;
                    while ((buff = fileStream.ReadLine()) != null) hiddenPAppsList.Add(buff);
                };
                fileStream.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                foreach (string line in EmbResources.hiddenpapps.Split('\n')) hiddenPAppsList.Add(line.TrimEnd(new[] { '\n', '\r', ' ' }));
            }
        }

        private void Apps_Enter(object sender, EventArgs e)
        {
            //ActiveForm.Enabled = false;
            LoadingApps loading = new LoadingApps();
            loading.Show();
            RefreshAppList(sender, e);
            loading.Dispose();
            //this.Enabled = true;
        }

        private void btnClearAppsRem_Click(object sender, EventArgs e)
        {
            AppsToRemove.Items.Clear();
            RefreshAppList(sender, e);
        }

        //----------------------------------------------PROVISIONED APPS---------------------------------------------------------------
        private void RefreshProvisionedApps(object sender, EventArgs e)
        {
            PListOfAppsInstalled.Items.Clear();
            powerShell.Commands.Clear();

            powerShell.AddCommand("get-appxProvisionedpackage").AddParameter("online");     //Collect list
            powerShell.AddCommand("Select").AddParameter("property", "displayname");        //Filter by name


            // TODO: This definitely doesn't need to be a new powershell instance for each removal
            foreach (PSObject result in powerShell.Invoke())                                //Run the command
            {
                string current = result.ToString();
                if (hiddenPAppsList != null) if ((hiddenPAppsList.Any(current.Contains)) && !PShowHiddenApps.Checked) continue; //Don't add this item if it's part of the hidden list (unless override)
                if (PListOfAppsInstalled.Items.Contains(Regex.Replace(current, "(@{DisplayName=)|(})", ""))) continue;          //Don't add this item if it already exists 
                PListOfAppsInstalled.Items.Add(Regex.Replace(current, "(@{DisplayName=)|(})", ""));                             //Add the item
            }
            string appInst = PListOfAppsInstalled.Items.ToString();
            foreach (string item in PAppsToRemove.Items) if (item.Any(appInst.Contains)) PListOfAppsInstalled.Items.Remove(item);   //if any of the items just collected exist in the apps to remove, remove them from the apps installed list

            UpdateProvisionedAppCount();                                                                                                 //Update the counters
        }

        private void ClearAllProvisionedApps(object sender, EventArgs e)
        {
            PAppsToRemove.Items.Clear();
            RefreshProvisionedApps(sender, e);
        }

        private void AddProvisionedAppItem(object sender, EventArgs e)
        {
            if (PListOfAppsInstalled.Items.Count != 0)
            {
                if (PListOfAppsInstalled.SelectedItem == null) PListOfAppsInstalled.SelectedIndex = 0;
                while (PListOfAppsInstalled.SelectedItem != null)
                {
                    PAppsToRemove.Items.Add(PListOfAppsInstalled.SelectedItem);
                    PListOfAppsInstalled.Items.Remove(PListOfAppsInstalled.SelectedItem);
                }
                UpdateProvisionedAppCount();
            }
        }

        private void ClearProvisionedAppItem(object sender, EventArgs e)
        {
            if (PAppsToRemove.Items.Count != 0)
            {
                if (PAppsToRemove.SelectedItem == null) PAppsToRemove.SelectedIndex = 0;
                while (PAppsToRemove.SelectedItem != null)
                {
                    PListOfAppsInstalled.Items.Add(PAppsToRemove.SelectedItem);
                    PAppsToRemove.Items.Remove(PAppsToRemove.SelectedItem);
                }
                UpdateProvisionedAppCount();
            }
        }

        private void AddAllProvisionedAppItem(object sender, EventArgs e)
        {
            foreach (var item in PListOfAppsInstalled.Items) PAppsToRemove.Items.Add(item);
            PListOfAppsInstalled.Items.Clear();
            UpdateProvisionedAppCount();
        }

        private void ProvisionedUninstallClick(object sender, EventArgs e)                            // TODO: EXCEPTION THROWN WHEN REMOVING 3D.BUILDER
        {
            if (PAppsToRemove.Items.Count == 0) { MessageBox.Show("No apps to uninstall"); }
            else
            {
                Enabled = false;
                MessageBox.Show("Uninstall will be attempted after pressing OK.\nThe application will become active when uninstall is complete.");

                UninstallProvisionedApps();

                Enabled = true;
                PAppsToRemove.Items.Clear();
                MessageBox.Show("Selected Provisioned Apps have been removed.");

                RefreshProvisionedApps(sender, e);
            }
        }

        void UninstallProvisionedApps()
        {
            foreach (var item in PAppsToRemove.Items)
            {
                powerShell.Commands.Clear();
                powerShell.AddCommand("Get-AppxProvisionedPackage").AddParameter("Online");
                powerShell.AddCommand("Where-Object").AddParameter("FilterScript", ScriptBlock.Create("$_.displayname -match \"" + item.ToString() + "\""));

                powerShell.AddCommand("remove-appxProvisionedpackage").AddParameter("Online");

                List<PSObject> psOut = new List<PSObject>(powerShell.Invoke());
            }
            /* if (powerShell.HadErrors)
             {
                 string fail = "Provisioned App removal failed for the following:\n";
                 btnPRefresh_Click(sender, e);
                 foreach (string i in PAppsToRemove.Items) if (PListOfAppsInstalled.Items.Contains(i)) fail += i + "\n";
                 MessageBox.Show(fail);
                 powerShell.Streams.Error.Clear();
             }*/
            if (powerShell.HadErrors) powerShell.Streams.Error.Clear(); //The only time this should throw errors, is if a package doesn't exist, which is not a problem
        }
        private void ProvisonedAppPageOpen(object sender, EventArgs e)
        {
            if (isAdmin)
            {
                LoadingApps loading = new LoadingApps();
                loading.Show();
                RefreshProvisionedApps(sender, e);
                loading.Dispose();
            }
        }

        private void PShowHiddenApps_CheckedChanged(object sender, EventArgs e)
        {
            if (PShowHiddenApps.Checked) MessageBox.Show("USING THIS CAN CAUSE IRREPARABLE DAMAGE TO YOUR OS");
            RefreshProvisionedApps(sender, e);
        }

        private void UpdateProvisionedAppCount()
        {
            int appCount = PListOfAppsInstalled.Items.Count;
            int removeCount = PAppsToRemove.Items.Count;
            lblPInstalledCount.Text = "Apps to keep [" + appCount.ToString() + "]";
            lblPRemoveCount.Text = "Apps to remove [" + removeCount.ToString() + "]";

            btnPMvAllRight.Enabled = btnPMvRight.Enabled = (appCount != 0);
            btnPMvLeft.Enabled = btnPUninstall.Enabled = btnPClearAppsRem.Enabled = (removeCount != 0);
        }

        private void ShowProvisionedAppInfo(object sender, EventArgs e)
        {
            MessageBox.Show("Provisoned apps are applications that Windows will attempt to reinstall during updates, or when a new user account is made.");
        }

        //-----------------------------------------------REGISTRY/PRIVACY SYSTEM-------------------------------------------------------
        private void ApplyPrivacySettings(object sender, EventArgs e)
        {
            RegistryKey RegHKLM = Registry.LocalMachine;
            if (Environment.Is64BitOperatingSystem) RegHKLM = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

            RegistryKey RegHKCU = Registry.CurrentUser;
            if (Environment.Is64BitOperatingSystem) RegHKCU = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);

            RegistryKey activeRegHKLM = RegHKLM;
            RegistryKey activeRegHKCU = RegHKCU;

            //privacy- general
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                bool itmChk = checkedListBox1.GetItemChecked(i);
                string key = null;
                switch (i)
                {
                    case 0: //Advertising ID
                        key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo\";
                        activeRegHKCU.OpenSubKey(key, true).SetValue("Enabled", itmChk ? 1 : 0, RegistryValueKind.DWord);
                        break;

                    case 1: //Locally relevant content
                        key = @"Control Panel\International\User Profile";
                        activeRegHKCU.OpenSubKey(key, true).SetValue("HttpAcceptLanguageOptOut", !itmChk ? 1 : 0, RegistryValueKind.DWord);
                        break;

                    case 2: //suggested content in start
                        key = @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager";
                        activeRegHKCU.OpenSubKey(key, true).SetValue("SubscribedContent-338388Enabled", itmChk ? 1 : 0, RegistryValueKind.DWord);
                        break;

                    case 3: //track app launches
                        key = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
                        activeRegHKCU.OpenSubKey(key, true).SetValue("Start_TrackProgs", itmChk ? 1 : 0, RegistryValueKind.DWord);
                        break;
                }
            }
            //privacy - notifications
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                bool isChecked = checkedListBox2.GetItemChecked(i);
                string key = null;
                switch (i)
                {
                    case 0://tips and tricks
                        key = @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager";
                        activeRegHKCU.OpenSubKey(key, true).SetValue("SubscribedContent-338389Enabled", isChecked ? 1 : 0, RegistryValueKind.DWord);
                        break;

                    case 1://whats new
                        key = @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager";
                        activeRegHKCU.OpenSubKey(key, true).SetValue("SubscribedContent-310093Enabled", isChecked ? 1 : 0, RegistryValueKind.DWord);
                        break;

                    case 2://notifications on lockscreen
                        key = @"Software\Microsoft\Windows\CurrentVersion\Notifications\Settings";
                        activeRegHKCU.OpenSubKey(key, true).SetValue("NOC_GLOBAL_SETTING_ALLOW_TOASTS_ABOVE_LOCK", isChecked ? 1 : 0, RegistryValueKind.DWord);
                        break;
                }

            }
            //privacy - app access control
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                bool isChecked = checkedListBox3.GetItemChecked(i);
                string basekey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore";
                string[] keyList = RegHKCU.OpenSubKey(basekey).GetSubKeyNames();
                string subkey = "";

                switch (i)
                {
                    case 0://camera
                        subkey = @"webcam";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 1://microphone
                        subkey = @"microphone";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 2://location services
                        subkey = @"location";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 3://radios
                        subkey = @"radios";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 4://notifications
                        subkey = @"userNotificationListener";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 5://speech and typing
                        basekey = @"Software\Microsoft";
                        subkey = @"InputPersonalization";
                        keyList = activeRegHKCU.OpenSubKey(basekey).GetSubKeyNames();
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        foreach (string key in new[] { "RestrictImplicitInkCollection", "RestrictImplicitTextCollection" })
                        {
                            activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue(key, isChecked ? 0 : 1, RegistryValueKind.DWord);
                        }

                        basekey += @"\" + subkey;
                        subkey = @"TrainedDataStore";
                        keyList = activeRegHKCU.OpenSubKey(basekey).GetSubKeyNames();
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("HarvestContacts", isChecked ? 1 : 0, RegistryValueKind.DWord);
                        break;

                    case 6://account into
                        subkey = @"userAccountInformation";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 7://contacts
                        subkey = @"contacts";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 8://calendar
                        subkey = @"appointments";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 9://call history
                        subkey = @"phoneCallHistory";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 10://email
                        subkey = @"email";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 11://tasks
                        subkey = @"userDataTasks";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 12://messaging (sms)
                        subkey = @"chat";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 13://unpaired communication
                        subkey = @"bluetoothSync";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 14://app diagnostic information
                        subkey = @"appDiagnostics";
                        if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("Value", isChecked ? "Allow" : "Deny", RegistryValueKind.String);
                        break;

                    case 15://background running
                        basekey = @"Software\Microsoft\Windows\CurrentVersion\";
                        subkey = @"BackgroundAccessApplications";
                        if (!activeRegHKCU.OpenSubKey(basekey).GetSubKeyNames().Contains(subkey)) activeRegHKCU.CreateSubKey(basekey + @"\" + subkey);
                        activeRegHKCU.OpenSubKey(basekey + @"\" + subkey, true).SetValue("GlobalUserDisabled", isChecked ? 0 : 1, RegistryValueKind.DWord);
                        break;
                }
            }
            //privacy - start menu
            for (int i = 0; i < checkedListBox4.Items.Count; i++)
            {
                string key = @"SOFTWARE\Policies\Microsoft\Windows";
                string[] keyList = RegHKLM.OpenSubKey(key).GetSubKeyNames();
                string subkey = "";
                bool isChecked = checkedListBox4.GetItemChecked(i);

                switch (i)
                {
                    case 0: //disable cortana
                        subkey = @"Windows Search";
                        if (!keyList.Contains(subkey)) activeRegHKLM.CreateSubKey(key + @"\" + subkey);
                        activeRegHKLM.OpenSubKey(key + @"\" + subkey, true).SetValue("AllowCortana", isChecked ? 1 : 0, RegistryValueKind.DWord);
                        break;

                    case 1: //safe search
                        subkey = @"Windows Search";
                        if (!keyList.Contains(subkey)) activeRegHKLM.CreateSubKey(key + @"\" + subkey);
                        activeRegHKLM.OpenSubKey(key + @"\" + subkey, true).SetValue("ConnectedSearchSafeSearch", isChecked ? 1 : 3, RegistryValueKind.DWord);
                        break;

                    case 2: //internet seach
                        subkey = @"Windows Search";
                        if (!keyList.Contains(subkey)) activeRegHKLM.CreateSubKey(key + @"\" + subkey);
                        activeRegHKLM.OpenSubKey(key + @"\" + subkey, true).SetValue("ConnectedSearchUseWeb", isChecked ? 1 : 0, RegistryValueKind.DWord);
                        break;

                    case 3: //device history
                        subkey = @"Windows Search";
                        if (!keyList.Contains(subkey)) activeRegHKLM.CreateSubKey(key + @"\" + subkey);
                        activeRegHKLM.OpenSubKey(key + @"\" + subkey, true).SetValue("DeviceHistoryEnabled", isChecked ? 1 : 0, RegistryValueKind.DWord);
                        break;
                }

            }
            //Telemtry ====================================================================================================================================================
            //tailored experiences
            activeRegHKCU.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Privacy", true).SetValue("TailoredExperiencesWithDiagnosticDataEnabled", checkBox1.Checked ? 1 : 0, RegistryValueKind.DWord); //Enabled

            //block telemetry
            ServiceController service = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == "DiagTrack");
            /* 0 = Boot * 1 = System * 2 = Automatic * 3 = Manual * 4 = Disabled */
            if (service != null)
            {
                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                activeRegHKLM.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack", true).SetValue("Start", checkBox2.Checked ? 2 : 4, RegistryValueKind.DWord);
                if (checkBox2.Checked) service.Start();
            }

            if (RegHKCU.OpenSubKey(@"Software\Microsoft\Siuf") == null) activeRegHKCU.CreateSubKey(@"Software\Microsoft\Siuf");

            if (checkBox3.Checked == true)              //ask for feedback
            {
                string key = @"Software\Microsoft\Siuf";
                string[] keyList = RegHKCU.OpenSubKey(key).GetSubKeyNames();

                if (keyList.Contains("Rules"))
                {
                    string[] vals = RegHKCU.OpenSubKey(key + @"\Rules").GetValueNames();

                    if (vals.Contains("NumberOfSIUFInPeriod")) activeRegHKCU.OpenSubKey(key + @"\Rules", true).DeleteValue("NumberOfSIUFInPeriod");
                }

            }
            else
            {
                string key = @"Software\Microsoft\Siuf";
                string[] keyList = RegHKCU.OpenSubKey(key).GetSubKeyNames();
                string subkey = "Rules";

                if (!keyList.Contains(subkey)) activeRegHKCU.CreateSubKey(key + @"\" + subkey);
                activeRegHKCU.OpenSubKey(key + @"\" + subkey, true).SetValue("NumberOfSIUFInPeriod", 0, RegistryValueKind.DWord); //Don't ask for feedback
            }

            HandleTelemetryReport();

            MessageBox.Show("Privacy settings have been saved to OS!\n(if anything has reset itself, it means the operation failed)");
            btnRefreshPriv_Click(sender, e);
        }

        private void HandleTelemetryReport()
        {
            RegistryKey RegHKCU = Registry.CurrentUser;
            if (Environment.Is64BitOperatingSystem) RegHKCU = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);

            //telemetry report
            string key = @"SOFTWARE\Policies\Microsoft\Windows";
            string[] keyList = RegHKCU.OpenSubKey(key).GetSubKeyNames();
            string subkey = "DataCollection";

            if (!keyList.Contains(subkey)) RegHKCU.CreateSubKey(key + @"\" + subkey);
            RegHKCU.OpenSubKey(key + @"\" + subkey, true).SetValue("AllowTelemetry", radioButton1.Checked ? 1 : 3, RegistryValueKind.DWord);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            //selects all the checkboxes if "select all" button is pressed
            if (btnSelectAll.Text == "Select All")
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
                for (int i = 0; i < checkedListBox2.Items.Count; i++)
                {
                    checkedListBox2.SetItemChecked(i, true);
                }
                for (int i = 0; i < checkedListBox3.Items.Count; i++)
                {
                    checkedListBox3.SetItemChecked(i, true);
                }
                for (int i = 0; i < checkedListBox4.Items.Count; i++)
                {
                    checkedListBox4.SetItemChecked(i, true);
                }

                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;

                btnSelectAll.Text = "Deselect All";
            }

            //deselects all the checkboxes if "deselect all" button is pressed
            else if (btnSelectAll.Text == "Deselect All")
            {
                UncheckAll();
            }
        }

        public void UncheckAll()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                checkedListBox3.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBox4.Items.Count; i++)
            {
                checkedListBox4.SetItemChecked(i, false);
            }

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;

            if (runOncePrivCheck == false) btnSelectAll.Text = "Select All";
        }

        private void Privacy_Enter(object sender, EventArgs e)
        {
            if (runOncePrivCheck)
            {
                btnRefreshPriv_Click(sender, e);
                checkBox2_CheckedChanged(sender, e);
                runOncePrivCheck = false;
            }

        }

        private void btnRefreshPriv_Click(object sender, EventArgs e)
        {
            UncheckAll();
            string[] sc = null;
            string subKey = null;

            RegistryKey RegHKLM = Registry.LocalMachine;
            if (Environment.Is64BitOperatingSystem) RegHKLM = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

            RegistryKey RegHKCU = Registry.CurrentUser;
            if (Environment.Is64BitOperatingSystem) RegHKCU = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);

            RegistryKey activeRegHKLM;
            RegistryKey activeRegHKCU;
            //------------------------------------------------------GENERAL--------------------------------------------------------------

            bool pass = true;
            activeRegHKCU = RegHKCU.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion");           //AdvertisingInfo
            //AdvertisingInfo
            subKey = "AdvertisingInfo";
            if (activeRegHKCU != null)
            {
                if (activeRegHKCU.GetSubKeyNames().Contains(subKey))
                {
                    if (activeRegHKCU.OpenSubKey(subKey).GetValue("Enabled") != null)
                    {
                        if ((Int32)activeRegHKCU.OpenSubKey(subKey).GetValue("Enabled") != 0)
                            checkedListBox1.SetItemChecked(0, true);
                    }
                    else pass = false;
                }
                else pass = false;
            }
            else pass = false;
            if (!pass) checkedListBox1.SetItemChecked(0, true);
            if (activeRegHKCU != null) activeRegHKCU.Close();
            pass = true;


            activeRegHKCU = RegHKCU.OpenSubKey(@"Control Panel\International\User Profile");            //Locally relevant content
            if (activeRegHKCU != null)
            {
                if (activeRegHKCU.GetValueNames().Contains("HttpAcceptLanguageOptOut"))
                {
                    if ((Int32)activeRegHKCU.GetValue("HttpAcceptLanguageOptOut") != 1)
                        checkedListBox1.SetItemChecked(1, true);
                }
                else pass = false;
            }
            else pass = false;
            if (!pass) checkedListBox1.SetItemChecked(1, true);
            if (activeRegHKCU != null) activeRegHKCU.Close();
            pass = true;

            activeRegHKCU = RegHKCU.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager"); //Suggested content
            sc = activeRegHKCU.GetValueNames();
            if (sc.Contains("SubscribedContent-338388Enabled"))
            { if ((Int32)activeRegHKCU.GetValue("SubscribedContent-338388Enabled") == 1) checkedListBox1.SetItemChecked(2, true); }
            else checkedListBox1.SetItemChecked(2, true);
            activeRegHKCU.Close();

            activeRegHKCU = RegHKCU.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");     //Track app launches
            if (activeRegHKCU.GetValueNames().Contains("Start_TrackProgs"))
            {
                if ((Int32)activeRegHKCU.GetValue("Start_TrackProgs") == 0) checkedListBox1.SetItemChecked(3, false);
                else checkedListBox1.SetItemChecked(3, true);
            }
            else checkedListBox1.SetItemChecked(3, true);
            activeRegHKCU.Close();

            //------------------------------------------------------NOTIFICATIONS-----------------------------------------------------

            activeRegHKCU = RegHKCU.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager");//Tips and tricks
            sc = activeRegHKCU.GetValueNames();
            if (sc.Contains("SubscribedContent-338389Enabled"))
            { if ((Int32)activeRegHKCU.GetValue("SubscribedContent-338389Enabled") == 1) checkedListBox2.SetItemChecked(0, true); }
            else checkedListBox2.SetItemChecked(0, true);
            activeRegHKCU.Close();

            activeRegHKCU = RegHKCU.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager");//What's new
            sc = activeRegHKCU.GetValueNames();
            if (sc.Contains("SubscribedContent-310093Enabled"))
            { if ((Int32)activeRegHKCU.GetValue("SubscribedContent-310093Enabled") == 1) checkedListBox2.SetItemChecked(1, true); }
            else checkedListBox2.SetItemChecked(1, true);
            activeRegHKCU.Close();

            activeRegHKCU = RegHKCU.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Notifications\Settings");//Notifications on Lockscreen
            sc = activeRegHKCU.GetValueNames();
            if (sc.Contains("NOC_GLOBAL_SETTING_ALLOW_TOASTS_ABOVE_LOCK"))
            { if ((Int32)activeRegHKCU.GetValue("NOC_GLOBAL_SETTING_ALLOW_TOASTS_ABOVE_LOCK") == 1) checkedListBox2.SetItemChecked(2, true); }
            else checkedListBox2.SetItemChecked(2, true);
            activeRegHKCU.Close();


            //------------------------------------------------------START MENU--------------------------------------------------------
            if (RegHKLM.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search") != null)                  //Cortana
            {
                activeRegHKLM = RegHKLM.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search");
                if (activeRegHKLM.GetValueNames().Contains("AllowCortana"))
                {
                    if ((Int32)activeRegHKLM.GetValue("AllowCortana") == 1) { checkedListBox4.SetItemChecked(0, true); }
                }
                else checkedListBox4.SetItemChecked(0, true);
                activeRegHKLM.Close();
            }
            else checkedListBox4.SetItemChecked(0, true);

            if (RegHKLM.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search") != null)                  //Safe Search
            {
                activeRegHKLM = RegHKLM.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search");
                if (activeRegHKLM.GetValueNames().Contains("ConnectedSearchSafeSearch"))
                {
                    if ((Int32)activeRegHKLM.GetValue("ConnectedSearchSafeSearch") != 3) { checkedListBox4.SetItemChecked(1, true); }
                }
                else checkedListBox4.SetItemChecked(1, true);
                activeRegHKLM.Close();
            }
            else checkedListBox4.SetItemChecked(1, true);

            if (RegHKLM.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search") != null)                  //Web Search
            {
                activeRegHKLM = RegHKLM.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search");
                if (activeRegHKLM.GetValueNames().Contains("ConnectedSearchUseWeb"))
                {
                    if ((Int32)activeRegHKLM.GetValue("ConnectedSearchUseWeb") != 0) { checkedListBox4.SetItemChecked(2, true); }
                }
                else checkedListBox4.SetItemChecked(2, true);
                activeRegHKLM.Close();
            }
            else checkedListBox4.SetItemChecked(2, true);


            if (RegHKLM.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search") != null)                  //Device History
            {
                activeRegHKLM = RegHKLM.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search");
                if (activeRegHKLM.GetValueNames().Contains("DeviceHistoryEnabled"))
                {
                    if ((Int32)activeRegHKLM.GetValue("DeviceHistoryEnabled") != 0) { checkedListBox4.SetItemChecked(3, true); }
                }
                else checkedListBox4.SetItemChecked(3, true);
                activeRegHKLM.Close();
            }
            else checkedListBox4.SetItemChecked(3, true);



            //------------------------------------------------------APP ACCESS CONTROL--------------------------------------------------------
            //----------CONSENT STORE----------------------- OPTED TO USE CURRENT USER--------------------------------------------------------
            activeRegHKCU = RegHKCU.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore");
            sc = activeRegHKCU.GetSubKeyNames();

            if (sc.Contains("webcam"))                                                                                                  //Webcam
            { if ((string)activeRegHKCU.OpenSubKey("webcam").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(0, true); }
            else checkedListBox3.SetItemChecked(0, true);

            if (sc.Contains("microphone"))                                                                                              //Microphone
            { if ((string)activeRegHKCU.OpenSubKey("microphone").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(1, true); }
            else checkedListBox3.SetItemChecked(1, true);

            if (sc.Contains("location"))                                                                                                //Location Services
            { if ((string)activeRegHKCU.OpenSubKey("location").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(2, true); }
            else checkedListBox3.SetItemChecked(2, true);

            if (sc.Contains("radios"))                                                                                                  //Radios
            { if ((string)activeRegHKCU.OpenSubKey("radios").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(3, true); }
            else checkedListBox3.SetItemChecked(3, true);

            if (sc.Contains("userNotificationListener"))                                                                                //Notifications
            { if ((string)activeRegHKCU.OpenSubKey("userNotificationListener").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(4, true); }
            else checkedListBox3.SetItemChecked(4, true);
            activeRegHKCU.Close();


            activeRegHKCU = RegHKCU.OpenSubKey(@"Software\Microsoft\InputPersonalization");                                             //Inking, typing, speech
            if (activeRegHKCU != null)
            {
                if (activeRegHKCU.GetValueNames().Contains("RestrictImplicitInkCollection") ||
                    activeRegHKCU.GetValueNames().Contains("RestrictImplicitTextCollection"))
                {
                    if (((Int32)activeRegHKCU.GetValue("RestrictImplicitInkCollection") != 1) ||
                         ((Int32)activeRegHKCU.GetValue("RestrictImplicitTextCollection") != 1))
                        checkedListBox3.SetItemChecked(5, true);
                }
                else checkedListBox3.SetItemChecked(5, true);
                activeRegHKCU.Close();
            }
            else checkedListBox3.SetItemChecked(5, true);


            activeRegHKCU = RegHKCU.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore");

            if (sc.Contains("userAccountInformation"))                                                                                  //Account Info
            { if ((string)activeRegHKCU.OpenSubKey("userAccountInformation").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(6, true); }
            else checkedListBox3.SetItemChecked(6, true);

            if (sc.Contains("contacts"))                                                                                                //Contacts
            { if ((string)activeRegHKCU.OpenSubKey("contacts").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(7, true); }
            else checkedListBox3.SetItemChecked(7, true);

            if (sc.Contains("appointments"))                                                                                            //Calender
            { if ((string)activeRegHKCU.OpenSubKey("appointments").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(8, true); }
            else checkedListBox3.SetItemChecked(8, true);

            if (sc.Contains("phoneCallHistory"))                                                                                        //Call history
            { if ((string)activeRegHKCU.OpenSubKey("phoneCallHistory").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(9, true); }
            else checkedListBox3.SetItemChecked(9, true);

            if (sc.Contains("email"))                                                                                                   //Email
            { if ((string)activeRegHKCU.OpenSubKey("email").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(10, true); }
            else checkedListBox3.SetItemChecked(10, true);

            if (sc.Contains("userDataTasks"))                                                                                           //Tasks
            { if ((string)activeRegHKCU.OpenSubKey("userDataTasks").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(11, true); }
            else checkedListBox3.SetItemChecked(11, true);

            if (sc.Contains("chat"))                                                                                                    //Messaging
            { if ((string)activeRegHKCU.OpenSubKey("chat").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(12, true); }
            else checkedListBox3.SetItemChecked(12, true);

            if (sc.Contains("bluetoothSync"))                                                                                           //Unpaired Communications (bluetooth sync with unpaired devices)
            { if ((string)activeRegHKCU.OpenSubKey("bluetoothSync").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(13, true); }
            else checkedListBox3.SetItemChecked(13, true);

            if (sc.Contains("appDiagnostics"))                                                                                           //App Diagnostics
            { if ((string)activeRegHKCU.OpenSubKey("appDiagnostics").GetValue("Value") != "Deny") checkedListBox3.SetItemChecked(14, true); }
            else checkedListBox3.SetItemChecked(14, true);

            activeRegHKCU.Close();
            activeRegHKCU = RegHKCU.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications");

            if (activeRegHKCU != null)
            {
                if (activeRegHKCU.GetValueNames().Contains("GlobalUserDisabled"))                                                       //Background Running
                { if ((Int32)activeRegHKCU.GetValue("GlobalUserDisabled") != 1) checkedListBox3.SetItemChecked(15, true); }
                else checkedListBox3.SetItemChecked(15, true);
            }
            else checkedListBox3.SetItemChecked(15, true);



            //------------------------------------------------------TELEMETRY------------------------------------------------------------------

            activeRegHKCU = RegHKCU.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection");                                 //Allow Telemetry
            if (activeRegHKCU != null)
            {
                if (activeRegHKCU.GetValueNames().Contains("AllowTelemetry"))
                {
                    if ((Int32)activeRegHKCU.GetValue("AllowTelemetry") == 1) radioButton1.Checked = true;
                    else radioButton2.Checked = true;
                }
                else radioButton2.Checked = true;
                activeRegHKCU.Close();
            }
            else radioButton2.Checked = true;


            activeRegHKCU = RegHKCU.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Privacy");                                   //Tailored Experiences
            if (activeRegHKCU != null)
            {
                if (activeRegHKCU.GetValueNames().Contains("TailoredExperiencesWithDiagnosticDataEnabled"))
                {
                    if ((Int32)activeRegHKCU.GetValue("TailoredExperiencesWithDiagnosticDataEnabled") != 0) checkBox1.Checked = true;
                }
                else checkBox1.Checked = true;
                activeRegHKCU.Close();
            }
            else checkBox1.Checked = true;


            activeRegHKCU = RegHKCU.OpenSubKey(@"Software\Microsoft\Siuf\Rules");                                                       //Ask for feedback
            if (activeRegHKCU != null)
            {
                if (activeRegHKCU.GetValueNames().Contains("NumberOfSIUFInPeriod"))
                {
                    if ((Int32)activeRegHKCU.GetValue("NumberOfSIUFInPeriod") != 0) checkBox3.Checked = true;
                }
                else checkBox3.Checked = true;
                activeRegHKCU.Close();
            }
            else checkBox3.Checked = true;

            //DiagTrack                                                                                                                 //Block Telemetry
            ServiceController serCont = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == "DiagTrack");
            if (serCont != null)
            {
                if (serCont.StartType != ServiceStartMode.Disabled) checkBox2.Checked = true;
                serCont.Close();
            }
            else checkBox2.Checked = false;


        }

        //----------------------------------------------------------TOOLSETS-----------------------------------------------------------
        private void btnUninstOneDrive_Click(object sender, EventArgs e)
        {
            MessageBox.Show("OneDrive will now uninstall. This will take some time.\nApplication will be disabled until this is done.");
            Enabled = false;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;

            string sysEnv = Environment.GetEnvironmentVariable("SYSTEMROOT");
            proc.StartInfo.FileName = "taskkill"; proc.StartInfo.Arguments = "/F /IM OneDrive.exe";
            proc.Start();

            if (Environment.Is64BitOperatingSystem) { proc.StartInfo.FileName = (sysEnv + @"\SysWOW64\OneDriveSetup.exe"); }
            else proc.StartInfo.FileName = (sysEnv + @"\System32\OneDriveSetup.exe");
            proc.StartInfo.Arguments = "/uninstall";
            proc.Start();

            btnUninstOneDrive.Enabled = false;
            proc.WaitForExit();
            /*  //This section is supposed to take ownership of onedrive folder, and delete it, but it's not working quite right yet
            string odDirect = Environment.GetEnvironmentVariable("USERPROFILE") + @"\OneDrive\";
            if (System.IO.Directory.Exists(odDirect))
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(odDirect);
                System.Security.AccessControl.FileSecurity fileSecurity = fileInfo.GetAccessControl();
                fileSecurity.SetOwner(ident.User);

                //            System.IO.Directory.SetAccessControl(odDirect, );
                System.IO.Directory.Delete(odDirect);
            }*/
            Enabled = true;
            MessageBox.Show("Onedrive has been uninstalled.");
        }

        private void btnResPhotoV_Click(object sender, EventArgs e)     //This uses a script provided by https://www.tenforums.com/tutorials/14312-restore-windows-photo-viewer-windows-10-a.html
        {
            ApplyRegistry(EmbResources.Restore_Windows_Photo_Viewer_ALL_USERS);

            MessageBox.Show("Windows Photo Viewer has been restored!\nDon't forget to set it to your default photo application through the settings app.\n\n" +
                "Registry file by: Shawn Brink \nhttps://www.tenforums.com/tutorials/14312-restore-windows-photo-viewer-windows-10-a.html");
        }

        private void btnUnPhotoV_Click(object sender, EventArgs e)
        {
            ApplyRegistry(EmbResources.Undo_Restore_Windows_Photo_Viewer_ALL_USERS);

            MessageBox.Show("Windows Photo Viewer has been removed!\nDon't forget to set it to your default photo application through the settings app.\n\n" +
                "Registry file by: Shawn Brink \nhttps://www.tenforums.com/tutorials/14312-restore-windows-photo-viewer-windows-10-a.html");
        }

        void ApplyRegistry(string resource)                                                             //Apply registry file (from resource) to windows
        {                                                                                               //This is really only used for "Windows Photo Viewer"
            System.Diagnostics.Process proc = new System.Diagnostics.Process();                         //An external process is created

            proc.StartInfo.CreateNoWindow = true;                                                       //We don't want a command prompt to pop up
            proc.StartInfo.RedirectStandardError = true;                                                //We won't need feedback for this
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;

            string tempPath = System.IO.Path.GetTempPath() + @"\Win10Photo" + Guid.NewGuid() + ".reg";  //Create a temp FilePath
            System.IO.StreamWriter sW = new System.IO.StreamWriter(tempPath, false, Encoding.Unicode);  //Open a file at the path
            sW.Write(resource);                                                                         //Load data from resource into Win10Photo(#).reg
            sW.Close();                                                                                 //Close the file

            proc.StartInfo.FileName = "REG";                                                            //REG IMPORT Win10Photo(#).reg (/reg:64)
            proc.StartInfo.Arguments = "IMPORT \"" + tempPath + "\"";
            if (Environment.Is64BitOperatingSystem) proc.StartInfo.Arguments += " /reg:64";             //This is needed, otherwise windows places registry keys in the wrong place

            proc.Start();                                                                               //Execute command
            proc.WaitForExit();
            System.IO.File.Delete(tempPath);                                                            //Delete the file
        }

        private void btnUninstEdge_Click(object sender, EventArgs e)                                    //Disable edge browser by moving its core files from * to *_DISABLED
        {
            Enabled = false;

            Process[] proc = Process.GetProcessesByName("MicrosoftEdge");                                                           //Kill running instances of Edge
            foreach (var pro in proc) { pro.Kill(); }
            foreach (var pro in proc) { pro.WaitForExit(); }
            System.Threading.Thread.Sleep(500);                                                                                     //Wait just to be sure

            string path = null;
            string[] pathAR = Directory.GetDirectories(Environment.GetEnvironmentVariable("WINDIR") + @"\SystemApps\", "Microsoft.MicrosoftEdge_*"); //Find edge
            if (pathAR.Count() == 0) MessageBox.Show("Edge browser not found!");
            else
            {
                path = pathAR[0];
                if (path.EndsWith("_DISABLED") && ((string)btnUninstEdge.Tag != "D")) MessageBox.Show("Edge is already disabled!");
                else try
                    {
                        if ((string)btnUninstEdge.Tag == "D")                                                                       //If edge is tagged as disabled then enable it
                        {
                            File.GetAccessControl(path).SetOwner(new NTAccount(Environment.UserDomainName, Environment.UserName));  //Set file permissions
                            string newPath = path.Remove((path.LastIndexOf("_DISABLED")));                                          //Trim path to remove _DISABLED                                                    //ERROR PRINT
                            Directory.Move(path, newPath);                                                                          //Apply changes
                            MessageBox.Show("Edge Browser has been Enabled!");                                                      //Inform user
                            btnUninstEdge.Tag = "";                                                                                 //Update UI
                            btnUninstEdge.Text = "Disable\nEdge Browser\n(not recommended)";
                        }
                        else
                        {
                            File.GetAccessControl(path).SetOwner(new NTAccount(Environment.UserDomainName, Environment.UserName));
                            Directory.Move(path, path + "_DISABLED");
                            MessageBox.Show("Edge Browser has been disabled!\nIt will not run if you attempt to start it.\n\n(THIS MAY MAKE YOUR SYSTEM UNSTABLE)");
                            btnUninstEdge.Tag = "D";
                            btnUninstEdge.Text = "Enable\nEdge Browser";
                        }
                    }
                    catch (System.Security.AccessControl.PrivilegeNotHeldException) { MessageBox.Show("EdgeBrowser disable failed."); }
                    catch (IOException) { MessageBox.Show("Edge is in use by the OS, please try again later."); }
            }
            Enabled = true;
        }

        private void btnUnPin_Click(object sender, EventArgs e)
        {
            if (StartManagement()) MessageBox.Show("Start menu has been cleared!");
            else MessageBox.Show("Start menu clear has failed.");
        }

        private void btnResStart_Click(object sender, EventArgs e)
        {
            string path = Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Microsoft\Windows\Shell\LayoutModification.xml";

            if (File.Exists(path)) if (File.ReadAllText(path) == EmbResources.LayoutModification) File.Delete(path);
            if (StartManagement(false)) MessageBox.Show("Start menu has been reset to default layout.");
            else MessageBox.Show("Start menu reset has failed");
        }

        bool StartManagement(bool clear = true)
        {
            string regpath = @"Software\Microsoft\Windows\CurrentVersion\CloudStore\Store\Cache\DefaultAccount\";
            string[] regvals = Registry.CurrentUser.OpenSubKey(regpath).GetSubKeyNames();
            string key = null;
            string matchKey = @"$start.tilegrid$windows.data.curatedtilecollection.tilecollection";
            foreach (string s in regvals) { if (s.Contains(matchKey)) key = s; }
            if (key == null) MessageBox.Show("Start menu tile data cannot be found\nOperation has failed.");
            else
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(regpath, true);
                regKey.DeleteSubKeyTree(key);

                string path = Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Microsoft\Windows\Shell\LayoutModification.xml";
                if (clear)
                {
                    StreamWriter sw = new StreamWriter(path);
                    sw.Write(EmbResources.LayoutModification);
                    sw.Close();
                }

                foreach (Process proc in Process.GetProcessesByName("explorer")) { proc.Kill(); proc.WaitForExit(); }       //Explorer restarts itself

                return true;
            }
            return false;
        }                                                      //Management utility (clear start cache), unpin all optional 

        private void btnQuickAccessEn_Click(object sender, EventArgs e)
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\", true);
            registry.SetValue("LaunchTo", 1);
            registry.Close();

            MessageBox.Show("Explorer Home has been set to \"This PC\"");
        }

        private void btnQuickAccessDis_Click(object sender, EventArgs e)
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\", true);
            registry.SetValue("LaunchTo", 2);
            registry.Close();

            MessageBox.Show("Explorer Home has been set to \"Quick Access\"");
        }

        private void Tools_Enter(object sender, EventArgs e)
        {
            string[] pathAR = Directory.GetDirectories(Environment.GetEnvironmentVariable("WINDIR") + @"\SystemApps\", "Microsoft.MicrosoftEdge_*");    //Check if Edge exists
            if (pathAR.Count() == 0) { btnUninstEdge.Enabled = false; }                                                                                 //Disable button if edge is missing
            else if (pathAR[0].EndsWith("_DISABLED"))                                                                                                   //If edge is disabled, then update UI
            {
                btnUninstEdge.Tag = "D";
                btnUninstEdge.Text = "Enable\nEdge Browser";
            }
            else                                                                                                                                        //Otherwise edge must be intact
            {                                                                                                                                           //Update UI
                btnUninstEdge.Tag = "";
                btnUninstEdge.Text = "Disable\nEdge Browser\n(not recommended)";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //if "block telemetry" is selected, disable telemetry report group box
            if (checkBox2.Checked == false)
            {
                groupBox2.Enabled = false;
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            //re-enables the telemetry report group box if "block telemetry" is deselected
            else
            {
                groupBox2.Enabled = true;
            }
            if (runOncePrivCheck == false) SelectButtonName();
        }

        //----------------------------------------------------------SAVE/LOAD----------------------------------------------------------
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            removeAppsList.Clear();
            removeProvisionedList.Clear();
            foreach (string item in AppsToRemove.Items) removeAppsList.Add(item);
            foreach (string item in PAppsToRemove.Items) removeProvisionedList.Add(item);

            //populates the removeTest list with the removeAppsList list
            xmlConfig.removeTest = removeAppsList;
            xmlConfig.removeProvisionedTest = removeProvisionedList;

            // TODO: This really needs to be a JSON dump and all of the checkboxes need matching IDs so it can be completely dynamic
            for (int i = 0; i < checkBoxList.Count; i++) checkBoxList[i] = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++) checkBoxList[i] = checkedListBox1.GetItemChecked(i) ? 1 : 0;
            for (int i = 0; i < checkedListBox2.Items.Count; i++) checkBoxList[4 + i] = checkedListBox2.GetItemChecked(i) ? 1 : 0;
            for (int i = 0; i < checkedListBox3.Items.Count; i++) checkBoxList[7 + i] = checkedListBox3.GetItemChecked(i) ? 1 : 0;
            for (int i = 0; i < checkedListBox4.Items.Count; i++) checkBoxList[23 + i] = checkedListBox4.GetItemChecked(i) ? 1 : 0;

            checkBoxList[27] = checkBox1.Checked ? 1 : 0;
            checkBoxList[28] = checkBox2.Checked ? 1 : 0;
            checkBoxList[29] = checkBox3.Checked ? 1 : 0;
            checkBoxList[30] = radioButton1.Checked ? 1 : 0;
            checkBoxList[31] = radioButton2.Checked ? 1 : 0;

            //populates the checkTest list with the checkBoxList list
            xmlConfig.checkTest = checkBoxList;

            //serialize the c object (holds the checkTest and removeTest lists)
            Save.Serialize(ref xmlConfig, "saveFile.xml");

            //open the popup that informs the user the save worked
            new Save_Load { label = "Saved settings to config file" }.ShowDialog();
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            if (Save.Deserialize(ref xmlConfig, "saveFile.xml") == true)
            {
                parseXML();
                (new Save_Load { label = "Successfully Loaded saved settings from config file" }).ShowDialog();
                runOncePrivCheck = false;
            }

            else new Save_Load { label = "Config file could not be found" }.ShowDialog();
        }

        private void SelectButtonName()
        {
            // If any checkbox in the tab is not checked, use select all instead of deselect
            btnSelectAll.Text = "Deselect All";
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i) == false)
                {
                    btnSelectAll.Text = "Select All";
                }
            }
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i) == false)
                {
                    btnSelectAll.Text = "Select All";
                }
            }
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                if (checkedListBox3.GetItemChecked(i) == false)
                {
                    btnSelectAll.Text = "Select All";
                }
            }
            for (int i = 0; i < checkedListBox4.Items.Count; i++)
            {
                if (checkedListBox4.GetItemChecked(i) == false)
                {
                    btnSelectAll.Text = "Select All";
                }
            }

            if (btnSelectAll.Text == "Deselect All") btnSelectAll.Text = "Select All";

            if (checkBox1.Checked == false) btnSelectAll.Text = "Select All";
            if (checkBox2.Checked == false) btnSelectAll.Text = "Select All";
            if (checkBox3.Checked == false) btnSelectAll.Text = "Select All";
        }

        private void SelectButtonText(object sender, ItemCheckEventArgs e)
        {
            if (runOncePrivCheck == false) SelectButtonName();
        }

        private void SelectButtonText(object sender, EventArgs e)
        {
            if (runOncePrivCheck == false) SelectButtonName();
        }
        //----------------------------------------------------------SAVE/LOAD----------------------------------------------------------

        private void LoadFromXml(string name)
        {
            if (File.Exists(name) == true)
            {
                var serializer = new XmlSerializer(typeof(Config));
                using (var stream = File.OpenRead(name))
                {
                    xmlConfig = (Config)(serializer.Deserialize(stream));
                }
                parseXML();
            }
        }

        private void parseXML()
        {
            UncheckAll();

            checkBoxList = xmlConfig.checkTest;
            removeAppsList = xmlConfig.removeTest;
            removeProvisionedList = xmlConfig.removeProvisionedTest;

            AppsToRemove.Items.Clear();
            PAppsToRemove.Items.Clear();
            foreach (var item in removeAppsList) AppsToRemove.Items.Add(item);
            foreach (var item in removeProvisionedList) PAppsToRemove.Items.Add(item);

            for (int i = 0; i < 4; i++) checkedListBox1.SetItemChecked(i, checkBoxList[i] == 1);
            for (int i = 4; i < 7; i++) checkedListBox2.SetItemChecked(i - 4, checkBoxList[i] == 1);
            for (int i = 7; i < 23; i++) checkedListBox3.SetItemChecked(i - 7, checkBoxList[i] == 1);
            for (int i = 23; i < 27; i++) checkedListBox4.SetItemChecked(i - 23, checkBoxList[i] == 1);

            checkBox1.Checked = checkBoxList[27] == 1;
            checkBox2.Checked = checkBoxList[28] == 1;
            checkBox3.Checked = checkBoxList[29] == 1;
            radioButton1.Checked = checkBoxList[30] == 1;
            radioButton2.Checked = checkBoxList[31] == 1;

        }

        public class Config
        {
            public List<int> checkTest { get; set; }

            public List<string> removeTest { get; set; }

            public List<string> removeProvisionedTest { get; set; }
        }

        //this is the xml serializer stuff
        public static class Save
        {
            public static void Serialize(ref Config c, string name)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog { Filter = "XML-File | *.xml" };

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                        File.WriteAllText(saveFileDialog1.FileName, "");
                        var serializer = new XmlSerializer(typeof(Config));
                        using (var stream = File.OpenWrite(saveFileDialog1.FileName))
                        {
                            serializer.Serialize(stream, c);
                        }
                    }
                }
            }

            public static bool Deserialize(ref Config c, string name)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog { Filter = "XML-File | *.xml" };

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var serializer = new XmlSerializer(typeof(Config));
                    using (var stream = File.OpenRead(openFileDialog1.FileName))
                    {
                        c = (Config)(serializer.Deserialize(stream));
                    }
                    return true;
                }
                return false;
            }
        }

        private void LoadPresetRecommended(object sender, EventArgs e) { LoadPreset(EmbResources.RecommendedSettings); }

        private void LoadPresetMinimal(object sender, EventArgs e) { LoadPreset(EmbResources.MinimalSettings); }

        private void LoadPresetRemoveAll(object sender, EventArgs e) { LoadPreset(EmbResources.RemoveAllSettings); }

        private void LoadPreset(string config)
        {
            runOncePrivCheck = false;
            string tempPath = Path.GetTempPath() + @"\Win10PrivUtil" + Guid.NewGuid() + ".xml";
            StreamWriter writer = new StreamWriter(tempPath, false, Encoding.Unicode);
            writer.Write(config);
            writer.Close();
            LoadFromXml(tempPath);
            File.Delete(tempPath);

            MessageBox.Show("Settings Loaded\nPress GO to apply or customize using the tabs.");
        }

        private void ApplyPreset(object sender, EventArgs e)
        {
            MessageBox.Show("Appling current configuration: Depending on settings, this could take some time.\nMake sure to read all prompts");
            if (runOncePrivCheck == false) ApplyPrivacySettings(sender, e);
            if (AppsToRemove.Items.Count != 0) UninstallApps(sender, e);
            if (PAppsToRemove.Items.Count != 0) ProvisionedUninstallClick(sender, e);
            MessageBox.Show("All done! Check out the Tools tab for extra features.");
        }
        //----------------------------------------------------------SAVE/LOAD----------------------------------------------------------

        private void GitLinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Process.Start(gitURL.Text); }

    }
}
