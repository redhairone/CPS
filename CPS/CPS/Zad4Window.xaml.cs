﻿using CPS.VM;
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
using System.Windows.Shapes;

namespace CPS
{
    /// <summary>
    /// Logika interakcji dla klasy Zad4Window.xaml
    /// </summary>
    public partial class Zad4Window : Window
    {
        public Zad4Window()
        {
            InitializeComponent();
            DataContext = new Zad4ViewModel();
        }
    }
}
