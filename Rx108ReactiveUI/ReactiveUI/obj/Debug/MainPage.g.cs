﻿#pragma checksum "C:\Users\ashutosh\Documents\Visual Studio 2010\Projects\ReactiveUI\ReactiveUI\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BB34677DEEA90E5D3CF959764CDC61D0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.450
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace ReactiveUI {
    
    
    public partial class MainPage : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Button NonRxButton;
        
        internal System.Windows.Controls.Button RxButton;
        
        internal System.Windows.Controls.Button RxWithSchedulerButton;
        
        internal System.Windows.Controls.ComboBox MyComboBox;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/ReactiveUI;component/MainPage.xaml", System.UriKind.Relative));
            this.NonRxButton = ((System.Windows.Controls.Button)(this.FindName("NonRxButton")));
            this.RxButton = ((System.Windows.Controls.Button)(this.FindName("RxButton")));
            this.RxWithSchedulerButton = ((System.Windows.Controls.Button)(this.FindName("RxWithSchedulerButton")));
            this.MyComboBox = ((System.Windows.Controls.ComboBox)(this.FindName("MyComboBox")));
        }
    }
}

