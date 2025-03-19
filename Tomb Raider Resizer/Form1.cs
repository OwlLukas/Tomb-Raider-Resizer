using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Tomb_Raider_Resizer.Resize;

namespace Tomb_Raider_Resizer
{
    public partial class Form1 : Form
    {
        private List<GameInfo> games = new List<GameInfo>();

        // Timer zum kontinuierlichen Prüfen des Prozessstatus
        private Timer processCheckTimer;
        // Flag, das angibt, ob der Prozess gefunden wurde
        private bool isProcessFound = false;

        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Hinzufügen der Spiele in die ComboBox
            games.Add(new GameInfo("Tomb Raider I-III Starring Lara Croft", "tomb123"));
            games.Add(new GameInfo("Tomb Raider IV-VI Remastered", "tomb456"));
            games.Add(new GameInfo("Tomb Raider The Angel of Darkness (2003)", "TRAOD", "TRAOD_P3", "TRAOD_P4"));

            cb_gameList.DataSource = games;
            cb_gameList.SelectedIndex = 0;
            cb_gameList.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_gameList.SelectedIndexChanged += cb_gameList_SelectedIndexChanged;

            // Monitor ComboBox füllen
            PopulateMonitorComboBox();
            cb_monitor.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_monitor.SelectedIndexChanged += cb_monitor_SelectedIndexChanged;

            // Docking-Position ComboBox konfigurieren
            cb_dockPosition.Items.Clear();
            cb_dockPosition.Items.Add("Top Left");
            cb_dockPosition.Items.Add("Top Right");
            cb_dockPosition.Items.Add("Bottom Left");
            cb_dockPosition.Items.Add("Bottom Right");
            cb_dockPosition.Items.Add("Center");
            cb_dockPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_dockPosition.SelectedIndex = 4; // Standard: Zentriert
            cb_dockPosition.SelectedIndexChanged += cb_dockPosition_SelectedIndexChanged;

            // Event-Handler für TextChanged in den Feldern Breite und Höhe
            tB_w.TextChanged += (s, e) => UpdateButtonStates();
            tb_Y.TextChanged += (s, e) => UpdateButtonStates();

            // Initial den Button deaktivieren
            button1.Enabled = false;

            // Alle weiteren Steuerelemente auch deaktivieren, solange kein Prozess gefunden wurde
            UpdateButtonStates();

            // Starte sofort die Prozessüberprüfung für das initial ausgewählte Spiel
            GameInfo initialGame = cb_gameList.SelectedItem as GameInfo;
            if (initialGame != null)
            {
                CheckProcessStatus(initialGame);
            }
        }

        /// <summary>
        /// Aktualisiert den Zustand der Steuerelemente.
        /// Der Resize-Button wird nur aktiviert, wenn beide Felder gültige Zahlen enthalten und der Prozess gefunden wurde.
        /// Ebenso werden weitere Controls (Monitor, Docking und CheckBoxen) nur aktiviert, wenn der Prozess gefunden wurde.
        /// </summary>
        private void UpdateButtonStates()
        {
            int width, height;
            bool validInputs = int.TryParse(tB_w.Text, out width) && int.TryParse(tb_Y.Text, out height);
            bool enableControls = validInputs && isProcessFound;

            // Steuerelemente, die direkte Eingaben zulassen:
            button1.Enabled = enableControls;      // Resize-Button
            cb_monitor.Enabled = isProcessFound;     // Monitor-Auswahl
            cb_dockPosition.Enabled = isProcessFound;  // Docking-Position
            checkBox1.Enabled = isProcessFound;      // z.B. ForceWindowed
            checkBox2.Enabled = isProcessFound;      // z.B. RemoveFrame
            tB_w.Enabled = isProcessFound;           // TextBox für Breite
            tb_Y.Enabled = isProcessFound;           // TextBox für Höhe

            // Labels deaktivieren, wenn der Prozess nicht gefunden wurde:
            lblMonitor.Enabled = isProcessFound;
            lblDocking.Enabled = isProcessFound;
            lblResolution.Enabled = isProcessFound;
            lblWindowOptions.Enabled = isProcessFound;
            lvlnote.Enabled = isProcessFound;
            lblwidth.Enabled = isProcessFound;       // Label für Breite
            lblheight.Enabled = isProcessFound;      // Label für Höhe
        }




