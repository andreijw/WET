﻿using System;
using System.Windows;

using WET.lib;
using WET.lib.Enums;

namespace WET.WPF
{
    public partial class MainWindow : Window
    {
        private readonly lib.ETWMonitor _monitor;

        public MainWindow()
        {
            InitializeComponent();

            _monitor = new ETWMonitor();

            _monitor.OnEvent += _monitor_OnEvent;

            Closing += MainWindow_Closing;

            _monitor.Start(monitorTypes: MonitorTypes.TcpDisconnect | MonitorTypes.TcpReceive | MonitorTypes.ProcessStart | MonitorTypes.RegistryUpdate | MonitorTypes.RegistryDelete | MonitorTypes.TcpConnect);
        }

        private void _monitor_OnEvent(object sender, lib.Containers.ETWEventContainerItem e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                txtBxDLLLoads.Text = txtBxDLLLoads.Text.Insert(0, $"{e.MonitorType}: {e.JSON}{Environment.NewLine}");
            });
        }
        
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _monitor.Stop();
        }
    }
}