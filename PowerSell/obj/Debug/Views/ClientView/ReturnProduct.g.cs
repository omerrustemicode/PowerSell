﻿#pragma checksum "..\..\..\..\Views\ClientView\ReturnProduct.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D8378A8069FE2E6519822F0667E7C6C2C56D46313D3AEF99D1150FEB57D800E1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PowerSell.Views.ClientView;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace PowerSell.Views.ClientView {
    
    
    /// <summary>
    /// ReturnProduct
    /// </summary>
    public partial class ReturnProduct : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\Views\ClientView\ReturnProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtOrderListId;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\Views\ClientView\ReturnProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSearchOrder;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\Views\ClientView\ReturnProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridProducts;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Views\ClientView\ReturnProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtQuantityToReturn;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\Views\ClientView\ReturnProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnReturnProduct;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PowerSell;component/views/clientview/returnproduct.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\ClientView\ReturnProduct.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtOrderListId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.btnSearchOrder = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\..\Views\ClientView\ReturnProduct.xaml"
            this.btnSearchOrder.Click += new System.Windows.RoutedEventHandler(this.btnSearchOrder_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dataGridProducts = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.txtQuantityToReturn = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btnReturnProduct = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\Views\ClientView\ReturnProduct.xaml"
            this.btnReturnProduct.Click += new System.Windows.RoutedEventHandler(this.btnReturnProduct_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
