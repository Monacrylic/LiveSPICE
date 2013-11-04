﻿using System;
using System.ComponentModel;
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

namespace LiveSPICE
{
    public class MenuItemIcon : MenuItem, INotifyPropertyChanged
    {
        static MenuItemIcon() { DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuItemIcon), new FrameworkPropertyMetadata(typeof(MenuItemIcon))); }
        private Image enabled, disabled;

        private double width = 16;
        private double height = 16;
        [LocalizabilityAttribute(LocalizationCategory.None, Readability = Readability.Unreadable)]
        [TypeConverterAttribute(typeof(LengthConverter))]
        public double ImageWidth { get { return width; } set { width = value; NotifyChanged("ImageWidth"); } }
        [LocalizabilityAttribute(LocalizationCategory.None, Readability = Readability.Unreadable)]
        [TypeConverterAttribute(typeof(LengthConverter))]
        public double ImageHeight { get { return height; } set { height = value; NotifyChanged("ImageHeight"); } }

        public ImageSource IconSource 
        { 
            get { return enabled != null ? enabled.Source : null; } 
            set 
            {
                if (value != null)
                {
                    enabled = new Image() { Source = value, SnapsToDevicePixels = true };
                    if (enabled != null)
                    {
                        disabled = ImageButton.MakeDisabledImage(enabled);
                        enabled.SetBinding(Image.WidthProperty, new Binding("ImageWidth") { Source = this });
                        enabled.SetBinding(Image.HeightProperty, new Binding("ImageHeight") { Source = this });
                        disabled.SetBinding(Image.WidthProperty, new Binding("ImageWidth") { Source = this });
                        disabled.SetBinding(Image.HeightProperty, new Binding("ImageHeight") { Source = this });
                    }
                }
                else
                {
                    enabled = null;
                    disabled = null;
                }
                Update();
                NotifyChanged("IconSource"); 
            } 
        }


        public MenuItemIcon() { IsEnabledChanged += OnEnabledChanged; }

        private void Update() { Icon = IsEnabled ? enabled : disabled; }

        private void OnEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) { Update(); }

        // INotifyPropertyChanged.
        private void NotifyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
