﻿
using SoundWave.Core.Data;
using SoundWave.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoundWave.App
{
    /// <summary>
    /// Логика взаимодействия для SongListPage.xaml
    /// </summary>
    public sealed partial class SongListPage : Page
    {
        public SongListPage(MainViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
