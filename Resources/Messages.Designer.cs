//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Messages", typeof(Messages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not Matched with {0}.
        /// </summary>
        public static string Confirm {
            get {
                return ResourceManager.GetString("Confirm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalide Email Format.
        /// </summary>
        public static string EmailAddressError {
            get {
                return ResourceManager.GetString("EmailAddressError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Operation was Not Successfull.
        /// </summary>
        public static string Failur {
            get {
                return ResourceManager.GetString("Failur", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Maximum allowed file size is 1 MB.
        /// </summary>
        public static string MaxFileSize {
            get {
                return ResourceManager.GetString("MaxFileSize", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Record Found.
        /// </summary>
        public static string NotFind {
            get {
                return ResourceManager.GetString("NotFind", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This Field Is Required.
        /// </summary>
        public static string Required {
            get {
                return ResourceManager.GetString("Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Minimum String Length Of {0} should be {2} .
        /// </summary>
        public static string stringLength {
            get {
                return ResourceManager.GetString("stringLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Operation was Successfull.
        /// </summary>
        public static string Succeed {
            get {
                return ResourceManager.GetString("Succeed", resourceCulture);
            }
        }
    }
}
