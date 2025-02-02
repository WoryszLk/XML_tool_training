using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;
using System.Xml;
using XML_tool.Services;
using System.Windows.Controls;
using XML_tool.Views;

namespace XML_tool
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenAddXmlWindow(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new AddXmlView();
            ButtonPanel.Visibility = Visibility.Collapsed;
        }

        private void OpenRemoveXmlWindow(object sender, RoutedEventArgs e) 
        {
            MainContent.Content = new RemoveXmlView();
            ButtonPanel.Visibility = Visibility.Collapsed;
        }
    }
}
