﻿#pragma checksum "..\..\Module_stat.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "41F347C4E374205E4E01EC0F96ED45C74E610C62"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

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
using VeloMax_Debes_Delemazure;
using XamlAnimatedGif;


namespace VeloMax_Debes_Delemazure {
    
    
    /// <summary>
    /// Module_stat
    /// </summary>
    public partial class Module_stat : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 53 "..\..\Module_stat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView datagrid;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\Module_stat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock text;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\Module_stat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock text2;
        
        #line default
        #line hidden
        
        
        #line 393 "..\..\Module_stat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button boutonPrecedent;
        
        #line default
        #line hidden
        
        
        #line 430 "..\..\Module_stat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button boutonSuivant;
        
        #line default
        #line hidden
        
        
        #line 469 "..\..\Module_stat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock text_precedent;
        
        #line default
        #line hidden
        
        
        #line 476 "..\..\Module_stat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock text_suivant;
        
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
            System.Uri resourceLocater = new System.Uri("/VeloMax_Debes_Delemazure;component/module_stat.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Module_stat.xaml"
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
            this.datagrid = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.text = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.text2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            
            #line 98 "..\..\Module_stat.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_quantitevendue);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 136 "..\..\Module_stat.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_expirationadhesion);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 178 "..\..\Module_stat.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_programme);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 216 "..\..\Module_stat.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_client);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 258 "..\..\Module_stat.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_commande);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 296 "..\..\Module_stat.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_boutique);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 342 "..\..\Module_stat.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_quitter);
            
            #line default
            #line hidden
            return;
            case 11:
            this.boutonPrecedent = ((System.Windows.Controls.Button)(target));
            
            #line 392 "..\..\Module_stat.xaml"
            this.boutonPrecedent.Click += new System.Windows.RoutedEventHandler(this.btn_precedent);
            
            #line default
            #line hidden
            return;
            case 12:
            this.boutonSuivant = ((System.Windows.Controls.Button)(target));
            
            #line 429 "..\..\Module_stat.xaml"
            this.boutonSuivant.Click += new System.Windows.RoutedEventHandler(this.btn_suivant);
            
            #line default
            #line hidden
            return;
            case 13:
            this.text_precedent = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.text_suivant = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

