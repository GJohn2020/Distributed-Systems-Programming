using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Windows;
using System.Windows.Controls;

namespace client
{
    public partial class MainWindow : Window
    {
        private HubConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSignalR();
        }

        private void InitializeSignalR()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:2118/chatroom")
                .WithAutomaticReconnect()
                .Build();

            // Receive messages
            connection.On<string, string>("GetMessage", (username, message) =>
            {
                Dispatcher.Invoke(() =>
                {
                    MessagesListBox.Items.Add($"{username}: {message}");
                });
            });
        }

        // CONNECT BUTTON
        private async void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.StartAsync();
                MessageBox.Show("Connected successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}");
            }
        }

        // SEND BUTTON
        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            if (connection.State != HubConnectionState.Connected)
            {
                MessageBox.Show("Not connected to server.");
                return;
            }

            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(MessageTextBox.Text))
            {
                MessageBox.Show("Enter username and message.");
                return;
            }

            await connection.InvokeAsync("BroadcastMessage",
                UsernameTextBox.Text,
                MessageTextBox.Text);

            MessageTextBox.Clear();
        }

        protected override async void OnClosed(EventArgs e)
        {
            if (connection != null)
                await connection.DisposeAsync();

            base.OnClosed(e);
        }
    }
}