﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetAnalyzersDemo {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NetAnalyzersDemo.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All view models should inherit from BaseViewModel..
        /// </summary>
        internal static string ViewModelInheritanceAnalyzerDescription {
            get {
                return ResourceManager.GetString("ViewModelInheritanceAnalyzerDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to View model &apos;{0}&apos; class does not inherit from BaseViewModel. If a class is a view model (name ends with &apos;ViewModel&apos;) it should inherit from BaseViewModel..
        /// </summary>
        internal static string ViewModelInheritanceAnalyzerMessageFormat {
            get {
                return ResourceManager.GetString("ViewModelInheritanceAnalyzerMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to View models should inherit from BaseViewModel..
        /// </summary>
        internal static string ViewModelInheritanceAnalyzerTitle {
            get {
                return ResourceManager.GetString("ViewModelInheritanceAnalyzerTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All view model names should end with &apos;ViewModel&apos;.
        /// </summary>
        internal static string ViewModelNamingAnalyzerDescription {
            get {
                return ResourceManager.GetString("ViewModelNamingAnalyzerDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type name &apos;{0}&apos; inherits from BaseViewModel but does not end with &apos;ViewModel&apos;. All view models should names should end with &apos;ViewModel&apos;..
        /// </summary>
        internal static string ViewModelNamingAnalyzerMessageFormat {
            get {
                return ResourceManager.GetString("ViewModelNamingAnalyzerMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to View models names should end with &apos;ViewModel&apos;.
        /// </summary>
        internal static string ViewModelNamingAnalyzerTitle {
            get {
                return ResourceManager.GetString("ViewModelNamingAnalyzerTitle", resourceCulture);
            }
        }
    }
}
