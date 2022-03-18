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
            btnFeed.IsEnabled = false;
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
                    currentTamagotchi.Grow();
                    UpdateListBox();
                }
            }
            else
            {
                MessageBox.Show(SelectEggMessage(), "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddEgg_Click(object sender, RoutedEventArgs e)
        {
            tamagotchiCollection.AddEggs(1);
            UpdateListBox();
        }

        private void btnHatch_Click(object sender, RoutedEventArgs e)
        {
            if (lstTamagotchis.SelectedItem != null)
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
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);


                        }

                    }
                    else
                    {
                        btnHatch.IsEnabled = false;
                    }


                }
                else
                {
                    MessageBox.Show(SelectEggMessage(), "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string SelectEggMessage()
        {
            string message = $"Gelieve een ei te selecteren";
            return message;
        }
    }
}

