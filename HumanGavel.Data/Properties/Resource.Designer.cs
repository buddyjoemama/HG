﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HumanGavel.Data.Properties {
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
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HumanGavel.Data.Properties.Resource", typeof(Resource).Assembly);
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
        ///   Looks up a localized string similar to CREATE PROCEDURE [dbo].[GetTrending]
        ///	-- Add the parameters for the stored procedure here
        ///	@skip int,
        ///	@take int
        ///AS
        ///BEGIN
        ///	-- SET NOCOUNT ON added to prevent extra result sets from
        ///	-- interfering with SELECT statements.
        ///	SET NOCOUNT ON;
        ///
        ///	select @take = max(d) from(select @take as d union select 100 as d) f
        ///	select @skip = max(d) from(select @skip as d union select 0 as d) f
        ///
        ///	select * from (select v.CaseId, v.ParticipantId, sum(v.Value) as val, max(c.Name) as CaseName, max(p.Name) as Particip [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SP_GetTrending {
            get {
                return ResourceManager.GetString("SP_GetTrending", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to if OBJECT_ID(&apos;GetTrending&apos;) is not null
        ///DROP PROCEDURE [dbo].[GetTrending].
        /// </summary>
        internal static string SP_GetTrendingDrop {
            get {
                return ResourceManager.GetString("SP_GetTrendingDrop", resourceCulture);
            }
        }
    }
}
