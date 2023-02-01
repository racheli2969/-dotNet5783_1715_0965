﻿#pragma checksum "..\..\..\..\Order\Order.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8CF2EB157A4C9CC567FA44A62074099059E7E1C1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PL.Order;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PL.Order {
    
    
    /// <summary>
    /// OrderWindow
    /// </summary>
    public partial class OrderWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CustomerName;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Email;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label OrderStatus;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DateOrdered;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DateDelivered;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DateReceived;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label name;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDateDelivered;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtReceivedDate;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView OrderItemListView;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn displayId;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdateDeliveryDate;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Order\Order.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnReceivedDate;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL_;V1.0.0.0;component/order/order.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Order\Order.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.CustomerName = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.Email = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.OrderStatus = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.DateOrdered = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.DateDelivered = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.DateReceived = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.name = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.txtDateDelivered = ((System.Windows.Controls.TextBox)(target));
            
            #line 34 "..\..\..\..\Order\Order.xaml"
            this.txtDateDelivered.LostFocus += new System.Windows.RoutedEventHandler(this.DateDelivered_LostFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            this.txtReceivedDate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.OrderItemListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 11:
            this.displayId = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 12:
            
            #line 49 "..\..\..\..\Order\Order.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.btnUpdateDeliveryDate = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\..\Order\Order.xaml"
            this.btnUpdateDeliveryDate.Click += new System.Windows.RoutedEventHandler(this.btnUpdateDeliveryDate_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.btnReceivedDate = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\Order\Order.xaml"
            this.btnReceivedDate.Click += new System.Windows.RoutedEventHandler(this.UpdateReceivedDate_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

