﻿#pragma checksum "..\..\..\..\Product\ProductList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0DFA12391E66089F7EB953780B33468AD7CE3E02"
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
    /// ProductListWindow
    /// </summary>
    public partial class ProductListWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\Product\ProductList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\Product\ProductList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UpGrid;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Product\ProductList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CategorySelector;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Product\ProductList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Product\ProductList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\Product\ProductList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ProductListView;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Product\ProductList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn displayId;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Product\ProductList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddProduct;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL_;component/product/productlist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Product\ProductList.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.UpGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.CategorySelector = ((System.Windows.Controls.ComboBox)(target));
            
            #line 21 "..\..\..\..\Product\ProductList.xaml"
            this.CategorySelector.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CategorySelector_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lbl = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.btnBack = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\Product\ProductList.xaml"
            this.btnBack.Click += new System.Windows.RoutedEventHandler(this.backToAdmin_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ProductListView = ((System.Windows.Controls.ListView)(target));
            
            #line 26 "..\..\..\..\Product\ProductList.xaml"
            this.ProductListView.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ProductListView_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 26 "..\..\..\..\Product\ProductList.xaml"
            this.ProductListView.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ProductListView_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.displayId = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 8:
            this.AddProduct = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\Product\ProductList.xaml"
            this.AddProduct.Click += new System.Windows.RoutedEventHandler(this.AddProduct_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

