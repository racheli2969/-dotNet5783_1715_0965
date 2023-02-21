﻿#pragma checksum "..\..\..\..\Product\ProductItem.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F013195A51CE9963380C4CA556AA27BA3D1D8391"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PL_.Product;
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


namespace PL_.Product {
    
    
    /// <summary>
    /// ProductItemWindow
    /// </summary>
    public partial class ProductItemWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\Product\ProductItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblId;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Product\ProductItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblForName;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Product\ProductItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblName;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Product\ProductItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPrice;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Product\ProductItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCategory;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Product\ProductItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddToCart;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Product\ProductItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDecreaseFromCart;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\Product\ProductItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtAvailability;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL_;component/product/productitem.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Product\ProductItem.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lblId = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            
            #line 18 "..\..\..\..\Product\ProductItem.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 19 "..\..\..\..\Product\ProductItem.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseAllWindows);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lblForName = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.lblName = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.lblPrice = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lblCategory = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.btnAddToCart = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\..\Product\ProductItem.xaml"
            this.btnAddToCart.Click += new System.Windows.RoutedEventHandler(this.AddToCart_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnDecreaseFromCart = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\Product\ProductItem.xaml"
            this.btnDecreaseFromCart.Click += new System.Windows.RoutedEventHandler(this.DecreaseFromCart_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.txtAvailability = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

