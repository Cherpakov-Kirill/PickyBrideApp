﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HallWebApi {
    using System;
    
    
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
    public class resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HallWebApi.resources", typeof(resources).Assembly);
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
        ///   Looks up a localized string similar to The average value of the princess&apos;s happiness = {0}.
        /// </summary>
        public static string AvgOfPrincessHappiness {
            get {
                return ResourceManager.GetString("AvgOfPrincessHappiness", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AttemptNum {0} | Taken contender : name = &quot;{1} {2}&quot;, prettiness = {3}.
        /// </summary>
        public static string ChosenContenderInfo {
            get {
                return ResourceManager.GetString("ChosenContenderInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #{0} : {1} {2} : {3}.
        /// </summary>
        public static string ContenderInfo {
            get {
                return ResourceManager.GetString("ContenderInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are no new contenders for the princess in the hall.
        /// </summary>
        public static string NoNewContender {
            get {
                return ResourceManager.GetString("NoNewContender", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to numberOfContenders should be more then zero.
        /// </summary>
        public static string NumberOfContendersShouldBeMoreThenZero {
            get {
                return ResourceManager.GetString("NumberOfContendersShouldBeMoreThenZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Princess chose contender with prettiness = {0}  Princess happiness : {1}.
        /// </summary>
        public static string PrincessChoseThePrinceResult {
            get {
                return ResourceManager.GetString("PrincessChoseThePrinceResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Princess could not choose any contender. Princess happiness : {0}.
        /// </summary>
        public static string PrincessCouldNotChooseAnyContenderResult {
            get {
                return ResourceManager.GetString("PrincessCouldNotChooseAnyContenderResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempt number = {0} | Princess happiness = {1}.
        /// </summary>
        public static string PrincessHappinessIs {
            get {
                return ResourceManager.GetString("PrincessHappinessIs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This contender did not visit the Princess.
        /// </summary>
        public static string ThisContenderDidNotVisit {
            get {
                return ResourceManager.GetString("ThisContenderDidNotVisit", resourceCulture);
            }
        }
    }
}
