﻿//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#if NET45 || NET46

#region using

using System.Management.Automation;
using System.Threading.Tasks;

#endregion

namespace Dapplo.Jira.PowerShell.Support
{
	/// <summary>
	///     The base for Async Cmdlets
	/// </summary>
	public abstract class AsyncCmdlet : PSCmdlet
	{
		/// <summary>
		///     This is called from the "Powershell Cmdlet" framework, calls the BeginProcessingAsync
		/// </summary>
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			AsyncPump.Run(async () => await BeginProcessingAsync());
		}

		/// <summary>
		///     Override this to implement the BeginProcessing with Async code
		/// </summary>
		protected virtual Task BeginProcessingAsync()
		{
			return Task.FromResult(0);
		}

		/// <summary>
		///     This is called from the "Powershell Cmdlet" framework, calls the EndProcessingAsync
		/// </summary>
		protected sealed override void EndProcessing()
		{
			AsyncPump.Run(async () => await EndProcessingAsync());
		}

		/// <summary>
		///     Override this to implement the EndProcessing with Async code
		/// </summary>
		protected virtual Task EndProcessingAsync()
		{
			return Task.FromResult(0);
		}

		/// <summary>
		///     Override to ProcessRecord and call ProcessRecordAsync
		/// </summary>
		protected sealed override void ProcessRecord()
		{
			AsyncPump.Run(async () => await ProcessRecordAsync());
		}

		/// <summary>
		///     Override this to implement the ProcessRecord with Async code
		/// </summary>
		protected virtual Task ProcessRecordAsync()
		{
			return Task.FromResult(0);
		}
	}
}

#endif