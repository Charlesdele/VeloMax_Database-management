﻿#pragma checksum "..\..\AddEdit_fournisseur.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E6EC075D9086569397278F3BCFD5683C317AEA6A"
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
    /// AddEdit_fournisseur
    /// </summary>
    public partial class AddEdit_fournisseur : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 248 "..\..\AddEdit_fournisseur.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox get_siret;
        
        #line default
        #line hidden
        
        
        #line 263 "..\..\AddEdit_fournisseur.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox get_nom;
        
        #line default
        #line hidden
        
        
        #line 278 "..\..\AddEdit_fournisseur.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox get_contact;
        
        #line default
        #line hidden
        
        
        #line 316 "..\..\AddEdit_fournisseur.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox menu_adresse;
        
        #line default
        #line hidden
        
        
        #line 324 "..\..\AddEdit_fournisseur.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox get_libelle;
        
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
            System.Uri resourceLocater = new System.Uri("/VeloMax_Debes_Delemazure;component/addedit_fournisseur.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddEdit_fournisseur.xaml"
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
            this.get_siret = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.get_nom = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.get_contact = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.menu_adresse = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.get_libelle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            
            #line 346 "..\..\AddEdit_fournisseur.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnEnregistrer_click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 382 "..\..\AddEdit_fournisseur.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnAnnuler_click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 419 "..\..\AddEdit_fournisseur.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_adresse);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
