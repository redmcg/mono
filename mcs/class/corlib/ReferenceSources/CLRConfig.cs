///------------------------------------------------------------------------------
/// <copyright file="CLRConfig.cs" company="Microsoft">
///     Copyright (c) Microsoft Corporation.  All rights reserved.
/// </copyright>                               
///
/// <owner>gpaperin</owner>
///------------------------------------------------------------------------------

using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Runtime.InteropServices;
using System.Security;

namespace System {
	/// <summary>
	/// For now, this class should be the central point to collect all managed declarations
	/// of native functions designed to expose config switches.
	/// In Dev11 M2.2 we will redesign this class to expose CLRConfig from within the CLR
	/// and refactor managed Fx code to access all compat switches through here.
	/// </summary>
	[FriendAccessAllowed]
	internal class CLRConfig {

		[FriendAccessAllowed]
		[System.Security.SecurityCritical]
		[ResourceExposure(ResourceScope.None)]
		[SuppressUnmanagedCodeSecurity]
		internal static bool CheckLegacyManagedDeflateStream()
		{
			return false;
		}

		[System.Security.SecurityCritical]
		[ResourceExposure(ResourceScope.None)]
		[SuppressUnmanagedCodeSecurity]
		internal static bool CheckThrowUnobservedTaskExceptions()
		{
			return false;
		}

	}  // internal class CLRConfig

}  // namespace System

// file CLRConfig