        // Event-Handler: Wird aufgerufen, wenn sich die Auswahl in der Spiele-ComboBox ändert.
        private void cb_gameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameInfo selectedGame = cb_gameList.SelectedItem as GameInfo;
            if (selectedGame != null)
            {
                CheckProcessStatus(selectedGame);
            }
        }

        /// <summary>
        /// Prüft kontinuierlich den Prozessstatus des angegebenen Spiels.
        /// Aktualisiert das Label lbl_processstatus entsprechend und ruft UpdateButtonStates() auf.
        /// </summary>
        /// <param name="game">Das GameInfo-Objekt, dessen Prozess(e) überwacht werden sollen.</param>
        private void CheckProcessStatus(GameInfo game)
        {
            // Vorherigen Timer beenden, falls bereits einer aktiv ist
            if (processCheckTimer != null)
            {
                processCheckTimer.Stop();
                processCheckTimer.Dispose();
                processCheckTimer = null;
            }

            // Timer initialisieren (Intervall 1000ms = 1 Sekunde)
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000;
            processCheckTimer.Tick += (s, ev) =>
            {
                // Die Prozessabfrage asynchron ausführen
                Task.Run(() =>
                {
                    bool processFound = false;
                    foreach (string procName in game.ProcessNames)
                    {
                        Process[] processes = Process.GetProcessesByName(procName);
                        if (processes.Length > 0)
                        {
                            processFound = true;
                            break;
                        }
                    }
                    // Aktualisiere das UI im UI-Thread
                    this.BeginInvoke((Action)(() =>
                    {
                        isProcessFound = processFound;
                        lbl_processstatus.Text = processFound ? "Process Found!" : "Not Connected!";
                        UpdateButtonStates();
                    }));
                });
            };

            processCheckTimer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Breite und Höhe holen
            int width = Convert.ToInt32(tB_w.Text);
            int height = Convert.ToInt32(tb_Y.Text);

            // Zustände der CheckBoxen abfragen
            bool RemoveFrame = checkBox2.Checked;
            bool ForceWindowed = checkBox1.Checked;

            // Hole das aktuell ausgewählte Spiel
            GameInfo selectedGame = cb_gameList.SelectedItem as GameInfo;
            if (selectedGame == null)
                return;

            // Ermittele den Arbeitsbereich des in cb_monitor ausgewählten Monitors
            int monitorIndex = cb_monitor.SelectedIndex;
            if (monitorIndex < 0 || monitorIndex >= Screen.AllScreens.Length)
                return;
            Rectangle workingArea = Screen.AllScreens[monitorIndex].WorkingArea;
            DockPosition selectedDock = (DockPosition)cb_dockPosition.SelectedIndex;

            // ResizeWindow Methode aus der Resize-Klasse aufrufen – mit Arbeitsbereich und Dock-Position
            if (selectedGame.Title == "Tomb Raider I-III Starring Lara Croft" ||
                selectedGame.Title == "Tomb Raider IV-VI Remastered")
            {
                Tomb_Raider_Resizer.Resize.ResizeWindow(selectedGame.ProcessNames.FirstOrDefault(), width, height, RemoveFrame, ForceWindowed, workingArea, selectedDock);
            }
            else
            {
                return;
            }
        }

        // Methode zum Befüllen der Monitor-ComboBox
        private void PopulateMonitorComboBox()
        {
            cb_monitor.Items.Clear();

            foreach (Screen screen in Screen.AllScreens)
            {
                string itemText = $"{screen.DeviceName} ({screen.Bounds.Width}x{screen.Bounds.Height})";
                cb_monitor.Items.Add(itemText);
            }

            if (cb_monitor.Items.Count > 0)
            {
                cb_monitor.SelectedIndex = 0;
            }
        }

        // Event-Handler für die Monitor-Auswahl
        private void cb_monitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoveGameWindowToSelectedMonitor();
        }

        private void MoveGameWindowToSelectedMonitor()
        {
            int index = cb_monitor.SelectedIndex;
            if (index < 0 || index >= Screen.AllScreens.Length)
                return;

            Screen selectedScreen = Screen.AllScreens[index];
            Rectangle workingArea = selectedScreen.WorkingArea;

            // Hole das aktuell ausgewählte Spiel
            GameInfo selectedGame = cb_gameList.SelectedItem as GameInfo;
            if (selectedGame == null)
                return;

            // Hier wird der erste gültige Prozessname verwendet.
            string processName = selectedGame.ProcessNames.FirstOrDefault();
            if (string.IsNullOrEmpty(processName))
                return;

            // Aufruf der Methode in der Resize-Klasse
            Tomb_Raider_Resizer.Resize.MoveWindowToMonitor(processName, workingArea);
        }

        private void cb_dockPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hole den Zielmonitor anhand der cb_monitor-Auswahl
            int monitorIndex = cb_monitor.SelectedIndex;
            if (monitorIndex < 0 || monitorIndex >= Screen.AllScreens.Length)
                return;
            Screen selectedScreen = Screen.AllScreens[monitorIndex];
            Rectangle workingArea = selectedScreen.WorkingArea;

            // Hole das aktuell ausgewählte Spiel
            GameInfo selectedGame = cb_gameList.SelectedItem as GameInfo;
            if (selectedGame == null)
                return;

            // Verwende den ersten gültigen Prozessnamen
            string processName = selectedGame.ProcessNames.FirstOrDefault();
            if (string.IsNullOrEmpty(processName))
                return;

            // Bestimme die gewünschte Dock-Position anhand des SelectedIndex der ComboBox
            DockPosition dockPos = (DockPosition)cb_dockPosition.SelectedIndex;
            Tomb_Raider_Resizer.Resize.DockWindowToMonitor(processName, workingArea, dockPos);
        }
    }
}
