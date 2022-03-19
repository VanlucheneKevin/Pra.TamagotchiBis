using System;
using System.Windows;
using Pra.Tamagotchi.Core.Interfaces;
using Pra.Tamagotchi.Core.Services;
using Pra.Tamagotchi.Core.Entities;

namespace Pra.Tamagotchi.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TamagotchiCollection tamagotchiCollection;
        public MainWindow()
        {
            InitializeComponent();
            tamagotchiCollection = new TamagotchiCollection();
            lstTamagotchis.SelectedIndex = 0;
            CreateSettingsButtons();
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            lstTamagotchis.ItemsSource = tamagotchiCollection.Tamagotchis;
            lstTamagotchis.Items.Refresh();
        }

        private void btnGrow_Click(object sender, RoutedEventArgs e)
        {
            if (lstTamagotchis.SelectedItem != null)
            {

                ITamagotchi currentTamagotchi = (ITamagotchi)lstTamagotchis.SelectedItem;
                if (currentTamagotchi is IHatchable)
                {

                    currentTamagotchi.Grow();
                    UpdateListBox();
                }
                if (currentTamagotchi is IFeedable)
                {
                    try
                    {
                        currentTamagotchi.Grow();
                        UpdateListBox();

                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, "groeien mislukt!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(SelectEggMessage(), "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddEgg_Click(object sender, RoutedEventArgs e)
        {
            tamagotchiCollection.AddEggs(1);
            UpdateListBox();
        }
        private void BtnFeed_Click(object sender, RoutedEventArgs e)
        {
            if (lstTamagotchis.SelectedItem != null)
            {
                ITamagotchi currentTamagotchi = (ITamagotchi)lstTamagotchis.SelectedItem;
                if (currentTamagotchi is Chick currentChicken)
                {
                    try
                    {
                        currentChicken.Feed();
                        UpdateListBox();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, "voeden mislukt", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void BtnHatch_Click(object sender, RoutedEventArgs e)
        {
            if (lstTamagotchis.SelectedItem != null)
            {
                ITamagotchi currentTamagotchi = (ITamagotchi)lstTamagotchis.SelectedItem;

                if (currentTamagotchi is Egg currentEgg)
                {
                    try
                    {
                        tamagotchiCollection.Hatch(currentEgg);
                        UpdateListBox();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, "Broeden mislukt", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(SelectEggMessage(), "Broeden mislukt", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LstTamagotchis_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CreateSettingsButtons();
        }

        private string SelectEggMessage()
        {
            string message = $"Gelieve een ei te selecteren";
            return message;
        }

        private void CreateSettingsButtons()
        {
            if(lstTamagotchis.SelectedItem == null)
            {
                btnFeed.IsEnabled = false;
                btnGrow.IsEnabled = false;
                btnHatch.IsEnabled = false;
                btnAddEgg.IsEnabled = true;
            }
            else
            {
                ITamagotchi currentTamagotchi = (ITamagotchi)lstTamagotchis.SelectedItem;
                if(currentTamagotchi is IHatchable)
                {
                    btnGrow.IsEnabled = true;
                    btnHatch.IsEnabled = true;
                    btnFeed.IsEnabled = false;
                }
                if(currentTamagotchi is IFeedable)
                {
                    btnFeed.IsEnabled = true;
                    btnGrow.IsEnabled = true;
                    btnHatch.IsEnabled = false;
                }
            }
        }

    }
}

