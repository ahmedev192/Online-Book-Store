﻿#pragma checksum "..\..\..\..\Views\EditBookView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EA883F268DCE19530363232ACA434B0CCBA8B85A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace OnlineBookStore.Views {
    
    
    /// <summary>
    /// EditBookView
    /// </summary>
    public partial class EditBookView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\Views\EditBookView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TitleTextBox;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Views\EditBookView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AuthorTextBox;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Views\EditBookView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PriceTextBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Views\EditBookView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox StockTextBox;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Views\EditBookView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EditionTextBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Views\EditBookView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CoverImageTextBox;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Views\EditBookView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CategoryComboBox;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Views\EditBookView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UploadImageButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/OnlineBookStore;V1.0.0.0;component/views/editbookview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\EditBookView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TitleTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.AuthorTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.PriceTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.StockTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.EditionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.CoverImageTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.CategoryComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.UploadImageButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\..\Views\EditBookView.xaml"
            this.UploadImageButton.Click += new System.Windows.RoutedEventHandler(this.UploadImageButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 37 "..\..\..\..\Views\EditBookView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

